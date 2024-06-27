import os
import sys

def rename_folder(current_name, new_name):
    try:
        os.rename(current_name, new_name)
        print(f"success")
    except FileNotFoundError:
        print(f"The folder '{current_name}' does not exist")
    except FileExistsError:
        print(f"A folder with the name '{new_name}' already exists")
    except Exception as e:
        print(f"An error occurred: {e}")

if __name__ == "__main__":
    # Get current folder name and new folder name from the user
    current_name = sys.argv[1]
    new_name = sys.argv[2]

     
    rename_folder(current_name, new_name)
