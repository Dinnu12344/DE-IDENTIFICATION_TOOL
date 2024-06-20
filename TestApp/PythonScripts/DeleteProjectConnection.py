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
import sys


if __name__ == "__main__":
    # Extract arguments passed from command line
    project_name = sys.argv[1]
    
    response=d.delete_folders(mf.tool_path+"\\"+project_name)
    print("success")




