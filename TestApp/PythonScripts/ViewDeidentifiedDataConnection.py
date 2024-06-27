import json
import Miscellaneous_Functions as mf
import sys
import pandas as pd
from tabulate import tabulate
 
if __name__ == "__main__":
    # Extract arguments passed from command line
    import sys
    project_name = sys.argv[1]
    table_name = sys.argv[2]
 
    db_file_path=mf.tool_path+"\\"+project_name+"\\TablesData\\Data.db"
    if(mf.check_table_existence("de_identified_"+table_name,db_file_path)=="True"):
        pd.set_option('display.max_rows', None)
        pd.set_option('display.max_columns', None)
        respone=mf.SqlLite_Data_To_Df(db_file_path,f'de_identified_{table_name}')
        json_list = respone.to_dict(orient='records')
 
        print("[")
        print(",\n".join(json.dumps(record, indent=4) for record in json_list))
        print("]")
        # Optional: Display tabulated result in a readable format
        result = tabulate(respone, headers='keys', tablefmt='plain', showindex=False)
    else:
        print(f"The table de_identified_{table_name} doest exist in sqlite")