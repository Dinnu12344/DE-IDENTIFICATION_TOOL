# import json
# import Miscellaneous_Functions as mf
# import sqlite3
# import sys
# import De_Identification as de
# import Export as ex
# import sqlite3
# import getpass
# import os
# import Miscellaneous_Functions as mf
# import datetime
# import pandas as pd
# import Import as i
# from IPython.display import display
# from tabulate import tabulate

# if __name__ == "__main__":
#     # Extract arguments passed from command line
#     project_name = sys.argv[1]
#     table_name = sys.argv[2]
#     db_file_path=mf.tool_path+"\\"+project_name+"\\TablesData\\Data.db"
#     if(mf.check_table_existence(table_name,db_file_path)==True):
#         pd.set_option('display.max_rows', None)
#         pd.set_option('display.max_columns', None)
#         respone=mf.SqlLite_Data_To_Df(db_file_path,table_name)
        
#         # print(respone.to_string())
#         result =    tabulate(respone, headers='keys', tablefmt='simple', showindex=False)
#         display(result)
#         # res = pd.DataFrame(result)
#         # print(result)
#         # print(respone)
#         # respone.show()
#         # print("success")
#     else:
#         print(f"The table {table_name} doest exist in sqlite")

    
# import json
# import Miscellaneous_Functions as mf
# import sqlite3
# import sys
# import De_Identification as de
# import Export as ex
# import getpass
# import os
# import datetime
# import pandas as pd
# import Import as i
# from IPython.display import display
# from tabulate import tabulate

# if __name__ == "__main__":
#     # Extract arguments passed from command line
#     project_name = sys.argv[1]
#     table_name = sys.argv[2]
#     db_file_path = mf.tool_path + "\\" + project_name + "\\TablesData\\Data.db"
    
#     if mf.check_table_existence(table_name, db_file_path):
#         pd.set_option('display.max_rows', None)
#         pd.set_option('display.max_columns', None)
#         response = mf.SqlLite_Data_To_Df(db_file_path, table_name)
        
#         # Convert DataFrame to JSON
#         json_result = response.to_json(orient='records')
        
#         # Print JSON result
#         print(json_result)
        
#         # Optional: Display tabulated result in a readable format
#         result = tabulate(response, headers='keys', tablefmt='simple', showindex=False)
#         # display(result)
#     else:
#         print(f"The table {table_name} does not exist in sqlite")

import json
import Miscellaneous_Functions as mf
import sys
import pandas as pd
from tabulate import tabulate

if __name__ == "__main__":
    # Extract arguments passed from command line
    project_name = sys.argv[1]
    table_name = sys.argv[2]
    db_file_path = mf.tool_path + "\\" + project_name + "\\TablesData\\Data.db"
    
    if mf.check_table_existence(table_name, db_file_path):
        pd.set_option('display.max_rows', None)
        pd.set_option('display.max_columns', None)
        response = mf.SqlLite_Data_To_Df(db_file_path, table_name)
        
        # Convert DataFrame to a list of JSON objects
        # json_list = response.reset_index().to_dict(orient='records')
        json_list = response.to_dict(orient='records')

        
        # Print JSON list with each object on a new line
        print("[")
        print(",\n".join(json.dumps(record, indent=4) for record in json_list))
        print("]")
        
        # Optional: Display tabulated result in a readable format
        result = tabulate(response, headers='keys', tablefmt='simple', showindex=False)
        # display(result)
    else:
        print(f"The table {table_name} does not exist in sqlite")
