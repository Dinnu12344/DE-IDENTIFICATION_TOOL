import pandas as pd
import pyodbc
import sqlite3
import random
import Miscellaneous_Functions as mf

#--------------------------------------------------------------------------------------------------------------------------
# Fuction for importing the SqlServer Data to the dataframe

def Import_SqlServer_Data_To_SqLite(server,database,table_name,n, db_file_path,project_name,username,password,schema):
    
    # print("Import_SqlServer_Data_To_SqLite Function")
    n=int(n)
    # SQL Server connection parameters
    sql_server_params = {
            'server': server,
            'database': database,
            'username': username,
            'password': password
            #'trusted_connection': 'yes'  # Use Windows authentication
        }
    #Connect to SQL Server
    conn_sql_server = pyodbc.connect(
            f"DRIVER=ODBC Driver 17 for SQL Server;"
            f"SERVER={sql_server_params['server']};"
            f"DATABASE={sql_server_params['database']};"
            f"UID={sql_server_params['username']};"
            f"PWD={sql_server_params['password']}"
        )
    try:
         
        # SQL query to select data from SQL Server table
        if schema:
            sql_query = f'''
            SELECT TOP {n}  * 
            FROM {database}.{schema}.{table_name}
            ORDER BY NEWID()
            '''
        else:
            sql_query = f'''
            SELECT TOP {n} * 
            FROM {database}..{table_name}
            ORDER BY NEWID()
            '''
        
        # Read data from SQL Server into a DataFrame
        df = pd.read_sql(sql_query, conn_sql_server)

        df.replace('', pd.NA, inplace=True)

        # Handle nulls and convert data types
        for column in df.columns:
            if df[column].dtype == 'object':  # Handle object (string) columns
                df[column].fillna('NA', inplace=True)  # Fill nulls with 'NA' for string columns
            elif df[column].dtype == 'int64':  # Handle integer columns
                df[column].fillna(0, inplace=True)  # Fill nulls with 0 for integer columns
            elif df[column].dtype == 'float64':  # Handle float columns
                df[column].fillna(0.0, inplace=True)  # Fill nulls with 0.0 for float columns
                # Convert float columns to integer if all values are integers
                if df[column].apply(lambda x: x.is_integer()).all():
                    df[column] = df[column].astype(int)
            elif df[column].dtype == 'datetime64':  # Handle datetime columns
                df[column].fillna(pd.to_datetime('1900-01-01'), inplace=True)  # Fill nulls with a default date
            # Add more conditions for handling other data types as needed
        print(df)

        mf.Df_Data_To_Sqlite(db_file_path,df,table_name)

        Comment = f"Successfully imported Table : '{table_name}' inside the Project : '{project_name}'"
        Status = "Success"
        # print(Comment,Status)
        return Status,Comment

    except Exception as e:
        Status = "Failed"
        comment = f"Import '{table_name}' Failed with one or more data exception : {e}"
        return Status,comment
        # print(f"An error {e} occured while getting the data from sqlserver")
    finally:
        conn_sql_server.close()

#-------------------------------------------------------------------------------------------------------
    
#Function for importing the CSV data to the DataFrame

def Import_CSV_Data_To_SqLite(db_file_path,path,n,table_name,table_name_folder_path,delimeter,quotechar):
    # print("SqLite Function")
    n=int(n)
    try:
        # Get the total number of rows in the CSV file
        total_rows = sum(1 for _ in open(path))

        # Calculate the rows to skip
        skip_rows = sorted(random.sample(range(1, total_rows + 1), total_rows - n))
        
        if(delimeter==","):
            delimeter = ","

        if(delimeter==";"):
            delimeter = ";"

        if(quotechar=="\""):
            quotechar = "\""
        
        if(quotechar=="\'"):
            quotechar = "\'"
        
        # Read the sample of the CSV file
        df = pd.read_csv(path, skiprows=skip_rows,delimiter=delimeter,quotechar=quotechar)
       
        df.replace('    ', pd.NA, inplace=True)

        # Handle nulls and convert data types
        for column in df.columns:
            
            if df[column].dtype == 'object':  # Handle object (string) columns
                df[column].fillna('NA', inplace=True)  # Fill nulls with 'NA' for string columns

            elif df[column].dtype == 'int64':  # Handle integer columns
                df[column].fillna(0, inplace=True)  # Fill nulls with 0 for integer columns

            elif df[column].dtype == 'float64':  # Handle float columns
                df[column].fillna(0.0, inplace=True)  # Fill nulls with 0.0 for float columns
                # Convert float columns to integer if all values are integers
                if df[column].apply(lambda x: x.is_integer()).all():
                    df[column] = df[column].astype(int)

            elif df[column].dtype == 'datetime64':  # Handle datetime columns
                df[column].fillna(pd.to_datetime('1900-01-01'), inplace=True)  # Fill nulls with a default date
        
            # Add more conditions for handling other data types as needed
        
                
        response=mf.Df_Data_To_Sqlite(db_file_path,df,table_name)
        if response!="success":
            
            return response
        
        # print(df)
        # Comment = f"Successfully imported file : {path} as a table : {table_name} inside the project : {project_name}"
        mf.create_path(table_name_folder_path)

        config_files_path=table_name_folder_path+'\\ConfigFile'
        mf.create_path(config_files_path)

        log_files_path=table_name_folder_path+"\\LogFile"
        mf.create_path(log_files_path)
        Status = "success"
        # print(Comment,Status)
        return Status

    except Exception as e:
        Status = "Import Failed"
        # Comment = f"Import failed : {db_file_path} Failed with one or more data exception : {e}"
        return Status+f"{e}"
