import sqlite3
import os
import csv
import sys
import getpass
import Miscellaneous_Functions as mf

def export_table_to_csv(cursor, table_name, export_folder_path):
    """Export a single SQLite table to a CSV file."""
    cursor.execute(f"SELECT * FROM {table_name}")
    rows = cursor.fetchall()
    columns = [desc[0] for desc in cursor.description]
    
    # Define CSV file path
    csv_file_path = os.path.join(export_folder_path, f"{table_name}.csv")
    
    # Write data to CSV
    with open(csv_file_path, 'w', newline='', encoding='utf-8') as csv_file:
        writer = csv.writer(csv_file)
        writer.writerow(columns)
        writer.writerows(rows)

def check_all_tables_deidentified(sqlite_cursor):
    """Check if all tables are de-identified by ensuring each table has a corresponding de-identified version."""
    # Fetch all table names from SQLite
    sqlite_cursor.execute("SELECT name FROM sqlite_master WHERE type='table';")
    sqlite_tables = [row[0] for row in sqlite_cursor.fetchall()]
    
    non_deidentified_tables = [table for table in sqlite_tables if not table.startswith("de_identified_")]
    deidentified_tables = [table for table in sqlite_tables if table.startswith("de_identified_")]

    # Check if all non-deidentified tables have a de-identified counterpart
    deidentified_names = [table[len("de_identified_"):] for table in deidentified_tables]
    
    for table in non_deidentified_tables:
        if table not in deidentified_names:
            return False  # Table does not have a corresponding de-identified version

    return True

def generate_unique_export_folder(export_folder_base):
    """Generate a unique export folder name by incrementing the number if the folder already exists."""
    if not os.path.exists(export_folder_base):
        return export_folder_base

    counter = 1
    while True:
        new_folder = f"{export_folder_base}{counter}"
        if not os.path.exists(new_folder):
            return new_folder
        counter += 1

def export_sqlite_tables_to_csv(sqlite_db_path, export_folder_path):
    """Export all tables from SQLite database to CSV files, if all tables are de-identified."""
    try:
        # Connect to SQLite database
        sqlite_conn = sqlite3.connect(sqlite_db_path)
        sqlite_cursor = sqlite_conn.cursor()
        
        # Check if all tables are de-identified
        if not check_all_tables_deidentified(sqlite_cursor):
            raise ValueError("Not all tables are de-identified.")
        
        # Fetch all table names from SQLite
        sqlite_cursor.execute("SELECT name FROM sqlite_master WHERE type='table';")
        sqlite_tables = [row[0] for row in sqlite_cursor.fetchall()]
        
        # Filter to get only de-identified tables
        deidentified_tables = [table for table in sqlite_tables if table.startswith("de_identified_")]
        
        # Generate unique export folder name
        export_folder_path = generate_unique_export_folder(export_folder_path)
        
        # Create export folder
        os.makedirs(export_folder_path)
        
        # Export each de-identified table to CSV
        for table in deidentified_tables:
            export_table_to_csv(sqlite_cursor, table, export_folder_path)
        
        print(f"De-identified tables exported successfully to {export_folder_path}.")
    
    except Exception as e:
        print(f"An error occurred: {e}")
    
    finally:
        # Close connections
        sqlite_conn.close()

if __name__ == "__main__":
    if len(sys.argv) < 3:
        print("Error: Not enough arguments provided.")
        print("Usage: script.py <project_name> <export_folder_path>")
        sys.exit(1)

    # Extract arguments passed from command line
    project_name = sys.argv[1]
    export_folder_path = sys.argv[2]

    username = getpass.getuser()
    tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
    mf.create_path(tool_path)
    project_path = os.path.join(tool_path, project_name)
    mf.create_path(project_path)
    tables_data_path = os.path.join(tool_path, project_name, 'TablesData')
    mf.create_path(tables_data_path)
    db_file_path = os.path.join(tables_data_path, 'Data.db')

    # Set the base export folder name
    export_folder_base = os.path.join(export_folder_path, f"{project_name}_CSVExports")
    
    # Export SQLite tables to CSV files
    export_sqlite_tables_to_csv(db_file_path, export_folder_base)
