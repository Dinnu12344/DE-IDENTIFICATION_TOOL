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
            return "File not present"
    except Exception as e:
        return f"An error occurred: {e}"
    

if __name__ == "__main__":
    # Extract arguments passed from command line
    import sys
    project_name = sys.argv[1]
    table_name=sys.argv[2]
    
    
    db_file_path=mf.tool_path+'\\'+project_name+'\\TablesData'+'\\Data.db'

    config_file_path=mf.tool_path+'\\'+project_name+'\\'+table_name+'\\ConfigFile'+'\\'+table_name+'.json'

    result = check_json_file_presence(config_file_path)

    if result=='File present':
        response=de.de_Identification_Main(config_file_path,table_name,db_file_path)
        print(response)
    else:
        print(result)
