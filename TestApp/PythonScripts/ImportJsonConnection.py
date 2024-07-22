import json
import pandas as pd
import sqlite3
from typing import Any, Dict, List, Union
import random
import sys
import os
import Miscellaneous_Functions as mf
import getpass

def load_json(file_path: str) -> Union[Dict[str, Any], List[Dict[str, Any]]]:
    with open(file_path, 'r') as f:
        return json.load(f)

def normalize_nested_json(data: Any, parent_key: str = '', sep: str = '_') -> Dict[str, Any]:
    items = []
    if isinstance(data, dict):
        for k, v in data.items():
            new_key = f"{parent_key}{sep}{k}" if parent_key else k
            if isinstance(v, dict):
                items.extend(normalize_nested_json(v, new_key, sep=sep).items())
            elif isinstance(v, list):
                for i, item in enumerate(v):
                    items.extend(normalize_nested_json(item, f"{new_key}{sep}{i}", sep=sep).items())
            else:
                items.append((new_key, v))
    elif isinstance(data, list):
        for i, item in enumerate(data):
            items.extend(normalize_nested_json(item, f"{parent_key}{sep}{i}", sep=sep).items())
    else:
        items.append((parent_key, data))
    return dict(items)

def process_single_table(json_data: Union[Dict[str, Any], List[Dict[str, Any]]], table_name: str) -> Dict[str, pd.DataFrame]:
    if isinstance(json_data, dict):
        data = json_data[table_name]
    else:
        data = json_data
    return {table_name: pd.json_normalize(data)}

def process_multiple_tables(json_data: Dict[str, Any], parent_key: str = '') -> Dict[str, pd.DataFrame]:
    data_frames = {}
    for table_name, data in json_data.items():
        if isinstance(data, list):
            df = pd.json_normalize(data, sep='_')
            for col in df.columns:
                if isinstance(df[col].dropna().iloc[0], (dict, list)):
                    nested_df = pd.json_normalize(df[col].dropna().apply(lambda x: normalize_nested_json(x)), sep='_')
                    nested_table_name = f"{table_name}_{col}"
                    data_frames[nested_table_name] = nested_df
                    df.drop(columns=[col], inplace=True)
            data_frames[table_name] = df
        elif isinstance(data, dict):
            nested_frames = process_multiple_tables(data, parent_key=table_name)
            for nested_table_name, nested_df in nested_frames.items():
                full_table_name = f"{table_name}_{nested_table_name}" if parent_key else nested_table_name
                data_frames[full_table_name] = nested_df
    return data_frames

def save_to_sqlite(data_frames: Dict[str, pd.DataFrame], db_name: str, n: int) -> List[str]:
    new_tables = []
    with sqlite3.connect(db_name) as conn:
        for table_name, df in data_frames.items():
            if n > 0 and len(df) > n:
                df = df.sample(n=n).reset_index(drop=True)
            df.to_sql(table_name, conn, if_exists='replace', index=False)
            new_tables.append(table_name)
    return new_tables

def generate_unique_table_name(existing_tables: List[str], base_name: str = 'table') -> str:
    index = 1
    while f"{base_name}{index}" in existing_tables:
        index += 1
    return f"{base_name}{index}"

def main(file_path: str, db_name: str, n: int):
    json_data = load_json(file_path)
    existing_tables = get_all_tables(db_name)
    new_tables = []

    if isinstance(json_data, dict):
        data_frames = process_multiple_tables(json_data)
        saved_tables = save_to_sqlite(data_frames, db_name, n)
        new_tables.extend(saved_tables)
    elif isinstance(json_data, list):
        new_table_name = generate_unique_table_name(existing_tables + new_tables)
        data_frames = process_single_table(json_data, new_table_name)
        saved_tables = save_to_sqlite(data_frames, db_name, n)
        new_tables.extend(saved_tables)
    print("success")
    print(new_tables)

def get_all_tables(database_path: str) -> List[str]:
    with sqlite3.connect(database_path) as conn:
        cursor = conn.cursor()
        cursor.execute("SELECT name FROM sqlite_master WHERE type='table';")
        tables = cursor.fetchall()
    return [table[0] for table in tables]

if __name__ == '__main__':
    file_path = sys.argv[1]
    project_name = sys.argv[2]
    n = sys.argv[3]

    n = int(n)
    username = getpass.getuser()
    tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
    mf.create_path(tool_path)

    project_path = os.path.join(tool_path, project_name)
    mf.create_path(project_path)

    tables_data_path = os.path.join(project_path, 'TablesData')
    mf.create_path(tables_data_path)

    db_name = os.path.join(tables_data_path, 'Data.db')

    main(file_path, db_name, n)
