@echo off

if exist "C:\Program Files\Unity\Hub\Editor\2022.3.56f1\Editor\Unity.exe" set unityPath="C:\Program Files\Unity\Hub\Editor\2022.3.56f1\Editor\Unity.exe"

%unityPath% -projectPath %cd%