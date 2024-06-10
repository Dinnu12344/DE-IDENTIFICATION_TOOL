import json
import Miscellaneous_Functions as mf
import sqlite3

import De_Identification as de
import Export as ex
import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd
import Import as i
from tabulate import tabulate

if __name__ == "__main__":
    # Extract arguments passed from command line
    import sys
    project_name = sys.argv[1]
    table_name = sys.argv[2]

    db_file_path=mf.tool_path+"\\"+project_name+"\\TablesData\\Data.db"
    if(mf.check_table_existence("de_identified_"+table_name,db_file_path)==True):
        pd.set_option('display.max_rows', None)
        pd.set_option('display.max_columns', None)
        respone=mf.SqlLite_Data_To_Df(db_file_path,f'de_identified_{table_name}')
        print(tabulate(respone, headers='keys', tablefmt='plain', showindex=False))
    else:
        print(f"The table {table_name} doest exist in sqlite")

    
