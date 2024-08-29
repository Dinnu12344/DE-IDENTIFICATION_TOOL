import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd
import Import as i
import sys

def validate_csv(file_path, delimiter):
    try:
        # Try to read the CSV with the provided delimiter
        df = pd.read_csv(file_path, delimiter=delimiter)
        # Check if the DataFrame is not empty and has columns
        if df.empty or df.columns.size == 1:
            return False, "CSV file is empty or delimiter is incorrect."
        return True, df
    except Exception as e:
        return False, str(e)

def main():
    try:
        # Extract arguments passed from command line
        csvfilePath = sys.argv[1]
        project_name = sys.argv[2]
        n = sys.argv[3]
        table_name = sys.argv[4]
        delimiter = sys.argv[5]
        quotechar = sys.argv[6]

        # Validate the CSV file with the given delimiter
        is_valid, result = validate_csv(csvfilePath, delimiter)
        if not is_valid:
            print(result)
            return

        df = result

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

        if not mf.check_table_existence(table_name, db_file_path):
            Status, Comment = i.Import_CSV_Data_To_SqLite(db_file_path, csvfilePath, n, table_name, table_name_folder_path, delimiter, quotechar, project_name)
            run_end = datetime.datetime.now()
            run_time = run_end - run_start
    
            mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_start, run_end=run_end, status=Status, duration=run_time, comment=Comment)

            print(Status) 
        else:
            Status, Comment = i.Import_CSV_Data_To_SqLite(db_file_path, csvfilePath, n, table_name, table_name_folder_path, delimiter, quotechar, project_name)

            run_end = datetime.datetime.now()
            run_time = run_end - run_start
    
            mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_start, run_end=run_end, status=Status, duration=run_time, comment=Comment)

            print(Comment)  # Output the status

    except Exception as e:
        run_end = datetime.datetime.now()
        run_time = run_end - run_start

        error_message = f"Error: {str(e)}"
        mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_start, run_end=run_end, status="Failure", duration=run_time, comment=error_message)

        print(error_message)

if __name__ == "__main__":
    main()
