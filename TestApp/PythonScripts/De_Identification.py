import json
import sqlite3
import pandas as pd
import hashlib
from datetime import datetime
import numpy as np
import Miscellaneous_Functions as mf
import re

#-----------------------------------------------------------------------------------------------------------------

#This function is used for validating the usergiven data types for the columns of the table
def validate_UserGiven_DataTypes(json_file,df):
    ls=[]
    with open(json_file, 'r') as json_file:
        column_info_list = json.load(json_file)
        for column_info in column_info_list:
            column_name = column_info["Column"]
            data_type = column_info["DataType"]
            ls.append(column_name)
            # technique = column_info["technique"]
            
            ##
            if(data_type=="Int"):
                try:
                    df[column_name] = df[column_name].astype(int)
                except:
                    return "Cannot convert to Specified data type choose compatable data type"
            elif(data_type=="Float"):
                try:
                    df[column_name] = df[column_name].astype(float)
                except:
                    return "Cannot convert to Specified data type choose compatable data type"
            elif(data_type=="String"):
                try:
                    print(data_type,column_name)
                    df[column_name] = df[column_name].astype(str)
                except:
                    return "Cannot convert to Specified data type choose compatable data type"
            elif(data_type=="Date"):
                try:
                    df[column_name] = pd.to_datetime(df[column_name])
                except:
                    return "Cannot convert to Specified data type choose compatable data type"

    df = df[ls]
                
    return df 

#-----------------------------------------------------------------------------------------------------------
# Pesudomyzation

def pseudonymization(column_name, df):
    # Get distinct values from the specified column
    distinct_names = df[column_name].unique()

    # Create a dictionary to map original names to pseudonyms
    pseudonym_map = {name: f"{column_name}{i+1}" for i, name in enumerate(distinct_names)}

    # Replace values in the 'Name' column with pseudonyms
    df[column_name] = df[column_name].map(pseudonym_map)
    
    return df




#-----------------------------------------------------------------------------------------------------------
#Masking



def is_email(value):
    """
    Simple check to see if the string is an email address.
    """
    return bool(re.match(r"[^@]+@[^@]+\.[^@]+", value))

def mask_email(email):
    """
    Mask the email address while preserving structure.
    """
    username, domain = email.split('@')
    domain_name, domain_tld = domain.rsplit('.', 1)

    if len(username) <= 3:
        masked_username = '*' * len(username)
    else:
        masked_username = username[0] + '*' * (len(username) - 2) + username[-1]

    masked_domain_name = domain_name[0] + '*' * (len(domain_name) - 2) + domain_name[-1]

    return f"{masked_username}@{masked_domain_name}.{domain_tld}"

def mask_value(value):
    """
    Mask non-email values by their length and structure.
    """
    if len(value) <= 3:
        return '*' * len(value)
    else:
        return value[:2] + 'X' * (len(value) - 3) + value[-1]

def masking(column_name, df):
    df[column_name] = df[column_name].astype(str)
    distinct_values = df[column_name].unique()
    masking_map = {}

    for value in distinct_values:
        if is_email(value):
            masked_value = mask_email(value)
        else:
            masked_value = mask_value(value)
        masking_map[value] = masked_value

    df[column_name] = df[column_name].map(masking_map)

    return df


def masking(column_name, df):
    df[column_name]= df[column_name].astype(str)
    masked_df = masking(column_name, df)

    return masked_df



#-----------------------------------------------------------------------------------------------------------
#anonymization

def anonymization(column_name,df):
#     hashed_value = hashlib.sha256(name.encode()).hexdigest()
    distinct_names = df[column_name].unique()
    anonymization_map = {name: hashlib.sha256(name.encode()).hexdigest()  for i, name in enumerate(distinct_names)}

    df[column_name] = df[column_name].map(anonymization_map) 
    ("anonymization fun")
    return df
#-----------------------------------------------------------------------------------------------------------
#generalization

def generalization(column_name, data_type_df,df):
    
    print(df.dtypes)
    
    if data_type_df == 'Int' or data_type_df == 'Float':
        # Generalize numeric data (e.g., rounding, binning, scaling)
        # Example: Round numerical values to the nearest 10
        
        df[column_name] = df[column_name].apply(lambda x: round(x, -1))

    elif data_type_df == 'datetime64' or data_type_df=='Date' :
        # Generalize date and time data (e.g., bucketing, anonymization, rounding)
        # Example: Round dates to the nearest month
        df[column_name] = df[column_name].dt.to_period('M')
        df[column_name] = df[column_name].astype(str)


    else:
        print("Unsupported data type")

    return df

