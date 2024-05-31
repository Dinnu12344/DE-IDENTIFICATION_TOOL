import sqlite3
import os
import shutil

# Function to delete folders in the file system
def delete_project(folder_path):
        
    try:
        # Check if the file exists
        # fold=os.path.join(r"E:\New folder",r"New folder")
        #fold=os.path.join(folder_path)

        if os.path.exists(folder_path):
            # Delete the file
            shutil.rmtree(folder_path)

            print(f" {folder_path} has been deleted.")
            return "success"
        else:
            return f"{folder_path} does not exist."
            
    except Exception as e:
        return f"An error occurred while deleting the file: {e}"
        

# Function to delete folders in the file system
def delete_folders(folder_path,table_name_logfile):
        
    try:
        # Check if the file exists
        # fold=os.path.join(r"E:\New folder",r"New folder")
        fold=os.path.join(folder_path,table_name_logfile)

        if os.path.exists(fold):
            # Delete the file
            shutil.rmtree(fold)

            print(f" {table_name_logfile} has been deleted.")
        else:
            print(f"{table_name_logfile} does not exist.")
    except Exception as e:
        print(f"An error occurred while deleting the file: {e}")


def delete_tables(db_path,table_name):
# Connect to the SQLite database
    conn = sqlite3.connect(db_path)

    try:
        # Create a cursor object
        cursor = conn.cursor()

        # Define the SQL command to drop the table
        sql_command = f'DROP TABLE IF EXISTS "{table_name}";'

        # Execute the SQL command
        cursor.execute(sql_command)
        
        # Define the SQL command to drop the deitentified table
        sql_command = f'DROP TABLE IF EXISTS de_identified_"{table_name}";'

        cursor.execute(sql_command)

        # Commit the changes
        conn.commit()

        print(f'Table {table_name} deleted successfully.')

    except sqlite3.Error as e:
        print("SQLite error:", e)

    finally:
        # Close the connection
        conn.close()
