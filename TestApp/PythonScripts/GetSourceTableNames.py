import Miscellaneous_Functions as mf
import sqlite3
import os
 
def get_table_names(db_path):
    try:
        # Connect to the SQLite database
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()
        # Query to get all table names
        cursor.execute("SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'de_identified%';")
        tables = cursor.fetchall()
        # Extract table names from the result
        table_names = [table[0] for table in tables]
        return table_names
    except sqlite3.Error as e:
        return f"An error occurred: {e}"
    finally:
        # Close the connection
        if cursor:
            cursor.close()
        if conn:
            conn.close()
 
if __name__ == "__main__":
    # Extract arguments passed from command line
    import sys
    project_name = sys.argv[1]
    db_file_path = os.path.join(mf.tool_path, project_name, 'TablesData', 'Data.db')
 
    table_names = get_table_names(db_file_path)
    print(table_names)