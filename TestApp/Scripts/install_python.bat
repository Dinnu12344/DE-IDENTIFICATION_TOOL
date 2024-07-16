@echo off
REM Set Python installer and installation directory
SET "PYTHON_VERSION=python-3.12.4-amd64.exe"
SET "INSTALL_PATH=C:\Python312"
SET "INSTALLER_PATH=%~dp0%PYTHON_VERSION%"
 
REM Check if the script is run with an uninstall argument
if "%1"=="uninstall" (
    echo Uninstalling Python...
    rmdir /s /q "%INSTALL_PATH%"
    echo Python uninstallation complete.
    goto end
) 
 
REM Check if Python is already installed in the system
where python > nul 2>&1
if %errorlevel% equ 0 (
    echo Python is already installed.
    set "PYTHON_EXE=python"
    goto check_version
) else (
    echo Python not found, installing Python...
    "%INSTALLER_PATH%" /quiet InstallAllUsers=1 PrependPath=1 TargetDir="%INSTALL_PATH%"
 
    REM Verify installation
    if exist "%INSTALL_PATH%\python.exe" (
        echo Python installation succeeded.
        set "PYTHON_EXE=%INSTALL_PATH%\python.exe"
        REM Add Python to PATH (optional, depending on your needs)
        setx PATH "%INSTALL_PATH%;%PATH%"
        echo Added Python to PATH.
    ) else (
        echo Python installation failed.
        goto end
    )
)
 
:check_version
REM Check Python version to ensure it's the correct version
"%PYTHON_EXE%" --version > temp.txt 2>&1
set /p PYTHON_VERSION_INSTALLED=<temp.txt
del temp.txt
 
echo Installed Python version: %PYTHON_VERSION_INSTALLED%
if "%PYTHON_VERSION_INSTALLED:~7,5%"=="3.12." (
    echo Correct Python version is already installed.
) else (
    echo Different Python version installed. Installing the correct version...
    "%INSTALLER_PATH%" /quiet InstallAllUsers=1 PrependPath=1 TargetDir="%INSTALL_PATH%"
    set "PYTHON_EXE=%INSTALL_PATH%\python.exe"
)
 
:install_requirements
REM Install required Python packages
echo Installing required Python packages...
"%PYTHON_EXE%" -m pip install --upgrade pip
"%PYTHON_EXE%" -m pip install pandas numpy requests pyodbc sqlalchemy tabulate
 
REM Verify package installation
"%PYTHON_EXE%" -c "import pandas, numpy, requests, pyodbc, sqlalchemy, tabulate; print('Packages installed successfully')"
 
:end