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

def get_column_names(db_path, table_name):
    try:
        # Connect to the SQLite database
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()
        
        # Execute a query to retrieve column names
        cursor.execute(f"SELECT * FROM {table_name} LIMIT 0")        
        # Fetch all results
        columns_info = cursor.fetchall()
        
        # Extract column names
        column_names = [description[0] for description in cursor.description]  
        # print(db_file_path)      
        return column_names
    except sqlite3.Error as e:
        return f"An error occurred: {e}"
        
    finally:
        # Close the connection
        if conn:
            conn.close()

# Example usage



if __name__ == "__main__":
    # Extract arguments passed from command line
    import sys
    project_name = sys.argv[1]
    table_name = sys.argv[2]
    

    db_file_path = mf.tool_path+'\\'+project_name+'\\TablesData'+'\\Data.db'

    resp=get_column_names(db_file_path, table_name)

    print(resp)
