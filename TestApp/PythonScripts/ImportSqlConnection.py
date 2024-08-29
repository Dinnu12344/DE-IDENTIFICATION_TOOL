import De_Identification as de
import Export as ex
import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd
import Import as i
import sys
import pyodbc

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

def generate_name(filePath, projectName, logFile):
    # Combine the parameters to generate a name
    name = f"Name generated from parameters: {filePath}, {projectName}, {logFile}"
    return name

def main():
    try:
        # Extract arguments passed from the command line
        server_name = sys.argv[1]
        database_name = sys.argv[2]
        sql_server_password = sys.argv[3]
        sql_server_username = sys.argv[4]
        project_name = sys.argv[5]
        n = sys.argv[6]
        table_name = sys.argv[7]
        schema_name = sys.argv[8]

        print(schema_name)

        username = getpass.getuser()
        tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
        mf.create_path(tool_path)

        project_path = os.path.join(tool_path, project_name)
        mf.create_path(project_path)

        tables_data_path = os.path.join(tool_path, project_name, 'TablesData')
        mf.create_path(tables_data_path)

        db_file_path = os.path.join(tables_data_path, 'Data.db')
        table_name_folder_path = os.path.join(project_path, table_name)

        run_start = datetime.datetime.now()

        log_files_path = os.path.join(tool_path, project_name)
        log_files_path_table = os.path.join(log_files_path, table_name, "LogFile")
        mf.create_path(log_files_path_table)

        log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + ".log"
        filename = os.path.join(log_files_path_table, log_filename)

        # Check if the table has at least one row in SQL Server
        rows_count = get_table_row_count(server_name, database_name, table_name, schema_name, sql_server_username, sql_server_password)
        if rows_count == 0:
            raise ValueError(f"The table '{schema_name}.{table_name}' is empty and cannot be imported.")

        # Proceed with import only if the SQL table has rows
        if not mf.check_table_existence(table_name, db_file_path):
            Status, Comment = i.Import_SqlServer_Data_To_SqLite(server_name, database_name, table_name, n, db_file_path, project_name, sql_server_username, sql_server_password, schema_name)
            run_end = datetime.datetime.now()
            run_time = run_end - run_start
            mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_start, run_end=run_end, status=Status, duration=run_time, comment=Comment)
            print(Status)
        else:
            Status, Comment = i.Import_SqlServer_Data_To_SqLite(server_name, database_name, table_name, n, db_file_path, project_name, sql_server_username, sql_server_password, schema_name)
            run_end = datetime.datetime.now()
            run_time = run_end - run_start
            mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_start, run_end=run_end, status=Status, duration=run_time, comment=Comment)
            print(Comment)

    except Exception as e:
        run_end = datetime.datetime.now()
        run_time = run_end - run_start
        error_message = f"Error: {str(e)}"
        mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_start, run_end=run_end, status="Failure", duration=run_time, comment=error_message)
        print(error_message)

if __name__ == "__main__":
    main()
