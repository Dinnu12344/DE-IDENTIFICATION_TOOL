import os

def check_json_file_presence(file_path):
    try:
        # Check if the specified path points to an existing file
        if os.path.isfile(file_path):
            return "File present"
        else:
            return "File not present"
    except Exception as e:
        return f"An error occurred: {e}"

# Specify the path to your JSON file
file_path = r'C:\Users\Jayanth C\Downloads\Book.txt'

# Check if the file is present and print the result
result = check_json_file_presence(file_path)
print(result)
