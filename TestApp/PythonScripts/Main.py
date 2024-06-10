#Main application
import pdb
import Import as i
import Delete as d
import De_Identification as de
import Export as ex
import sqlite3
import getpass
import os
import Miscellaneous_Functions as mf
import datetime
import pandas as pd

username = getpass.getuser()
# print("Username:", username)

# Specify the path where you want to create the folder
tool_path = f'C:\\Users\\{username}\\AppData\\Roaming\\DeidentificationTool'
# print(mf.create_path(tool_path))

def parse_relationships(input_str):
    relationships = []
    for relationship_str in input_str.split(';'):
        if relationship_str.strip():
            parts = relationship_str.strip().split(',')
            relationship = {
                'Existing_Table': parts[0].strip(),
                'Existing_Column': parts[1].strip(),
                'Source_Table': parts[2].strip(),
                'Source_Column': parts[3].strip()
            }
            relationships.append(relationship)
    return relationships


def get_user_input():
    existing_table = input("Enter existing table name: ")
    existing_column = input("Enter existing column name: ")
    source_table = input("Enter source table name: ")
    source_column = input("Enter source column name: ")
    return f"{existing_table}, {existing_column}, {source_table}, {source_column}"


def generate_input_str():
    input_str = ""
    while True:
        user_input = get_user_input()
        input_str += user_input + "; "
        cont = input("Do you want to add another relationship? (yes/no): ")
        if cont.lower() != 'yes':
            break
    return input_str.rstrip("; ")


def Import(db_file_path,project_name,log_files_path):           
    # print("Import_Fun")
    # pdb.set_trace()
    print("Choose the source path")
    print("1.CSV")
    print("2.SqlServer")
    choice = input("Enter your choice : ")
    Job_name = "Import and Maintaining table relations"
    run_start = datetime.datetime.now()
    table_name = ''
    try:
        if(choice=="1"):
            csv_path=input("Give the Csv file path : ")
            
            # Check if the CSV file exists
            if os.path.exists(csv_path):
                # File exists, proceed with reading
                df = pd.read_csv(csv_path)
                # Now you can work with the DataFrame 'df'

                delimeter=input("Give the delimeter for the csv file as , ; : ")
                quotechar=input("Give the quotechar you want ' or ""\" : ")
                table_name=input("Give the table name : ")
                if(mf.check_table_existence(table_name,db_file_path)==True):
                    while(True):
                        print("Table with this name is already exist in this project")
                        table_name=input("Enter different table name : ")
                        if(mf.check_table_existence(table_name,db_file_path)==False):
                            break
                n=input("Give No of rows you want to import form table : ")

                run_start = datetime.datetime.now()
                #Creating table_level_log path dynamically
                log_files_path_table = os.path.join(log_files_path, f'{table_name}')
                mf.create_path(log_files_path_table)

                #Defining LogFileName 
                log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
                filename = os.path.join(log_files_path_table, log_filename)

                choice = input("Do you want relation with already existing data? (yes/no): ")

                if choice.lower() == 'yes':
                    input_str = generate_input_str()
                    # print(input_str)
                    relationshipsList = parse_relationships(input_str)
                    
                    # print(relationshipsList)
                    run_start = datetime.datetime.now()                
                    Status,comment = i.Import_CSV_Relational_Data_To_SqLite(relationshipsList,csv_path,db_file_path,n,table_name,delimeter,quotechar)
                    run_end = datetime.datetime.now()
                    run_time = run_end - run_start
                    mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = comment)

                else:
                    Status,comment = i.Import_CSV_Data_To_SqLite(db_file_path,csv_path,n,table_name,project_name,delimeter,quotechar)

                    run_end = datetime.datetime.now()
                    run_time = run_end - run_start
                    mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = comment)
            else:
                print(f"The file path specified : {csv_path} does not exist.")
        elif(choice=='2'):
            print("Please provide the credentials to connect with SQL server")
            server_name=input("Give the your server name : ")
            sql_server_username = input("Please provide your Username : ")
            sql_server_password = input("Please provide your Password : ")
            data_base_name=input("Give database name : ")
            schema_name = input("Please provide the Schema name of the table : ")
            table_name=input("Give table name : ")
            n=input("Give No of rows you want to import form table : ")

            run_start = datetime.datetime.now()
            #Creating table_level_log path dynamically
            log_files_path_table = os.path.join(log_files_path, f'{table_name}')
            mf.create_path(log_files_path_table)
            #Defining LogFileName 
            log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
            filename = os.path.join(log_files_path_table, log_filename)
            
            i.Import_SqlServer_Data_To_SqLite(server_name,data_base_name,table_name,n,db_file_path,project_name,sql_server_username,sql_server_password,schema_name)
            choice = input("Do you want relation with alredy existing data? (yes/no): ")
            if choice.lower() == 'yes':
                input_str = generate_input_str()
                # print(input_str)
                relationshipsList = parse_relationships(input_str)
                # print(relationshipsList)
                              
                Status,comment = i.Import_SqlServer_Relational_Data_To_SqLite(relationshipsList,server_name,data_base_name,table_name,n, db_file_path,project_name,sql_server_username,sql_server_password,schema_name)
                run_end = datetime.datetime.now()
                run_time = run_end - run_start
                mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = comment)

            else:
                Status,comment = i.Import_SqlServer_Data_To_SqLite(server_name,data_base_name,table_name,n,db_file_path,project_name,sql_server_username,sql_server_password,schema_name)
                run_end = datetime.datetime.now()
                run_time = run_end - run_start
                mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = comment)

        else:
            
            run_end = datetime.datetime.now()
            run_time = run_end - run_start
            Status = "Failed"
            Comment = "Invalid input"
            mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)
            print("Invalid input")

    except Exception as e:
        if table_name is not None:
            # table_name = input("Enter table name")
            log_files_path_table = os.path.join(log_files_path, f'{table_name}')
            mf.create_path(log_files_path_table)

            #Defining LogFileName 
            log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
            filename = os.path.join(log_files_path_table, log_filename)
            run_end = datetime.datetime.now()
            run_time = run_end - run_start
            Status = "Failed"
            Comment = f"Failed with one or more exceptions : {e}"
            print(Comment)
            mf.append_logs_to_file(file_path = filename, job_name=Job_name,run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)


