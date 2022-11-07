# CServer

## _DON'T INSTALL THIS DIRECTLY!_

This repo is meant as a submodule of [CSharper Practice](https://github.com/RobinBachus/CSharper).  
Download that and run:

```powershell
PS CSharper> git submodule update --init --recursive
PS CSharper> cd server/
PS CSharper/server> git pull origin master
```

This will put the source code of this project into 'server/'.

## about

This is the server component to my CSharper practice project. All c# code will be here as a backend for the website.

## changelog

### V0.0.3

- Split functionality for getting the HTTP requests and setting the responses
- Added RequestData class and interface for extra clarity
- Added calculator component
- Formatted console

### V0.0.2

- Added class to handle http requests

### V0.0.1

- Created project
- Added submodule to CSharper Practice
