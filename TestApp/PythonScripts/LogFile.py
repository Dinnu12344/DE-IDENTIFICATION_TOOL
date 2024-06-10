import logging
import os
import tkinter as tk
from tkinter import filedialog, scrolledtext
import datetime
import getpass

# Function for creating the folders
def create_path(path):
    if not os.path.exists(path):
        os.makedirs(path)
        print("Folder created successfully.")
    else:
        print("Folder already exists.")
    return path


def append_logs_to_file(file_path, run_start, run_end, status, duration, comment):
    try:
        # Open the file in append mode
        with open(file_path, "a+") as file:
            # Format the log message
            log_message = f"\nRun start: {run_start}\nRun End: {run_end}\nStatus: {status}\nDuration: {duration}\nComment: {comment}\n"
            # Append the formatted log message to the file
            file.write(log_message)
    except Exception as e:
        print(f"An error occurred: {e}")

# Configure logging
project = 'Project1'
username = getpass.getuser()

tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'

log_files_path = os.path.join(tool_path, project, 'LogFiles')
create_path(log_files_path)

table_name = "person1_data"

log_files_path_table = os.path.join(log_files_path, f'{table_name}')
create_path(log_files_path_table)

# Define the log file path dynamically
log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
filename = os.path.join(log_files_path_table, log_filename)

# Configure the logger
logging.basicConfig(filename=filename, level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

def show_log(filename):
        #log_textbox = tk.Text()
        try:
            with open(filename, 'r') as file:
                print("hello")
                log_contents = file.read()
                print(log_contents)
                # log_textbox.delete(1.0, tk.END)
                # log_textbox.insert(tk.END, log_contents)
        except FileNotFoundError:
            logging.error(f"Log file not found at path: ")
            print(f"Log file not found at path: ")

# def show_log(self):
#         try:
#             with open(filename, 'r') as file:
#                 log_contents = file.read()
#                 self.log_textbox.delete(1.0, tk.END)
#                 self.log_textbox.insert(tk.END, log_contents)
#         except FileNotFoundError:
#             logging.error(f"Log file not found at path: ")
#             print(f"Log file not found at path: ")
