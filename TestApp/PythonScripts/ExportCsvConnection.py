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
    # Extract arguments passed from command line
    run_start = datetime.datetime.now()

    saveFilePath=sys.argv[1]
    project_name = sys.argv[2]
    table_name=sys.argv[3]
    # print(saveFilePath)
    # print(project_name)
    # print(table_name)

    # print(f"ExportCsv Connection {saveFilePath} {project_name} {table_name} ")
    db_file_path=mf.tool_path+'\\'+project_name+'\\TablesData'+'\\Data.db'
    # print(f"db_file_path is : {db_file_path}")
    

    log_files_path=mf.tool_path+'\\'+project_name
    
    log_files_path_table = os.path.join(log_files_path, table_name, "LogFile")
    mf.create_path(log_files_path_table)
    
    log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + ".log"
    filename = os.path.join(log_files_path_table, log_filename)
    
    res=mf.check_table_existence("de_identified_"+table_name,db_file_path)
    if(res==True):
        print(res)
        Status,Comment=ex.export_to_csv(table_name,db_file_path,saveFilePath)
        run_end = datetime.datetime.now()
        run_time = run_end - run_start
    
        mf.append_logs_to_file(file_path = filename,job_name="Export", run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)
    
        print(Status)
    else:
        
        Status = "Failure"
        Comment = "Export failed with one or more exceptions"
        run_end = datetime.datetime.now()
        run_time = run_end - run_start
        mf.append_logs_to_file(file_path = filename,job_name="Export", run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)
        print(res)

