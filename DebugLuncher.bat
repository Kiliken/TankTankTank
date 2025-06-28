@echo off

set name=TankTankTank

if exist "%cd%\%name%.exe" set game="%cd%\%name%.exe"

set /p commands="[Unity] "


if "%commands:~0,5%"=="-std" (
    set commands=-ip 127.0.0.1 -port 25565 -player A
)

%game% -logFile "%cd%\%name%.txt" %commands%

pause