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
    saveFilePath=sys.argv[1]
    project_name = sys.argv[2]
    table_name=sys.argv[3]
    print(saveFilePath)
    print(project_name)
    print(table_name)

    print(f"ExportCsv Connection {saveFilePath} {project_name} {table_name} ")
    db_file_path=mf.tool_path+'\\'+project_name+'\\TablesData'+'\\Data.db'
    print(f"db_file_path is : {db_file_path}")
    res=mf.check_table_existence("de_identified_"+table_name,db_file_path)
    if(res=="Table is Present"):
        print(res)
        response=ex.export_to_csv(table_name,db_file_path,saveFilePath)
        print(response)
    else:
        print(res)

