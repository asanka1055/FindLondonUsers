# DWP Online Test - Asanka Dissanayake (Full CV / Resume: https://asanka1055.github.io/cv)


# Problem
Using the language of your choice please build your own API which calls the API at https://bpdts-test-app.herokuapp.com/, and returns people who are listed as either living in London, or whose current coordinates are within 50 miles of London.

# Solution
The solution I have developed is a small .NET Web API Project. It offers 2 API calls to return the out put data.

Two APIs return JSON list of the relevant users within London and all the users living within 50 miles of London.

# Building and Running
To build the solution download it to the local machine open it using Visual Studion 2013 or latest version of that and update the nuget packages and do the build and run.

# How to use
The solution offers 2 API calls for getting the specified users:

# API Calls
The solution has 2 GET rest endpoints. One to return all the users within 50 miles of London and one that returns all users in London by passing city as London.

GET http://localhost:2140/api/users/filtered-users - returns all the users within 50 miles of London.

GET http://localhost:2140/api/users/london-users - returns all the users of London.

# Technology
Used GeoCoordinate nuget package to get distance between two geo coordinates.

Used exception handling to avoid invalid data and avoid erros.

This project is based on .Net 4.5 and Web API 2.

UsersController contians two methods for API calls and User model contains attributes to data bind and retrieval.
