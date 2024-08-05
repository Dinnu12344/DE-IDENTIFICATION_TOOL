import sqlite3
import pyodbc
import sys
import getpass
import os
import Miscellaneous_Functions as mf

def sqlite_to_sqlserver_dtype(sqlite_dtype):
    """Map SQLite data types to SQL Server data types."""
    dtype_mapping = {
        'INTEGER': 'INT',
        'REAL': 'FLOAT',
        'TEXT': 'VARCHAR',
        'BLOB': 'VARBINARY',
        'NUMERIC': 'DECIMAL'
    }
    return dtype_mapping.get(sqlite_dtype.upper(), sqlite_dtype.upper())

def validate_schemas(sqlite_cursor, sql_server_cursor, sqlite_tables, sql_server_tables):
    """Validate schemas of all tables in SQLite against SQL Server."""
    print("validate_schemas")
    for table in sqlite_tables:
        if table not in sql_server_tables:
            raise ValueError(f"Table {table} does not exist in SQL Server")
        
        # Fetch SQLite table schema
        sqlite_cursor.execute(f"PRAGMA table_info({table});")
        sqlite_schema = sqlite_cursor.fetchall()
        
        # Fetch SQL Server table schema
        sql_server_cursor.execute(f"""
            SELECT COLUMN_NAME, DATA_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = '{table}';
        """)
        sql_server_schema = sql_server_cursor.fetchall()
        
        # Check if schemas match
        if len(sqlite_schema) != len(sql_server_schema):
            raise ValueError(f"Schema mismatch for table {table}: column count mismatch")
        
        for sqlite_col, sql_server_col in zip(sqlite_schema, sql_server_schema):
            sqlite_col_name, sqlite_col_type = sqlite_col[1], sqlite_col[2]
            sql_server_col_name, sql_server_col_type = sql_server_col[0], sql_server_col[1]
            
            if sqlite_col_name != sql_server_col_name or sqlite_to_sqlserver_dtype(sqlite_col_type) != sql_server_col_type.upper():
                raise ValueError(f"Schema mismatch for column '{sqlite_col_name}' in table '{table}': "
                                 f"SQLite({sqlite_col_name}, {sqlite_col_type}) != SQL Server({sql_server_col_name}, {sql_server_col_type})")

def migrate_sqlite_to_sqlserver(sqlite_db_path, sql_server_conn_str):
    try:
        print("migrate_sqlite_to_sqlserver function")
        # Connect to SQLite database
        sqlite_conn = sqlite3.connect(sqlite_db_path)
        sqlite_cursor = sqlite_conn.cursor()
        
        # Connect to SQL Server database
        sql_server_conn = pyodbc.connect(sql_server_conn_str)
        sql_server_cursor = sql_server_conn.cursor()
        
        # Fetch all table names from SQLite
        sqlite_cursor.execute("SELECT name FROM sqlite_master WHERE type='table';")
        sqlite_tables = [row[0] for row in sqlite_cursor.fetchall()]
        
        # Fetch all table names from SQL Server
        sql_server_cursor.execute("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';")
        sql_server_tables = [row[0] for row in sql_server_cursor.fetchall()]
        
        # Validate schemas
        validate_schemas(sqlite_cursor, sql_server_cursor, sqlite_tables, sql_server_tables)
        
        # Insert data if all schemas match
        for table in sqlite_tables:
            # Fetch all data from SQLite table
            sqlite_cursor.execute(f"SELECT * FROM {table}")
            rows = sqlite_cursor.fetchall()
            columns = [desc[0] for desc in sqlite_cursor.description]
            
            # Insert data into SQL Server table
            for row in rows:
                placeholders = ', '.join(['?'] * len(row))
                sql_server_cursor.execute(
                    f"INSERT INTO {table} ({', '.join(columns)}) VALUES ({placeholders})",
                    row
                )
        
        # Commit the transaction
        sql_server_conn.commit()
        print("Data migration completed success")
    
    except Exception as e:
        print(f"An error occurred: {e}")
    
    finally:
        # Close connections
        sqlite_conn.close()
        sql_server_conn.close()

# Example usage







if __name__ == "__main__":

        # Extract arguments passed from command line
    if len(sys.argv) < 6:
        print("Error: Not enough arguments provided.")
        print("Usage: script.py <server_name> <database_name> <sql_server_password> <sql_server_username> <project_name>")
        sys.exit(1)

    # Extract arguments passed from command line
    server_name = sys.argv[1]
    database_name = sys.argv[2]
    sql_server_password = sys.argv[3]
    sql_server_username = sys.argv[4]
    project_name = sys.argv[5]

       

    username = getpass.getuser()
    tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
    mf.create_path(tool_path)
    project_path = os.path.join(tool_path, project_name)
    mf.create_path(project_path)
    tables_data_path = os.path.join(tool_path, project_name, 'TablesData')
    mf.create_path(tables_data_path)
    db_file_path = os.path.join(tables_data_path, 'Data.db')
        

    print("Before migrate_sqlite_to_sqlserver function")

    #sqlite_db_path = r'C:\Users\Jayanth C\NewExportAll.db'
    sql_server_conn_str = f'DRIVER={{SQL Server}};SERVER={server_name};DATABASE={database_name};UID={sql_server_username};PWD={sql_server_password}'
    migrate_sqlite_to_sqlserver(db_file_path, sql_server_conn_str)






