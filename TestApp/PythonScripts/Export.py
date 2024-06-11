import pandas as pd
import sqlite3
import os
import pyodbc
from sqlalchemy import inspect
import datetime

def normalize_path(selected_path):
    # Get the absolute path to ensure uniform format
    drive, tail = os.path.splitdrive(selected_path)
    
    # Check if the tail is a root path (just a backslash)
    if tail == '\\' or tail == '':
        return drive + ':'
    else:
        return selected_path

def export_to_csv(Table_name,sqlite_conn,path):
    try:
        # Create a cursor object
        sqlite_conn = sqlite3.connect(sqlite_conn)
        # Read data from SQLite database
        df = pd.read_sql_query(f"SELECT * FROM de_identified_{Table_name}", sqlite_conn)
       

        if path:
            path_file = f"{Table_name}_"+ datetime.datetime.now().strftime("%Y%m%d_%H%M") + ".csv"
            path = os.path.join(path, path_file)
          
            # Export DataFrame to CSV file
            df.to_csv(path, index=False)

            # print("Export Successful:", f"{Table_name} exported to CSV file at {path}")

            Status = "Success"
            Comment = f"Export Successful: {Table_name} exported to CSV file at {path}"
            return Status,Comment
            # messagebox.showinfo("Export Successful", f"{table_name} exported to CSV file at {file_path}")
        else:
            # print("Export Cancelled: No file path entered.")

            Status = "Failed"
            # Comment = "Export Cancelled: No file path entered."
            return Status
            
        # messagebox.showinfo("Export Successful", f"{Table_name} exported to CSV file successfully.")
    except Exception as e:
        print("Export Error:", f"An error occurred: {e}")
        Status = "Failed"
        # Comment = f"Failed with one or more exceptions : {e}"
        return Status

        # messagebox.showerror("Export Error", f"An error occurred: {e}")


def table_exists(engine, schema, table_name):
    """Check if a table exists in the database."""
    inspector = inspect(engine)
    return table_name in inspector.get_table_names()

def get_sql_server_columns(engine, table_name):
    """Retrieve column names from the SQL Server table."""
    inspector = inspect(engine)
    columns = inspector.get_columns(table_name)
    return [column['name'] for column in columns]

def get_sql_server_columns(sql_server_conn, table_name):
    """
    Retrieve column names from a SQL Server table.
    Args:
        sql_server_conn (pyodbc.Connection): Connection to the SQL Server database.
        table_name (str): Name of the SQL Server table.
    Returns:
        list: List of column names.
    """
    sql_server_cursor = sql_server_conn.cursor()
    sql_server_cursor.execute(f'SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = \'{table_name}\'')
    columns = [row.COLUMN_NAME for row in sql_server_cursor.fetchall()]
    sql_server_cursor.close()
    return columns

