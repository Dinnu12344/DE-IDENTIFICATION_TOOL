import pyodbc
import pandas as pd
import sqlite3
import os
import re
import json
import Miscellaneous_Functions as mf


def convert_to_json_format(input_string):
    # Step 1: Replace the opening and closing square brackets with double quotes
    input_string = input_string.replace('[{', '[{"').replace('}]', '"}]')
    
    # Step 2: Replace colons following keys with double-quoted keys
    input_string = input_string.replace(':', '":"')
    
    # Step 3: Replace commas between key-value pairs with double-quoted values and commas
    input_string = input_string.replace(',', '","')
    
    # Step 4: Correct the formatting by replacing "" with "
    input_string = input_string.replace('""', '"')
    
    return input_string



def keep_n_rows_in_table(db_path, table_name, n):
    try:
        # Connect to the SQLite database
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()
        
        # Ensure n is not greater than the number of rows in the table
        cursor.execute(f"SELECT COUNT(*) FROM {table_name}")
        total_rows = cursor.fetchone()[0]
        if n >= total_rows:
            #print(f"Table {table_name} already has {total_rows} rows, which is less than or equal to {n}. No rows will be deleted.")
            return
        
        # Select the IDs of the first n rows to keep
        cursor.execute(f"SELECT rowid FROM {table_name} ORDER BY rowid LIMIT {n}")
        rows_to_keep = [row[0] for row in cursor.fetchall()]
        
        # Convert list of IDs to a comma-separated string
        ids_to_keep = ', '.join(map(str, rows_to_keep))
        
        # Delete rows that are not in the list of IDs to keep
        cursor.execute(f"DELETE FROM {table_name} WHERE rowid NOT IN ({ids_to_keep})")
        df2 = pd.read_sql_query(f"select * from {table_name}", conn)
        print(df2)
        # Commit the changes and close the connection
        conn.commit()
        conn.close()
        
        #print(f"Table {table_name} now contains only the first {n} rows.")
    
    except Exception as e:
        print(f"An error occurred: {e}")






def fetch_data_from_sql_server(server, database, table_name, batch_size, offset, username, password):
    conn_sql_server = pyodbc.connect(
        f"DRIVER=ODBC Driver 17 for SQL Server;"
        f"SERVER={server};"
        f"DATABASE={database};"
        f"UID={username};"
        f"PWD={password};"
    )
 
    query = f"""
    SELECT * FROM {table_name}
    ORDER BY (SELECT NULL)
    OFFSET {offset} ROWS FETCH NEXT {batch_size} ROWS ONLY
    """
    df = pd.read_sql(query, conn_sql_server)
    conn_sql_server.close()
    return df
 
def insert_data_into_sqlite(db_file_path, df, temp_table_name):
    #print("insert_data_into_sqlite function")
    try:
        conn_sqlite = sqlite3.connect(db_file_path)
        df.to_sql(temp_table_name, conn_sqlite, if_exists='append', index=False)
        
        #print("successfully inserted data",temp_table_name)
        #print(df)
        return "success"
    except Exception as e:
        print(e)
    finally:
        conn_sqlite.close()
 
def process_batches(server, database, table_name, batch_size, db_file_path, username, password, relationshipsList,rowCount):
    offset = 0
    batch_count = 0
    batchRowCount=0
    #relationshipsList = correct_json_format(relationshipsList)
    if isinstance(relationshipsList, str):
        relationshipsList = json.loads(relationshipsList)

# Verify the structure of relationshipsList
    #print(type(relationshipsList))
    #print(relationshipsList)

# Parse the JSON string into a Python object (list of dictionaries)


    sqlite_table_name = table_name.split('.')[1]
    while batchRowCount<rowCount:
        
        df = fetch_data_from_sql_server(server, database, table_name, batch_size, offset, username, password)
        batchRowCount+=len(df)
        #print(server, database, table_name, batch_size, offset, username, password)
        if df.empty:
            print("df is empty")
            break
        print(df)
        
        res=insert_data_into_sqlite(db_file_path, df, sqlite_table_name)
        if(res!="success"):
            return "Failed inserting the data into sqlite"
       
        # Perform joins in SQLite
        conn_sqlite = sqlite3.connect(db_file_path)
       
        #df = pd.read_sql_query(f"select * from {sqlite_table_name}", conn_sqlite)
        #print("inserted data",df)
        join_query = f"""SELECT {sqlite_table_name}.* FROM {sqlite_table_name} """
        #print(relationshipsList)
        for relationship in relationshipsList:
            if isinstance(relationship, dict):
                existing_table = relationship["ExistingTable"]
                existing_column = relationship["ExistingColumn"]
                source_column = relationship["SourceColumn"]
                source_table = relationship["SourceTable"]
 
                join_query += f""" INNER JOIN {existing_table}
                ON {source_table}.{source_column} = {existing_table}.{existing_column}
                """
        print(join_query)
        conn_sqlite.execute(join_query)
        df2 = pd.read_sql_query(f"select * from {sqlite_table_name}", conn_sqlite)
        print(df2)
        conn_sqlite.commit()
        conn_sqlite.close()
       
              
        batch_count += 1
        offset += batch_size


        if len(df) < batch_size:

            break

    keep_n_rows_in_table(db_file_path,sqlite_table_name,rowCount)
    

    return "success"
 
    #print(f"Processed {batch_count} batches and inserted data into {table_name}")
import sys 
if __name__ == "__main__":
    # Define parameters
    project_name=sys.argv[1]
    server=sys.argv[2]
    database=sys.argv[3]
    username=sys.argv[4]
    password=sys.argv[5]
    table_name=sys.argv[6]
    relationshipsList=sys.argv[7]
    rowCount=sys.argv[8]

    #print(relationshipsList)
    relationshipsList = convert_to_json_format(relationshipsList)
    relationshipsList = json.loads(relationshipsList)
    relationshipsList = json.dumps(relationshipsList, indent=4)
    #print("After")
    #print(relationshipsList)
    #print("end")

  
    rowCount=int(rowCount)
    batch_size = 10000

    db_file_path=mf.tool_path+'\\'+project_name+'\\TablesData'+'\\Data.db'
    #print(db_file_path)
    res=process_batches(server, database, table_name,batch_size, db_file_path, username, password, relationshipsList,rowCount)
    print(res)