def Congfig(db_file_path):
    # print ("Config FUncition")
    table_name=input("Give the table name for which you want the Deidentification Configuration : ")
    if(mf.check_table_existence(f"{table_name}",db_file_path)==True):
        print("Table is exist")
        print("config is not implemented as it is complex with python inputs")
    else:
        print(f"The table {table_name} doest exist or table doesnt deidentified")
    

def Deidentification(config_files_path,db_file_path):
    # print("Deidentification fun")
    Job_name = "De-identification of data"
    table_name=input("Enter the table name, that you want to deidentify : ")
    run_start = datetime.datetime.now()

    try:   
        run_start = datetime.datetime.now()

        #Creating table_level_log path dynamically
        log_files_path_table = os.path.join(log_files_path, f'{table_name}')
        mf.create_path(log_files_path_table)

        #Defining LogFileName 
        log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
        filename = os.path.join(log_files_path_table, log_filename)

        if(mf.check_file_existence(config_files_path+"\\"+table_name,f"{table_name}.json")==True):           
            Status, Comment = de.de_Identification_Main(config_files_path,table_name,db_file_path) 
            # Run ends here
            run_end = datetime.datetime.now()
            # Run time
            run_time = run_end - run_start
            mf.append_logs_to_file(file_path = filename, job_name=Job_name,run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)

        else:

            run_end = datetime.datetime.now()
            # Run time
            run_time = run_end - run_start
            Status = "Failed"
            Comment = "Enter correct table name and fill the Config file"
            mf.append_logs_to_file(file_path = filename, job_name=Job_name,run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment)
            print("Enter correct table name and fill the Config file")

    except Exception as e:
        run_end = datetime.datetime.now()

        # Run time
        run_time = run_end - run_start
        Status = "Failed"
        Comment = f"De-identification failed with one or more exception : {e}"
        mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment) 
        print(e)


