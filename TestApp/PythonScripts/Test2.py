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

# Specify the path to your text file
file_path = r'C:\Users\Jayanth C\AppData\Roaming\DeidentificationTool\Project1\Key\Key.txt'

# Read the content of the file and store it in a variable
file_content = read_text_file(file_path)

# Print the content (optional)
if file_content is not None:
    print(file_content)