#-------------------------------------------------------------------------------------------------------------


def Import_CSV_Relational_Data_To_SqLite(relationshipsList,csv_path,db_file_path,n,table_name,project_name,delimeter,quotechar):

    try:
        if(delimeter==","):
            delimeter = ","

        if(delimeter==";"):
            delimeter = ";"

        if(quotechar=="\""):
            quotechar = "\""
        
        if(quotechar=="\'"):
            quotechar = "\'"

        df = pd.read_csv(csv_path,delimiter=delimeter,quotechar=quotechar)

        df.replace('', pd.NA, inplace=True)



        # Handle nulls and convert data types
        for column in df.columns:
            if df[column].dtype == 'object':  # Handle object (string) columns
                df[column].fillna('NA', inplace=True)  # Fill nulls with 'NA' for string columns
            elif df[column].dtype == 'int64':  # Handle integer columns
                df[column].fillna(0, inplace=True)  # Fill nulls with 0 for integer columns
            elif df[column].dtype == 'float64':  # Handle float columns
                df[column].fillna(0.0, inplace=True)  # Fill nulls with 0.0 for float columns
                # Convert float columns to integer if all values are integers
                if df[column].apply(lambda x: x.is_integer()).all():
                    df[column] = df[column].astype(int)
            elif df[column].dtype == 'datetime64':  # Handle datetime columns
                df[column].fillna(pd.to_datetime('1900-01-01'), inplace=True)  # Fill nulls with a default date
            # Add more conditions for handling other data types as needed

        mf.Df_Data_To_Sqlite(db_file_path,df,table_name)
    except Exception as e:
        print(f"Error {e}")

    
    # Set initial join query
    join_query = f"""SELECT "{table_name}".* FROM "{table_name}" """
   
    # Construct JOIN conditions dynamically
    join_conditions = ""
    # relationshipsList = [
    #     {'Existing_Table': 'projects_df', 'Existing_Column': 'ProjectID', 'Source_Table': 'tasks_df', 'Source_Column': 'ProjectID'},
    #     {'Existing_Table': 'departments_df', 'Existing_Column': 'DepartmentID', 'Source_Table': 'projects_df', 'Source_Column': 'DepartmentID'}
    # ]

    for relationship in relationshipsList:
        existing_table = relationship['Existing_Table']
        existing_column = relationship['Existing_Column']
        source_table = relationship['Source_Table']
        current_column = relationship['Source_Column']

        join_conditions += f""" INNER JOIN "{existing_table}" ON "{source_table}".{current_column} = "{existing_table}".{existing_column}"""
        #print(join_conditions)
    # Append the JOIN conditions to the query
    join_query += join_conditions
    
    # print(join_query)

    try:
        # Establish a connection to the SQLite database
        conn = sqlite3.connect(db_file_path)

        # Read the table into a DataFrame
        df = pd.read_sql_query(join_query, conn)

        # Print the DataFrame
        # print(df)

        n = int(n)  # Number of rows to sample

        # Sample n rows from the DataFrame
        sampled_df = df.sample(n)

        # Print the sampled DataFrame
        print(sampled_df)

        mf.Df_Data_To_Sqlite(db_file_path,df,table_name)

        Comment = f"Successfully imported Table : '{table_name}' inside the Project : '{project_name}'"
        Status = "Success"
        # print(Comment,Status)
        return Status,Comment

    except sqlite3.Error as e:
        Status = "Failed"
        comment = f"SQLite error occurred:{e}"
        return Status,comment
        # print("SQLite error occurred:", e)
    finally:
        conn.close()


