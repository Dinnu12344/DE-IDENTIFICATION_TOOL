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

if __name__ == "__main__":

    # Ensure the script has enough arguments
    if len(sys.argv) < 4:
        print("Error: Not enough arguments provided.")
        sys.exit(1)

    # Extract arguments
    saveFilePath = sys.argv[1]  # Capture the saveFilePath
    project_name = sys.argv[2]  # Capture the project_name
    table_name = sys.argv[3]    # Capture the table_name

    run_start = datetime.datetime.now()

    # Construct the database file path
    db_file_path = os.path.join(mf.tool_path, project_name, 'TablesData', 'Data.db')
    
    # Define log files path
    log_files_path = os.path.join(mf.tool_path, project_name)
    log_files_path_table = os.path.join(log_files_path, table_name, "LogFile")
    mf.create_path(log_files_path_table)
    
    # Generate log filename
    log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + ".log"
    filename = os.path.join(log_files_path_table, log_filename)
    
    # Check if the table exists in the SQLite database
    res = mf.check_table_existence("de_identified_" + table_name, db_file_path)
    if res == "True":
        print(res)
        Status, Comment = ex.export_to_csv(table_name, db_file_path, saveFilePath)
        run_end = datetime.datetime.now()
        run_time = run_end - run_start
        
        # Check for permission denied status in the comment
        if "permission denied" in Comment.lower():
            # Send response to front end or log accordingly
            print("Permission Denied: Cannot write to the specified directory.")
            # Here you can include logic to send this error back to the front end if needed.
        
        # Log the operation
        mf.append_logs_to_file(
            file_path=filename,
            job_name="Export",
            run_start=run_start,
            run_end=run_end,
            status=Status,
            duration=run_time,
            comment=Comment
        )
    
        print(Status)
    else:
        Status = "Failure"
        Comment = "Export failed with one or more exceptions"
        run_end = datetime.datetime.now()
        run_time = run_end - run_start
        mf.append_logs_to_file(
            file_path=filename,
            job_name="Export",
            run_start=run_start,
            run_end=run_end,
            status=Status,
            duration=run_time,
            comment=Comment
        )
        print(res)
