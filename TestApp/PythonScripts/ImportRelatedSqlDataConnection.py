import pyodbc
import pandas as pd
import sqlite3
import os
import json
import Miscellaneous_Functions as mf
import datetime
import sys


import json

def validate_relationships_list(relationshipsList):
    required_keys = ["ExistingTable", "ExistingColumn", "SourceTable", "SourceColumn"]

    try:
        # Load the JSON string into a Python list of dictionaries
        relationshipsList = json.loads(relationshipsList)
        #print("Parsed relationships list:", relationshipsList)

        # Ensure it's a list
        if not isinstance(relationshipsList, list):
            raise ValueError("The relationshipsList should be a list.")

        # Check if all required keys are present and have non-empty values
        for relationship in relationshipsList:
            if not isinstance(relationship, dict):
                raise ValueError("Each relationship entry should be a dictionary.")
            for key in required_keys:
                if key not in relationship or not relationship[key]:
                    raise ValueError(f"Missing or empty value for key '{key}' in relationship: {relationship}")

    except json.JSONDecodeError as e:
        print("Invalid JSON format:", e)
        return "Failed", "Invalid JSON format."
    except ValueError as ve:
        print(str(ve))
        return "Failed", str(ve)
    except Exception as e:
        print("validate_relationships_list Relations fields are not filled properly:", e)
        return "Failed", "Relations fields are not filled properly."

    return "Success", "All relationships are valid."




def get_table_row_count(server_name, database_name, table_name, schema_name, username, password):
    conn_str = (
        f"DRIVER={{SQL Server}};"
        f"SERVER={server_name};"
        f"DATABASE={database_name};"
        f"UID={username};"
        f"PWD={password};"
    )
    conn = pyodbc.connect(conn_str)
    cursor = conn.cursor()

    query = f"SELECT COUNT(*) FROM {schema_name}.{table_name}"
    cursor.execute(query)
    row_count = cursor.fetchone()[0]

    cursor.close()
    conn.close()
    return row_count


def convert_to_json_format(input_string):
    input_string = input_string.replace('[{', '[{"').replace('}]', '"}]')
    input_string = input_string.replace(':', '":"')
    input_string = input_string.replace(',', '","')
    input_string = input_string.replace('""', '"')
    
    return input_string


def keep_n_rows_in_table(db_path, table_name, n):
    try:
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()

        cursor.execute(f"SELECT COUNT(*) FROM {table_name}")
        total_rows = cursor.fetchone()[0]
        if n >= total_rows:
            return

        cursor.execute(f"SELECT rowid FROM {table_name} ORDER BY rowid LIMIT {n}")
        rows_to_keep = [row[0] for row in cursor.fetchall()]
        ids_to_keep = ', '.join(map(str, rows_to_keep))

        cursor.execute(f"DELETE FROM {table_name} WHERE rowid NOT IN ({ids_to_keep})")
        #df2 = pd.read_sql_query(f"SELECT * FROM {table_name}", conn)
        #print(df2)

        conn.commit()
        conn.close()

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


def insert_data_into_sqlite(db_file_path, df, temp_table_name, extr_para):
    try:
        conn_sqlite = sqlite3.connect(db_file_path)
        df.to_sql(temp_table_name, conn_sqlite, if_exists=extr_para, index=False)
        return "success"
    except Exception as e:
        return str(e)
    finally:
        conn_sqlite.close()


def process_batches(server, database, table_name, batch_size, db_file_path, username, password, relationshipsList, rowCount):
    offset = 0
    batchRowCount = 0
    # Example usage
    #relationships_list_str = '[{"Existing Table": "Schools", "Existing Column":"ld", "SourceTable": "Dates","SourceColumn":"column2"}, {"Existing Table": "Schools1", "Existing Column":"ld1", "SourceTable": "Dates1","SourceColumn":"column2"}]'
    

    if isinstance(relationshipsList, str):
        try:
            #relationshipsList = json.loads(relationshipsList)
            #print("Printing relationshipsList ")
            #print(relationshipsList)
            status, message = validate_relationships_list(relationshipsList)
            #print(status, message) 
            if(status!="Success"):
                return status, message
            #print("contineue")
            relationshipsList = json.loads(relationshipsList)

        except Exception as e:
            print("Relations fields are not filled properly.")
            return "Failed","Relations fields are not filled properly."

    #sqlite_table_name = table_name.split('.')[1]
    
    
    while batchRowCount < rowCount:
        df = fetch_data_from_sql_server(server, database, table_name, batch_size, offset, username, password)
        batchRowCount += len(df)
        if df.empty:
            #print("Table is empty")
            break
        #print(df)

        res = insert_data_into_sqlite(db_file_path, df, table_name, 'append')
        if res != "success":
            return "Failed inserting the data into SQLite"

        conn_sqlite = sqlite3.connect(db_file_path)
        join_query = f"SELECT {table_name}.* FROM {table_name} "

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
        try:
            df = pd.read_sql_query(join_query, conn_sqlite)
        except Exception:
            print("Relations are not correct!")
            return "Failed","Relations are not correct!"
        if(df.empty!=True):   
            res = insert_data_into_sqlite(db_file_path, df, table_name, 'replace')
            if res != "success":
                
                return "Failed","Failed inserting the data into SQLite"
        else:
            print("There is no relation between tables") 
            return "Failed","Failed to import the table as the there is no relational data!"

        #df2 = pd.read_sql_query(f"SELECT * FROM {table_name}", conn_sqlite)
        #print(df2)

        conn_sqlite.commit()
        conn_sqlite.close()

        offset += batch_size

        if len(df) < batch_size:
            break

    keep_n_rows_in_table(db_file_path, table_name, rowCount)
    
    return "success", "Successfully imported related data"


if __name__ == "__main__":
    project_name = sys.argv[1]
    server = sys.argv[2]
    database = sys.argv[3]
    username = sys.argv[4]
    password = sys.argv[5]
    table_name = sys.argv[6]
    relationshipsList = sys.argv[7]
    rowCount = sys.argv[8]

    relationshipsList = convert_to_json_format(relationshipsList)
    relationshipsList = json.loads(relationshipsList)
    relationshipsList = json.dumps(relationshipsList, indent=4)

    rowCount = int(rowCount)
    batch_size = 10000

    server_name = server
    database_name = database
    schema_name, table_name = table_name.split('.')
    sql_server_username = username
    sql_server_password = password

    db_file_path = os.path.join(mf.tool_path, project_name, 'TablesData', 'Data.db')

    run_start = datetime.datetime.now()
    log_files_path = os.path.join(mf.tool_path, project_name)

    log_files_path_table = os.path.join(log_files_path, table_name, "LogFile")
    mf.create_path(log_files_path_table)

    log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + ".log"
    filename = os.path.join(log_files_path_table, log_filename)

    

    rows_count = get_table_row_count(server_name, database_name, table_name, schema_name, sql_server_username, sql_server_password)
    if rows_count == 0:
        print(f"The table {table_name} is empty and cannot be imported.")
        sys.exit(1)  # Use sys.exit instead of return to exit the script

    Status, Comment = process_batches(server, database, table_name, batch_size, db_file_path, username, password, relationshipsList, rowCount)
    run_end = datetime.datetime.now()
    run_time = run_end - run_start

    mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_start, run_end=run_end, status=Status, duration=run_time, comment=Comment)

    print(Status,Comment )
