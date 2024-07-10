import sqlite3
import Miscellaneous_Functions as mf
import sys

def rename_table(db_file_path, old_table_name, new_table_name):
    try:
        # Connect to the SQLite database
        conn = sqlite3.connect(db_file_path)
        cursor = conn.cursor()

        # Check if the old table exists
        cursor.execute(f"SELECT name FROM sqlite_master WHERE type='table' AND name='{old_table_name}';")
        if cursor.fetchone() is None:
            print(f"The table '{old_table_name}' does not exist.")
            return

        # Check if the new table name already exists
        cursor.execute(f"SELECT name FROM sqlite_master WHERE type='table' AND name='{new_table_name}';")
        if cursor.fetchone() is not None:
            print(f"A table with the name '{new_table_name}' already exists.")
            return

        # Rename the table
        cursor.execute(f"ALTER TABLE {old_table_name} RENAME TO {new_table_name};")
        conn.commit()

        print(f"success")

    except sqlite3.Error as e:
        print(f"An error occurred: {e}")

    finally:
        # Close the database connection
        if conn:
            conn.close()


def rename_deidentified_table(db_file_path, old_table_name, new_table_name):
    try:
        # Connect to the SQLite database
        conn = sqlite3.connect(db_file_path)
        cursor = conn.cursor()

        # Check if the old table exists
        cursor.execute(f"ALTER TABLE {old_table_name} RENAME TO {new_table_name};")
        conn.commit()
        print(f"success")

    except sqlite3.Error as e:
        print(f"An error occurred: {e}")

    finally:
        # Close the database connection
        if conn:
            conn.close()


if __name__ == "__main__":
    # Get database file path, old table name, and new table name from the user
    project_name=sys.argv[1]
    old_table_name = sys.argv[2]
    new_table_name = sys.argv[3]
    
    

    db_file_path=mf.tool_path+'\\'+project_name+'\\TablesData'+'\\Data.db'
    rename_table(db_file_path, old_table_name, new_table_name)
    
    rename_deidentified_table(db_file_path,"de_identified_"+old_table_name,"de_identified_"+new_table_name)