echo on

for /f "delims=" %%i in ('dir /b proto "proto/*.proto"') do protoc  --csharp_out=./Game/  ./proto/%%i  

pause