def export_to_sql_server_user_defined(Server_name, Database_name, db_file_path, table_name, sqlserver_table_name,username,password,schema):
    # SQLite connection settings
    sqlite_conn = sqlite3.connect(db_file_path)
    sqlite_cursor = sqlite_conn.cursor()

    # SQL Server connection settings
    sql_server_conn_str = f'DRIVER={{SQL Server}};SERVER={Server_name};DATABASE={Database_name};UID={username};PWD={password}'
    
    # Connect to SQL Server database
    sql_server_conn = pyodbc.connect(sql_server_conn_str)
    sql_server_cursor = sql_server_conn.cursor()

    try:
        sqlite_table_name = f"de_identified_{table_name}"

        # SQL Server connection settings
        sql_server_conn_str = f'DRIVER={{SQL Server}};SERVER={Server_name};DATABASE={Database_name};UID={username};PWD={password}'

        # Retrieve column names from SQLite table
        sqlite_cursor.execute(f'PRAGMA table_info({sqlite_table_name})')
        sqlite_columns = [row[1] for row in sqlite_cursor.fetchall()]

        # Construct column names with square brackets for SQL Server
        sql_server_columns = get_sql_server_columns(sql_server_conn, sqlserver_table_name)

        # Check if column names match
        if set(sqlite_columns) != set(sql_server_columns):
            mismatched_columns = {
                "SQLite_Columns": sqlite_columns,
                "SQL_Server_Columns": sql_server_columns
            }
            print("Mismatched column names: ", mismatched_columns)
            raise ValueError("Column names in SQLite table do not match those in SQL Server table.")

        # Retrieve data from SQLite table
        sqlite_cursor.execute(f'SELECT * FROM {sqlite_table_name}')
        rows = sqlite_cursor.fetchall()

        # Insert data into SQL Server table
        for row in rows:
            placeholders = ','.join('?' * len(row))
            columns_str = ', '.join(sql_server_columns)
            sql_server_cursor.execute(f'INSERT INTO {schema}.{sqlserver_table_name} ({columns_str}) VALUES ({placeholders})', row)
            sql_server_conn.commit()

        print("Data transfer completed successfully.")
        status = "Success"
        comment = "Data transfer completed successfully"
        return status,comment
    #      # SQL Server connection parameters
    #     sql_server_params = {
    #         'server': Server_name,
    #         'database': Database_name,
    #         'username': username,
    #         'password': password
    #         #'trusted_connection': 'yes'  # Use Windows authentication
    #     }
    #     sqlite_conn = sqlite3.connect(db_file_path)

    #      # Connect to SQL Server
    #     conn_sql_server = pyodbc.connect(
    #         f"DRIVER=ODBC Driver 17 for SQL Server;"
    #         f"SERVER={sql_server_params['server']};"
    #         f"DATABASE={sql_server_params['database']};"
    #         f"UID={sql_server_params['username']};"
    #         f"PWD={sql_server_params['password']}"
    #     )
    #     sql_server_conn_str = f"mssql+pyodbc://{username}:{password}@{Server_name}/{Database_name}?driver=ODBC+Driver+17+for+SQL+Server"
    #     sql_server_engine = create_engine(sql_server_conn_str)

    #     sqlite_engine = create_engine(f"sqlite:///{db_file_path}")
    # # sqlite_conn1 = f"DRIVER=ODBC Driver 17 for SQL Server;SERVER={Server_name};DATABASE={Database_name};Trusted_Connection=yes;"
    # # sql_server_conn = pyodbc.connect(sqlite_conn1)
    # # sqlite_conn = sqlite3.connect(db_file_path)

    #     # # Insert data into SQL Server table
    #     # if schema:
    #     #     sqlserver_table_name_with_schema = f"{schema}.{sqlserver_table_name}"
    #     # else:
    #     #     sqlserver_table_name_with_schema = sqlserver_table_name

    #     df = pd.read_sql_query(f"SELECT * FROM de_identified_{table_name}", sqlite_engine)
    #     print(df)
    #     # Check if the target table exists in SQL Server
    #     if table_exists(sql_server_engine, schema, sqlserver_table_name):
    #         print("Tables exists")

    #          # Get column names from SQL Server table
    #         sql_server_columns = get_sql_server_columns(sql_server_engine, sqlserver_table_name)

    #          # Check if column names in DataFrame match those in SQL Server table
    #         if set(df.columns) != set(sql_server_columns):
    #             mismatched_columns = {
    #                 "DataFrame_Columns": df.columns.tolist(),
    #                 "SQL_Server_Columns": sql_server_columns
    #             }
    #             print("Mismatched column Names: ", mismatched_columns)
    #             # print("Column names in SQL Server table:", sql_server_columns)
    #             raise ValueError(f"Column names in DataFrame do not match column names in SQL Server table '{table_name}'.")
            
    #         with sql_server_engine.connect() as connection:
    #                 sqlserver_table_name_with_schema = str(text(f"{schema}.{sqlserver_table_name}"))
    #                 print(table_name,"hello")
    #                 df.to_sql(sqlserver_table_name_with_schema, connection, index=False, if_exists='append')

    #         print(f"Data successfully inserted into SQL Server table '{sqlserver_table_name}'.")

    #         status = "Success"
    #         comment = f"Data successfully Exported SQL Server table '{sqlserver_table_name}'."
    #         return status, comment
            
    #     else:
    #         # print(f"Data successfully inserted into SQL Server table '{sqlserver_table_name}'.")
    #         status = "Failed"
    #         comment = f"Table '{sqlserver_table_name}' does not exist in the SQL Server database."
    #         return status, comment
    #         # raise ValueError(f"Table '{sqlserver_table_name}' does not exist in the SQL Server database.")


           
    except Exception as e:
        status = "Failed"
        comment = f"Failed with one or more exceptions : {e}"
        print("Export Error:", f"An error occurred: {e}")
        return status, comment

    finally:
        sqlite_cursor.close()
        sqlite_conn.close()
        sql_server_cursor.close()
        sql_server_conn.close()