def Import_SqlServer_Relational_Data_To_SqLite(relationshipsList,server,database,table_name,n,db_file_path,project_name,username,password,schema):
    # print("Import_SqlServer_Relational_Data_To_SqLite Function")
     # SQL Server connection parameters
    sql_server_params = {
            'server': server,
            'username': username,
            'password': password,
            'database': database 
        }

    #Connect to SQL Server
    conn_sql_server = pyodbc.connect(
            f"DRIVER=ODBC Driver 17 for SQL Server;"
            f"SERVER={sql_server_params['server']};"
            f"DATABASE={sql_server_params['database']};"
            f"UID={sql_server_params['username']};"
            f"PWD={sql_server_params['password']}"
        )
    
    try:
        # SQL query to select data from SQL Server table
        if schema:
            sql_query = f'''
            SELECT TOP {n} * 
            FROM {database}.{schema}.{table_name}
            ORDER BY NEWID()
            '''
        else:
            sql_query = f'''
            SELECT TOP {n} * 
            FROM {database}..{table_name}
            ORDER BY NEWID()
            '''

        # Read data from SQL Server into a DataFrame
        df = pd.read_sql(sql_query, conn_sql_server)
        # print(df)
        df.replace('', pd.NA, inplace=True)

        # Handle nulls and convert data types
        for column in df.columns:
            if df[column].dtype == 'object':  # Handle object (string) columns
                df[column].fillna('NA', inplace=True)  # Fill nulls with 'NA' for string columns
            elif df[column].dtype == 'int64':  # Handle integer columns
                df[column].fillna(0, inplace=True)  # Fill nulls with 0 for integer columns
            elif df[column].dtype == 'float64':  # Handle float columns
                df[column].fillna(0.0, inplace=True)  # Fill nulls with 0.0 for float columns
                # Convert float columns to integer if all values are integers
                if df[column].apply(lambda x: x.is_integer()).all():
                    df[column] = df[column].astype(int)
            elif df[column].dtype == 'datetime64':  # Handle datetime columns
                df[column].fillna(pd.to_datetime('1900-01-01'), inplace=True)  # Fill nulls with a default date
            # Add more conditions for handling other data types as needed
        mf.Df_Data_To_Sqlite(db_file_path,df,table_name)

        join_query = f"""SELECT "{table_name}".* FROM "{table_name}" """

        # Construct JOIN conditions dynamically
        join_conditions = ''
        existing_table = ''
        existing_column = ''
        current_column = ''

        # Sample relationships (replace with actual relationships)
        # relationshipsList = [
        #     {'Existing_Table': 'Projects', 'Existing_Column': 'ProjectID', 'Source_Table': 'Tasks', 'Source_Column': 'ProjectID'},
        #     {'Existing_Table': 'Departments', 'Existing_Column': 'DepartmentID', 'Source_Table': 'Projects', 'Source_Column': 'DepartmentID'}
        # ]

        for relationship in relationshipsList:
            existing_table = relationship['Existing_Table']
            existing_column = relationship['Existing_Column']
            current_column = relationship['Source_Column']
            source_table = relationship['Source_Table']

            join_conditions += f""" INNER JOIN "{existing_table}" ON "{source_table}".{current_column} = "{existing_table}".{existing_column}"""

        # Append the JOIN conditions to the query
        join_query += join_conditions
        # Print the final query
        # print(join_query)

        cursor = conn_sql_server.cursor()
        cursor.execute(join_query)
        # Fetch all rows
        rows = cursor.fetchall()

        # Convert fetched data to DataFrame
        df = pd.DataFrame([tuple(row) for row in rows], columns=[desc[0] for desc in cursor.description])
        # print(df)
        n = int(n)  # Number of rows to sample

        # Sample n rows from the DataFrame
        sampled_df = df.sample(n)

        # Print the sampled DataFrame
        print(sampled_df)

        mf.Df_Data_To_Sqlite(db_file_path,sampled_df,table_name)

        Comment = f"Successfully imported Table : '{table_name}' inside the Project : '{project_name}'"
        Status = "Success"
        # print(Comment,Status)
        return Status,Comment
    
    except Exception as e:
        Status = "Failed"
        comment = f"An error {e} occured while getting the data from sqlserver"
        return Status,comment
        # print(f"An error {e} occured while getting the data from sqlserver")

    finally:
            conn_sql_server.close()


        
