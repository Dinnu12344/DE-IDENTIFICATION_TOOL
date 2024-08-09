import json
import pandas as pd
import os
import sqlite3
import sys
import getpass
import Miscellaneous_Functions as mf  # Assuming Miscellaneous_Functions is imported as `mf`

def flatten_dict(d, parent_key='', sep='_'):
    """
    Recursively flattens a nested dictionary.
    """
    items = []
    for k, v in d.items():
        new_key = f"{parent_key}{sep}{k}" if parent_key else k
        if isinstance(v, dict):
            items.extend(flatten_dict(v, new_key, sep=sep).items())
        else:
            items.append((new_key, v))
    return dict(items)

def process_json(data, parent_key='', parent_id=None, sep='_', id_tracker={}):
    if isinstance(data, list):
        df = pd.DataFrame()
        for item in data:
            item_df = process_json(item, parent_key, parent_id, sep, id_tracker)
            df = pd.concat([df, item_df], ignore_index=True)
        return df
    elif isinstance(data, dict):
        rows = []
        nested_data = {}
        current_id = id_tracker.get(parent_key, 0) + 1
        id_tracker[parent_key] = current_id

        for key, value in data.items():
            if isinstance(value, dict):
                flat_dict = flatten_dict(value, f"{parent_key}{sep}{key}".lstrip(sep), sep)
                rows.extend(flat_dict.items())
            elif isinstance(value, list):
                nested_data[key] = value  # Store the nested array for further processing
            else:
                rows.append((f"{parent_key}{sep}{key}".lstrip(sep), value))

        main_df = pd.DataFrame([dict(rows)])
        main_df[f"{parent_key}id"] = current_id
        if parent_id is not None:
            main_df[f"{parent_key}parent_id"] = parent_id

        for key, value in nested_data.items():
            nested_df = process_json(value, f"{parent_key}{sep}{key}".lstrip(sep), current_id, sep, id_tracker)
            nested_table_name = f"{parent_key}{sep}{key}".lstrip(sep)
            if nested_table_name not in nested_tables:
                nested_tables[nested_table_name] = nested_df
            else:
                nested_tables[nested_table_name] = pd.concat([nested_tables[nested_table_name], nested_df], ignore_index=True)
                
        return main_df
    else:
        return pd.DataFrame([{parent_key: data}])

def check_table_exists(conn, table_name):
    query = f"SELECT name FROM sqlite_master WHERE type='table' AND name='{table_name}';"
    result = conn.execute(query).fetchone()
    return result is not None

def get_new_table_name(conn, base_name):
    counter = 1
    new_table_name = base_name
    while conn and check_table_exists(conn, new_table_name):
        new_table_name = f"{base_name}{counter}"
        counter += 1
    return new_table_name

def save_to_sqlite(db_name, n):
    conn = sqlite3.connect(db_name)
    created_tables = []  # List to track all newly created tables
    
    try:
        for table_name, df in nested_tables.items():
            for column in df.columns:
                if df[column].apply(lambda x: isinstance(x, dict)).any():
                    df = pd.concat([df.drop([column], axis=1), df[column].apply(pd.Series).add_prefix(f'{column}_')], axis=1)

            new_table_name = get_new_table_name(conn, table_name if table_name.strip() else "table1")
            df.head(n).to_sql(new_table_name, conn, if_exists='replace', index=False)
            created_tables.append(new_table_name)
    finally:
        conn.close()
    
    return created_tables

def process_json_file(file_path, db_name, n):
    with open(file_path, 'r') as file:
        json_data = json.load(file)

    global nested_tables
    nested_tables = {}

    if isinstance(json_data, list):
        nested_tables[get_new_table_name(None, "table1")] = process_json(json_data)
    else:
        db_key = next((k for k, v in json_data.items() if isinstance(v, dict)), None)
        if db_key:
            for key, value in json_data[db_key].items():
                if isinstance(value, list) and value:
                    table_name = key.strip() or get_new_table_name(None, "table1")
                    nested_tables[table_name] = process_json(value, table_name)
                elif isinstance(value, dict) or isinstance(value, list):
                    nested_tables[key] = process_json(value, key)
        else:
            nested_tables[get_new_table_name(None, "table1")] = process_json(json_data)

    created_tables = save_to_sqlite(db_name, n)
    print("success")
    print(created_tables)

def main(file_path, db_name, n):
    process_json_file(file_path, db_name, n)

if __name__ == '__main__':
    file_path = sys.argv[1]
    project_name = sys.argv[2]
    n = int(sys.argv[3])

    username = getpass.getuser()
    tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
    mf.create_path(tool_path)

    project_path = os.path.join(tool_path, project_name)
    mf.create_path(project_path)

    tables_data_path = os.path.join(project_path, 'TablesData')
    mf.create_path(tables_data_path)

    db_name = os.path.join(tables_data_path, 'Data.db')

    main(file_path, db_name, n)
