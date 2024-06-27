import De_Identification as de
import Export as ex
import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd
import Import as i


import os

def check_json_file_presence(file_path):
    try:
        # Check if the specified path points to an existing file
        if os.path.isfile(file_path):
            return "File present"
        else:
            return "config is not done or key is not given"
    except Exception as e:
        return f"An error occurred: {e}"


    
    

if __name__ == "__main__":
    # Extract arguments passed from command line
    import sys
    project_name = sys.argv[1]
    table_name=sys.argv[2]
    
    
    db_file_path=mf.tool_path+'\\'+project_name+'\\TablesData'+'\\Data.db'

    config_file_path=mf.tool_path+'\\'+project_name+'\\'+table_name+'\\ConfigFile'+'\\'+table_name+'.json'
    log_files_path=mf.tool_path+'\\'+project_name	
    
    run_start = datetime.datetime.now()

    log_files_path_table = os.path.join(log_files_path, table_name, "LogFile")
    mf.create_path(log_files_path_table)
    
    log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + ".log"
    filename = os.path.join(log_files_path_table, log_filename)

    result1 = check_json_file_presence(config_file_path)
    keys_path=mf.tool_path+"\\"+project_name+"\\keys.txt"
    result2 = check_json_file_presence(keys_path)
    if result1=='File present' and result2=='File present':
        Status,Comment=de.de_Identification_Main(config_file_path,table_name,db_file_path,keys_path)
        run_end = datetime.datetime.now()
        run_time = run_end - run_start
        mf.append_logs_to_file(file_path = filename,job_name="De-identify", run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)
        print(Status)
    else:
        Status = "Failure"
        Comment = "Failed with one or more exceptions"
        run_end = datetime.datetime.now()
        run_time = run_end - run_start
        mf.append_logs_to_file(file_path = filename,job_name="De-Identify", run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)
       
        print(result1)
