
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