def Export(db_file_path):
    # print("Export fun")
    print("Select one of the export option")
    print("1. CSV")
    print("2. Sql Server")
    print(db_file_path)
    Job_name = "Export Data"
    run_start = datetime.datetime.now()
    
    try:
        choice=input("Enter the export option: ") 
        run_start = datetime.datetime.now()
        if choice=='1':
            table_name=input("Enter table name: ")
            path = input("Enter the file path to save csv file : ")

            run_start = datetime.datetime.now()

            #Creating table_level_log path dynamically
            log_files_path_table = os.path.join(log_files_path, f'{table_name}')
            mf.create_path(log_files_path_table)

            #Defining LogFileName 
            log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
            filename = os.path.join(log_files_path_table, log_filename)

            if(mf.check_table_existence(f"{table_name}",db_file_path)==True):
                if(mf.check_table_existence(f"de_identified_{table_name}",db_file_path)==True):
                    Status , Comment = ex.export_to_csv(f"{table_name}",db_file_path,path)
                    run_end = datetime.datetime.now()
                    run_time = run_end - run_start
                    mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment) 

                    print(f"{table_name} has exported to csv successfully")
                else:
                    print("Table is not deidentified")
            else:
                #Creating table_level_log path dynamically
                log_files_path_table = os.path.join(log_files_path, 'Incorrect_Table_Name')
                mf.create_path(log_files_path_table)

                #Defining LogFileName 
                log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
                filename = os.path.join(log_files_path_table, log_filename)
                run_end = datetime.datetime.now()
                run_time = run_end - run_start
                Status = "Failed"
                Comment = mf.check_table_existence(table_name, db_file_path)
                print(Comment)
                mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment) 

                # print(f"The table {table_name} doest exist or table doesnt deidentified")

        elif choice=='2':
            table_name=input("Enter table name : ")

             #Creating table_level_log path dynamically
            log_files_path_table = os.path.join(log_files_path, f'{table_name}')
            mf.create_path(log_files_path_table)
            #Defining LogFileName 
            log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
            filename = os.path.join(log_files_path_table, log_filename)
            
            if(mf.check_table_existence(f"de_identified_{table_name}",db_file_path)==True):

                Server_name=input("Enter your internal server")
                sql_server_username = input("Enter your Username : ")
                sql_server_password = input("PEnter your Password : ")
                Database_name=input("Enter the database")
                schema = input("Enter Schema name of the table : ")
                sqlserver_table_name=input("Enter sql server table name to export data to it:")

                run_start = datetime.datetime.now()
                Status, Comment = ex.export_to_sql_server_user_defined(Server_name,Database_name,db_file_path , table_name,sqlserver_table_name, sql_server_username,sql_server_password,schema)
                run_end = datetime.datetime.now()
                run_time = run_end - run_start
                mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment) 

            else:
                 #Creating table_level_log path dynamically
                log_files_path_table = os.path.join(log_files_path, f'{table_name}')
                mf.create_path(log_files_path_table)
                #Defining LogFileName 
                log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
                filename = os.path.join(log_files_path_table, log_filename)

                run_end = datetime.datetime.now()
                run_time = run_end - run_start
                Status = "Failed"
                Comment = f"The table {table_name} doest exist"
                mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment) 

                print(f"The table {table_name} doest exist")
        else:
             #Creating table_level_log path dynamically
            log_files_path_table = os.path.join(log_files_path, f'{table_name}')
            mf.create_path(log_files_path_table)
            #Defining LogFileName 
            log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
            filename = os.path.join(log_files_path_table, log_filename)

            run_end = datetime.datetime.now()
            run_time = run_end - run_start
            Status = "Failed"
            Comment = f"Enter the correct option"
            mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment) 

    except Exception as e:
        #Creating table_level_log path dynamically
        log_files_path_table = os.path.join(log_files_path, f'{table_name}')
        mf.create_path(log_files_path_table)
        #Defining LogFileName 
        log_filename = datetime.datetime.now().strftime("%Y-%m-%d") + "_logfile.log"
        filename = os.path.join(log_files_path_table, log_filename)

        run_end = datetime.datetime.now()
        run_time = run_end - run_start
        Status = "Failed"
        Comment = f"Failed with one or more exceptions : {e}"
        mf.append_logs_to_file(file_path = filename,job_name=Job_name, run_start =run_start, run_end = run_end, status = Status, duration  = run_time, comment = Comment) 
        print("Error occured : ",e)


