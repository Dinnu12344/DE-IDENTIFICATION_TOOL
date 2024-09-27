import json
import sqlite3
import pandas as pd
import hashlib
from datetime import datetime
import numpy as np
from sqlalchemy import column
import Miscellaneous_Functions as mf
import re
from faker import Faker
from datetime import timedelta
from dateutil.relativedelta import relativedelta

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

def pseudonymization(column_name, df,hippa_related_column):
    print("pseudo staring")
    fake = Faker()
    faker_methods = {
    "name": fake.name,
    "first_name": fake.first_name,
    "last_name": fake.last_name,
    "address": fake.address,
    "street_address":fake.street_address,
    "city": fake.city,
    "state": fake.state,
    "postcode": fake.postcode,
    "zipcode":fake.zipcode,
    "latitude": fake.latitude,
    "longitude": fake.longitude,
    "date_of_birth": fake.date_of_birth,
    "date_this_century": fake.date_this_century,
    "phone_number": fake.phone_number,
    "email": fake.email,
    "ssn": fake.ssn,
    "random_number": lambda: fake.random_number(digits=10),  # Example with 10 digits
    "license_plate": fake.license_plate,
    "url": fake.url,
    "ipv4": fake.ipv4,
    "image_url": fake.image_url,

    'id': fake.uuid4,
    'gender': fake.random_element(elements=('Male', 'Female', 'Other')),
    'marital_status': fake.random_element(elements=('Single', 'Married', 'Divorced', 'Widowed')),
    'word': fake.word,
    'sentence': fake.sentence,
    'bank_account_number': fake.bban,
    }

    if hippa_related_column in faker_methods:
        faker_method = faker_methods[hippa_related_column]
        
        # Get unique values from the column
        unique_values = df[column_name].unique()
        
        # Generate fake data for each unique value
        fake_data = {val: faker_method() for val in unique_values}
        
        # Map the fake data back to the original column
        df[column_name] = df[column_name].map(fake_data)
    else:
        print(f"Faker method '{hippa_related_column}' not found.")
    print("pseduo ending")
    print(df)
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
#--------------------------------------------------------------------------------------------------

#def masking(column_name, df):
#   df[column_name]= df[column_name].astype(str)
#   masked_df = masking(column_name, df)
#
#    return masked_df



#-----------------------------------------------------------------------------------------------------------
#anonymization


import hashlib

def anonymization(column_name, df):
    # Get distinct values in the column (whether they are strings or integers)
    distinct_names = df[column_name].unique()

    # Create an anonymization map, converting each value to a string before hashing
    anonymization_map = {name: hashlib.sha256(str(name).encode()).hexdigest() for name in distinct_names}

    # Replace the original column with the hashed values
    df[column_name] = df[column_name].map(anonymization_map)
    
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


#----------------------------------------------------------------------------------------------------------
#Adding N day, months, years to date
def add_days(df, date_column, days):
    
    df[date_column] = pd.to_datetime(df[date_column]) + timedelta(days=days)
    return df

def add_months(df, date_column, months):
    
    df[date_column] = pd.to_datetime(df[date_column]).apply(lambda x: x + relativedelta(months=months))
    return df

def add_years(df, date_column, years):
    
    df[date_column] = pd.to_datetime(df[date_column]).apply(lambda x: x + relativedelta(years=years))
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



def replace_dates_within_range(df, date_column, start_date, end_date):
    #df[date_column] = pd.to_datetime(df[date_column])  # Ensure the date column is in datetime format
    start_date = datetime.strptime(start_date, "%Y-%m-%d")
    end_date = datetime.strptime(end_date, "%Y-%m-%d")
    
    date_range = (end_date - start_date).days
    df[date_column] = df[date_column].apply(lambda x: start_date + timedelta(days=np.random.randint(0, date_range)))
    return df
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
def process_column_info(column_name, data_type, technique, df,HippaRelatedColumn, primaryKey,keys_path):
    #print("Process Fun")
    if(primaryKey=="Yes"):
        
        key=""
        print(keys_path)
        # Open and read the file
        with open(keys_path, 'r') as file:
            key = file.read()
        print(key)
        df[f'{column_name}_hash_key'] = df[column_name].apply(lambda x: generate_hash(x, key))

    if technique == "Pseudonymization":
        df = pseudonymization(column_name, df,HippaRelatedColumn)
    elif technique == "Masking":
        df = masking(column_name, df)
    elif technique == "Anonymization":
        df = anonymization(column_name, df)
    elif technique == 'DateTo20_30years':
        df = dateTo20_30years(column_name, df)
    elif technique == 'DateToMonth,Year':
        df = month_year_generalization(column_name, df)
    else:
        #print("pROCESS FUN GEN CONDITION")
        #df = generalization(column_name, data_type, df)
        return df

    return df

#------------------------------------------------------------------------------------------------------------

#Date Time Add Range
def process_column_info1(column_name, data_type, technique, df,HippaRelatedColumn,startDate,endDate, primaryKey,keys_path):
    if(primaryKey=="Yes"):
        
        key=""
        print(keys_path)
        # Open and read the file
        with open(keys_path, 'r') as file:
            key = file.read()
        print(key)
        df[f'{column_name}_hash_key'] = df[column_name].apply(lambda x: generate_hash(x, key))

    if technique == "Pseudonymization":
        df = pseudonymization(column_name, df,HippaRelatedColumn)
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
    elif technique == 'DateTimeAddRange':
        df = replace_dates_within_range(df, column_name, startDate, endDate)
    
    else:
        #print("pROCESS FUN GEN CONDITION")
        #df = generalization(column_name, data_type, df)
        return df

    return df

#---------------------------------------------------------------------------------------------------

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
                    HippaRelatedColumn=column_info["HippaRelatedColumn"]
                    startDate=column_info["StartDate"]
                    endDate=column_info["EndDate"]
                    primaryKey = column_info["Keys"]
                    
                    #print("Start Date:"+startDate)
                    if startDate is None:
                        df = process_column_info(column_name, data_type, technique, df,HippaRelatedColumn, primaryKey,keys_path)
                    else:
                        df = process_column_info1(column_name, data_type, technique, df,HippaRelatedColumn,startDate,endDate, primaryKey,keys_path)
                    #if(startDate=='null')
                    #df = process_column_info(column_name, data_type, technique, df,HippaRelatedColumn, primaryKey,keys_path)

            # Display the DataFrame
            print(df)
            #df = df.convert_dtypes()
            # print("Deidentification Sucess")
            res=mf.Df_Data_To_Sqlite(db_file_path,df,"de_identified_"+table_name)
            if(res=="success"):


                Status = "Success"
                Comment = f"Successfully De-identified the data for the table : '{table_name}'"
                return Status,Comment
            else:
                Status = "Falied"
                Comment = f"De-identified failed: '{res}\t{table_name}'"
                return Status,Comment
        
    except Exception as e:

        Status = f"Failed {e}"
        # Comment = f"Failed to De-identify the data for the table : '{table_name}' with one or more exceptions : {e}"
        return Status  
        # print(e)



#-----------------------------------------------------------------------------------------------------------

#-----------------------------------------------------------------------------------------------------------

