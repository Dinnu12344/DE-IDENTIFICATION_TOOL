@echo off
SET "PYTHON_VERSION=python-3.12.4-amd64.exe"
SET "INSTALL_PATH=C:\Python312"
SET "INSTALLER_PATH=%~dp0%PYTHON_VERSION%"
 
REM Function to check if a command exists
verifyPython() {
    "%INSTALL_PATH%\python.exe" -V >nul 2>&1
}

REM Check if Python is already installed
verifyPython
if %ERRORLEVEL% == 0 (
    echo Python is already installed.
    goto install_requirements
)
 
REM Check if the installer exists
if not exist "%INSTALLER_PATH%" (
    echo Python installer not found at %INSTALLER_PATH%.
    goto end
)
 
REM Install Python
echo Installing Python...
"%INSTALLER_PATH%" /quiet InstallAllUsers=1 PrependPath=1 TargetDir="%INSTALL_PATH%"
 
REM Verify installation
verifyPython
if %ERRORLEVEL% == 0 (
    echo Python installation succeeded.
    REM Add Python to PATH
    setx PATH "%INSTALL_PATH%;%PATH%"
    echo Added Python to PATH.
) else (
    echo Python installation failed.
    goto end
)
 
:install_requirements
REM Install required Python packages
echo Installing required Python packages...
"%INSTALL_PATH%\python.exe" -m pip install --upgrade pip
"%INSTALL_PATH%\python.exe" -m pip install pandas numpy requests pyodbc sqlalchemy tabulate
 
REM Verify package installation
"%INSTALL_PATH%\python.exe" -c "import pandas, numpy, requests, pyodbc, sqlalchemy, tabulate; print('Packages installed successfully')"
 
:end
pause