def Logs():
    # print("Logs fun")
    try:
        table_name=input("Enter the table name to show its Logs : ")
        project_name=input("Enter the Project Name to show its Logs : ")
        log_date = input("Enter the day of log needs to be shown in the format (YYYY-MM-DD) : ")

        log_files_path = tool_path+'\\'+project_name+'\\'+"LogFiles"
        log_files_path_table = os.path.join(log_files_path, f'{table_name}')

        log_filename_obj = datetime.datetime.strptime(log_date, "%Y-%m-%d")
        log_filename = log_filename_obj.strftime("%Y-%m-%d") + "_logfile.log"
        filename = os.path.join(log_files_path_table, log_filename)
        print(filename)
        mf.show_log(filename)

    except Exception as e:
        print(e)
    
    
def Delete(tool_path,project_name,db_file_path):
    # print("Delete fun")
    table_name=input("Enter the table name that you want to delete : ")
    if(mf.check_table_existence(table_name,db_file_path)==True):
            d.delete_folders(tool_path+"\\"+project_name+"\\"+"LogFiles",table_name)
            d.delete_folders(tool_path+"\\"+project_name+"\\"+"ConfigFiles",table_name)
            d.delete_tables(db_file_path,table_name)
    else:
        print(f"The table {table_name} doest exist")


def table_level(tool_path,project_name,tables_data_path,config_files_path,log_files_path,db_file_path):
    while True:
        print("\nList of tables in project")
        l=mf.tables_list_Of_sqlite(db_file_path)
        for value in l:
            if not value.startswith("de_identified_"):
                print(value)
        print("Table level Menu:")
        print("1. Import New Table")
        # print("2. Deidentification Config")
        print("2. Deidentification of a Table")
        print("3. Export Deidentified Table")
        print("4. Table Logs")
        print("5. Delete Table")
        print("6. Close Project")

        choice = input("Enter your choice : ")

        if choice == '1':
            Import(db_file_path,project_name,log_files_path)
        # elif choice=='2':
        #     Congfig(db_file_path)
        elif choice == '2':
            Deidentification(config_files_path,db_file_path)
        elif choice == '3':
            Export(db_file_path)

        elif choice == '4':
            Logs()#Logs(log_files_path)
            
        elif choice == '5':
            Delete(tool_path,project_name,db_file_path)
        
        elif choice == '6':
            print(f"Closing {project_name}...")
            break
        else:
            print("Invalid choice!")

#Main
while True:
    print("\nDeIdentification Tool Menu:")
    print("1. Create Project ")
    print("2. Open Project")
    print("3. Exit application")

    choice = input("Enter your choice : ")

    if choice=='1':
        project_name=input("Give name for the Project : ")
        project_path=tool_path+'\\'+project_name
        print(mf.create_path(project_path))

        tables_data_path= tool_path+'\\'+project_name+'\\TablesData'
        print(mf.create_path(tables_data_path))

        config_files_path=tool_path+'\\'+project_name+'\\ConfigFiles'
        print(mf.create_path(config_files_path))

        log_files_path=tool_path+'\\'+project_name+'\\'+"LogFiles"
        print(mf.create_path(log_files_path))

        db_file_path = tables_data_path+'\\Data.db'

        print(f"Now you are using the project {project_name}")

        table_level(tool_path,project_name,tables_data_path,config_files_path,log_files_path,db_file_path)

    elif choice=='2':
        print("List of the Projects")
        mf.list_projects(tool_path)
        project_name=input("Enter the Project name that you want open : ")

        project_path=tool_path+'\\'+project_name

        tables_data_path= tool_path+'\\'+project_name+'\\TablesData'

        config_files_path=tool_path+'\\'+project_name+'\\ConfigFiles'

        log_files_path=tool_path+'\\'+project_name+'\\'+"LogFiles"

        db_file_path = tables_data_path+'\\Data.db'

        try:
        # Connect to the SQLite database (creates a new database file if it doesn't exist)
            conn = sqlite3.connect(db_file_path)
        except:
            print("error while creating the SQLite database for the project")
        finally:
        # Close the connection
            conn.close()
            print("SQLite database created successfully.")

        print(f"Now you are using the project {project_name}")

        table_level(tool_path,project_name,tables_data_path,config_files_path,log_files_path,db_file_path)

    elif choice=='3':
        print("Exiting the Application")
        print("Done")
        break

    else:
        print("Invalid choice")










