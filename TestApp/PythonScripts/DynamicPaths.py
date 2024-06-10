import pdb
import Import as i
import Delete as d
import De_Identification as de
import Export as ex
import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd


# def demo():
    
#     username = getpass.getuser()
#     # print("Username:", username)

#     # Specify the path where you want to create the folder
#     tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
#     # print(mf.create_path(tool_path))


#     project_path=tool_path+'\\'+project_name
#     print(mf.create_path(project_path))

#     tables_data_path= tool_path+'\\'+project_name+'\\TablesData'
#     print(mf.create_path(tables_data_path))

#     config_files_path=tool_path+'\\'+project_name+'\\ConfigFiles'
#     print(mf.create_path(config_files_path))

#     log_files_path=tool_path+'\\'+project_name+'\\'+"LogFiles"
#     print(mf.create_path(log_files_path))

#     db_file_path = tables_data_path+'\\Data.db'
