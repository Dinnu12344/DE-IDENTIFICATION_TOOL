import sqlite3
import pandas as pd
import os
import logging
import getpass




username = getpass.getuser()
tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
    
#------------------------------------------------------------------------------------------------------------------
#************* This function is used for sending the data from SqliLite to Dataframe  *****************

def SqlLite_Data_To_Df(db_file_path,table_name):
    # print("SqlLite_Data_To_Df Function")

    try:
    # Connect to SQLite database
        conn = sqlite3.connect(db_file_path)
        
        # Query data from the database
        query_data = f"SELECT * FROM {table_name}"
        df = pd.read_sql_query(query_data, conn)
        df = df.convert_dtypes()
        # Display the DataFrame
        # print(df) 

    except sqlite3.Error as e:
        print("SQLite error:", e)

    finally:
        # Close connection
        if conn:
            conn.close()

    return df

#------------------------------------------------------------------------------------------------------------------

#*****************  This funtion is used for saving the DF data into the SqlLite  ********************
def Df_Data_To_Sqlite(db_file_path,df,table_name):
    try:
        # Connect to SQLite database
        conn = sqlite3.connect(f'{db_file_path}')
        # Write DataFrame to SQLite table
        df.to_sql(f"{table_name}", conn, if_exists='replace', index=False)

        # Commit changes
        conn.commit()
        # print("Df_Data_To_Sqlite")
        return "success"

    except sqlite3.Error as e:
        return "SQLite error:", e

    finally:
        # Close connection
        if conn:
            conn.close()

#------------------------------------------------------------------------------------------------------------------

#********************** This method is used for displaying all the list of projects ***********************
def list_projects(path):
    try:
        # Get a list of all the items (files and folders) in the given path
        items = os.listdir(path)
        
        # Filter out only the folders from the list of items
        folders = [item for item in items if os.path.isdir(os.path.join(path, item))]
        
        # Print the list of folders
        print("Folders in", path, ":")
        for folder in folders:
            print(folder)
    
    except OSError as e:
        print("Error:", e)

#********************** This method is used for displaying all the list of tables of a project ***********************
def tables_list_Of_sqlite(database_path):
    # print("tables_list_Of_sqlite")
    try:
        # Connect to the SQLite database
        conn = sqlite3.connect(database_path)
        
        # Create a cursor object
        cursor = conn.cursor()
        
        # Define the SQL query to select table names
        sql_query = "SELECT name FROM sqlite_master WHERE type='table';"
        
        # Execute the query
        cursor.execute(sql_query)
        
        # Fetch all rows
        rows = cursor.fetchall()
        
        # Extract table names from the rows
        table_names = [row[0] for row in rows]
        
        # Print the table names
        l=[]
        # print("Tables in the Project :")
        for table_name in table_names:
            # print(table_name)
            l.append(table_name)
    
    except sqlite3.Error as e:
        print("SQLite error:", e)
    
    finally:
        # Close the connection
        conn.close()
    return l

#---------------------------------------------------------------------------------------------------------
#**************** This method is used for  checking whether the table is exist or not ****************
def check_table_existence(table_name, database_path):
    # print("check_table_existence fun")
    table_names = tables_list_Of_sqlite(database_path)
    # print(table_names)
    if table_name in table_names:
        return True
        #return "Table is Present"
    elif table_name=="":
        Comment = "Table does not specified"
        return Comment
    else:
        Comment = f"{table_name} does not exist"
        return Comment  

#---------------------------------------------------------------------------------------------------------
#**************** This method is used for  checking whether the file is exist or not ****************
def check_file_existence(folder_path, file_name):
    # Construct the file path

    file_path = os.path.join(folder_path, file_name)
    print(file_path)

    # Check if the file exists
    if os.path.isfile(file_path):
        return True
    return False


#Function for creting the folders
def create_path(path):
# Check if the folder already exists
    if not os.path.exists(path):
        # Create the folder if it doesn't exist
        os.makedirs(path)
       
#------------------------------------------------------------------------------------------------------------------

#--------------------Appending the log information into the specified path under the respected project------------------
def append_logs_to_file(file_path, job_name,run_start, run_end, status, duration, comment):
    try:
        # Open the file in append mode
        with open(file_path, "a+") as file:

            # Format the log message
            log_message = f"\nJob Name: {job_name}\nRun start: {run_start}\nRun End: {run_end}\nStatus: {status}\nDuration: {duration}\nComment: {comment}\n"

            # Append the formatted log message to the file
            file.write(log_message)

    except Exception as e:

        log_message = f"\nRun start: {run_start}\nRun End: {run_end}\nStatus: {status}\nDuration: {duration}\nError_Comment: {e}\n"

        file.write(log_message)
        #print(f"An error occurred: {e}")

#--------------------Displaying log using the specified path ------------------

def show_log(filename):
        #log_textbox = tk.Text()
        try:
            with open(filename, 'r') as file:
                log_contents = file.read()
                print(log_contents)
                # log_textbox.delete(1.0, tk.END)
                # log_textbox.insert(tk.END, log_contents)
        except FileNotFoundError:
            logging.error(f"Log file not found at path: {filename} ")
            print(f"Log file not found at path: {filename} ")



def read_text_file(file_path):
    try:
        with open(file_path, 'r') as file:
            # Read the entire content of the file
            content = file.read()
        return content
    except FileNotFoundError:
        print(f"File not found: {file_path}")
    except PermissionError:
        print(f"Permission denied: Unable to read the file {file_path}")
    except Exception as e:
        print(f"An error occurred: {e}")



