# Hello World API
**Basic API project for job application**
*by Kenneth Valenciano*


## About
- It's developed in .Net Core 3.1, using C#
- The chosen data storage option is Azure Cosmos DB.

## Database
- I am using Azure Cosmos DB because it is a distributed and redundant No-SQL database as-a-service, perfect for a quick solution like this one. 
- It is using the default collection names because I am using the free tier for this project

## How to quickly run this locally
- You must have Visual Studio and .Net Core 3.1 installed in your machine
- Clone this repository in your machine
- Open the solution in Visual Studio and run it from there.
- Change the values in the appsettings.json file with the ones provided by email

## How to consume the API
- It is available at https://helloworldapi20201203083901.azurewebsites.net/movies
- You can use this Postman Collection https://www.getpostman.com/collections/929c60e0e566e3d9a620 to quickly try the API

## Dataset
A sample data set was taken from https://github.com/mikeleguedes/json-movie-list