@echo off
call :FindDotnet || exit /b 1

"%DOTNET_PATH%" build "%~dp0Assignment_1_final_version.sln" --configuration Release || exit /b 1

start "ClientGUI" cmd /k ""%DOTNET_PATH%" run --project "%~dp0ClientGUI\ClientGUI.csproj" --configuration Release --no-build""
start "ForwardProxy" cmd /k ""%DOTNET_PATH%" run --project "%~dp0ForwardProxy\ForwardProxy.csproj" --configuration Release --no-build""
start "Server" cmd /k ""%DOTNET_PATH%" run --project "%~dp0Server\Server.csproj" --configuration Release --no-build""

goto :eof

:FindDotnet
set "DOTNET_PATH="
for /r "C:\Program Files\dotnet" %%D in (dotnet.exe) do (
    set "DOTNET_PATH=%%D"
    goto :FoundDotnet
)
for /r "C:\Program Files (x86)\dotnet" %%D in (dotnet.exe) do (
    set "DOTNET_PATH=%%D"
    goto :FoundDotnet
)
echo .NET SDK not found. Please ensure the .NET SDK is installed.
pause

:FoundDotnet
echo Found .NET SDK at %DOTNET_PATH%
