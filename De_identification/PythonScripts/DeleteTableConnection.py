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
import sys


if __name__ == "__main__":
    # Extract arguments passed from command line
    project_name = sys.argv[1]
    table_name=sys.argv[2]
    

    db_file_path=mf.tool_path+"\\"+project_name+"\\TablesData\\Data.db"
    if(mf.check_table_existence(table_name,db_file_path)==True):
            d.delete_folders(mf.tool_path+"\\"+project_name+"\\"+table_name)
            d.delete_tables(db_file_path,table_name)
            print("success")
    else:
        print(f"The table {table_name} doest exist in sqlite")