#-----------------------------------------------------------------------------------------------------------
#dateTo20_30years

def dateTo20_30years(column_name, df):
    # Assuming your DataFrame is named 'df'
    # df[column_name] = pd.to_datetime(df[column_name])

    # Calculate the current year
    current_year = datetime.now().year
    
    # Calculate the age
    df[column_name] = current_year - df[column_name].dt.year

    # Create age ranges
    df[column_name]= pd.cut(df[column_name], bins=[0, 10, 20, 30, 40, 50, float('inf')],
                            labels=['<10 years', '10-20 years', '20-30 years', '30-40 years', '40-50 years', '50+ years'])

    # Display the resulting DataFrame
    # print(df[['DateOfBirth', 'AgeRange']])
    return df

#-----------------------------------------------------------------------------------------------------------
#month_year_generalization

def month_year_generalization(column_name, df):
    df[column_name] = df[column_name].dt.strftime('%B %Y')
    return df

#-----------------------------------------------------------------------------------------------------------
#dateTimeAddRange

def dateTimeAddRange(column_name,df):

# Define the range of perturbation in days
    perturbation_range = 3

    # Apply perturbation to the datetime column
    df[column_name] = df[column_name] + pd.Timedelta(days=np.random.randint(-perturbation_range, perturbation_range+1))
    return df
    # Display the DataFrame with perturbed datetime values
    # print(df)
#-----------------------------------------------------------------------------------------------------------
def generate_hash(value,key):
    print("generate fun")
  
    # Convert both the value and key to strings
    value_str = str(value)
    key_str = str(key)
    # Combine the value and key
    combined = value_str + key_str
    # Use SHA-256 hash function to generate the hash key
    hash_key = hashlib.sha256(combined.encode()).hexdigest()
    return hash_key
#------------------------------------------------------------------------------------------------------------

#This function maps the each column to the respective technique function
def process_column_info(column_name, data_type, technique, df, primaryKey,keys_path):
    print("Process Fun")
    if(primaryKey=="Yes"):
        
        key=""
        print(keys_path)
        # Open and read the file
        with open(keys_path, 'r') as file:
            key = file.read()
        print(key)
        df[f'{column_name}_hash_key'] = df[column_name].apply(lambda x: generate_hash(x, key))

    if technique == "Pseudonymization":
        df = pseudonymization(column_name, df)
    elif technique == "Masking":
        df = masking(column_name, df)
    elif technique == "Anonymization":
        df = anonymization(column_name, df)
    elif technique == 'DateTo20_30years':
        df = dateTo20_30years(column_name, df)
    elif technique == 'DateToMonth,Year':
        df = month_year_generalization(column_name, df)
    elif technique == 'DateToAddRange':
        df = dateTimeAddRange(column_name, df)
    else:
        print("pROCESS FUN GEN CONDITION")
        df = generalization(column_name, data_type, df)

    return df

#------------------------------------------------------------------------------------------------------------

#This is the main function for the total procass of deidentification process
def de_Identification_Main(config_files_path,table_name,db_file_path,keys_path):

    try:
        
        df=mf.SqlLite_Data_To_Df(db_file_path,table_name)
        # Specify the fipath of the JSON file
        json_file = config_files_path

        df = validate_UserGiven_DataTypes(json_file, df)
        if isinstance(df, str) and df == "Cannot convert to Specified data type choose compatable data type":
            return "Send validation error to fornt end such that\nCannot convert to Specified data type, choose compatable data type"
        else:
            
            # Read the JSON file and process column information
            with open(json_file, 'r') as json_file:
                column_info_list = json.load(json_file)
                for column_info in column_info_list:
                    column_name = column_info["Column"]
                    data_type = column_info["DataType"]
                    technique = column_info["Technique"]
                    primaryKey = column_info["Keys"]
                    
                    df = process_column_info(column_name, data_type, technique, df, primaryKey,keys_path)

            # Display the DataFrame
            # print(df)
            df = df.convert_dtypes()
            # print("Deidentification Sucess")
            mf.Df_Data_To_Sqlite(db_file_path,df,"de_identified_"+table_name)

            Status = "Success"
            Comment = f"Successfully De-identified the data for the table : '{table_name}'"
            return Status,Comment
        
    except Exception as e:

        Status = f"Failed {e}"
        # Comment = f"Failed to De-identify the data for the table : '{table_name}' with one or more exceptions : {e}"
        return Status  
        # print(e)



#-----------------------------------------------------------------------------------------------------------

#-----------------------------------------------------------------------------------------------------------

