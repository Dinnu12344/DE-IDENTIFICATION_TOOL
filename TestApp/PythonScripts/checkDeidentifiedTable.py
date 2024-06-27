import Miscellaneous_Functions as mf
import getpass
import os
import sys


def main():
    try:
        # Extract arguments passed from command line
        
        project_name = sys.argv[1]
        table_name = sys.argv[2]

        #print(f"project is {project_name} and table is {table_name}")
        username = getpass.getuser()
        tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
        
        tables_data_path = tool_path+'\\'+project_name+'\\TablesData'
       
        db_file_path = os.path.join(tables_data_path, 'Data.db')
        #print(db_file_path)
        res=mf.check_table_existence('de_identified_'+table_name, db_file_path)
        if res=="True":
            #print("if block")
            print( "True")
        else:
            print(res)

    except Exception as e:
        print( f"Error {e} has occured")
           


if __name__ == "__main__":
    main()