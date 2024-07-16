﻿import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd
import Import as i
import sys

def generate_name(filePath, projectName, logFile):
    # Combine the parameters to generate a name
    name = f"Name generated from parameters: {filePath}, {projectName}, {logFile}"
    return name

def main():
    run_starts = datetime.datetime.now()  # Initialize run_starts
    log_files_path_table = ""
    filename = ""

    try:
        # Extract arguments passed from command line
        filePath = sys.argv[1]
        project_name = sys.argv[2]
        table_name = sys.argv[3]
        n = sys.argv[4]
        
       

        username = getpass.getuser()
        tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
        mf.create_path(tool_path)

        project_path = os.path.join(tool_path, project_name)
        mf.create_path(project_path)

        tables_data_path = os.path.join(tool_path, project_name, 'TablesData')
        mf.create_path(tables_data_path)

        db_file_path = os.path.join(tables_data_path, 'Data.db')

        table_name_folder_path = os.path.join(project_path, table_name)
        
        run_starts = datetime.datetime.now()
        log_files_path = os.path.join(tool_path, project_name)	

        log_files_path_table = os.path.join(log_files_path, table_name, "LogFile")
        mf.create_path(log_files_path_table)
        
        log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + ".log"
        filename = os.path.join(log_files_path_table, log_filename)

        if not mf.check_table_existence(table_name, db_file_path):    
            Status, Comment = i.Import_JSON_Data_To_SqLite(db_file_path, filePath, n, table_name, project_name)
            run_end = datetime.datetime.now()
            run_time = run_end - run_starts
    
            mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_starts, run_end=run_end, status=Status, duration=run_time, comment=Comment)

            print(Status) 
        else:
            Status, Comment = i.Import_JSON_Data_To_SqLite(db_file_path, filePath, n, table_name, project_name)


            run_end = datetime.datetime.now()
            run_time = run_end - run_starts
    
            mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_starts, run_end=run_end, status=Status, duration=run_time, comment=Comment)

            # Generate name based on parameters
            # name = generate_name(filePath, project_name, logFile)
            print(Comment)  # Output the status

    except Exception as e:
        run_end = datetime.datetime.now()
        run_time = run_end - run_starts

        error_message = f"Error: {str(e)}"
        mf.append_logs_to_file(file_path=filename, job_name="Import", run_start=run_starts, run_end=run_end, status="Failure", duration=run_time, comment=error_message)
        
        # print("Failure")
        print(error_message)

if __name__ == "__main__":
    main()
