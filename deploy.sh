#!/bin/bash

dotnet build --self-contained true --runtime linux-x64

dotnet publish --self-contained true --runtime linux-x64 

ssh phobos "rm /opt/todo_app_backend/*"

scp -r /home/antonyk/Source/todo_app_backend/bin/Debug/net6.0/linux-x64/publish/* phobos:/opt/todo_app_backend
