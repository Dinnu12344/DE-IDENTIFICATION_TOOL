# Main.py

import De_Identification as de
import Export as ex
import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd
import Import as i


def generate_name(filePath, projectName, logFile):
    # Combine the parameters to generate a name
    name = f"Name generated from parameters: {filePath}, {projectName}, {logFile}"
    return name

# Entry point to execute the script
if __name__ == "__main__":
    # Extract arguments passed from command line
    import sys
    csvfilePath = sys.argv[1]
    project_name = sys.argv[2]
    n=sys.argv[3]
    table_name=sys.argv[4]
    delimeter=sys.argv[5]
    quotechar=sys.argv[6]

    username = getpass.getuser()
    tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
    mf.create_path(tool_path)

    # project_name=input("Give name for the Project : ")
    project_path=tool_path+'\\'+project_name
    mf.create_path(project_path)

    tables_data_path= tool_path+'\\'+project_name+'\\TablesData'
    mf.create_path(tables_data_path)

    db_file_path = tables_data_path+'\\Data.db'


    table_name_folder_path=project_path+'\\'+table_name
    
 
    csv_Im_Respones=i.Import_CSV_Data_To_SqLite(db_file_path,csvfilePath,n,table_name,table_name_folder_path,delimeter,quotechar)

    # Generate name based on parameters
    # name = generate_name(filePath, project_name)
    print(csv_Im_Respones)  # Output the name
