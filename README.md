# City bike app (Backend using C# Dotnet6)
## The API layer, ApplicationCore layer, and Infrastructure layer are the three levels that make up this project's structure, which follows clean architecture approaches.
### .NET Core web api (Backend in C#), Docker Running (Container instances), Source code in Github
## Development Envirnoment
#### Development tools:
	Visual studio Code
	SqlServer

#### Docker:
	Desktop Application
	Docker hub Account
	Microsoft Azure Account
	Github Account
### Required Packages
#### In the ApplicationCore Project:
	AutoMapper.Extensions.Microsoft.DependencyInjection
	Microsoft.Extensions.Logging.Abstractions
	Microsoft.AspNetCore.Http.Features
	System.Linq.Dynamic.Core
#### In the Infrastructure Project:
	LumenWorksCsvReader
	Microsoft.EntityFrameworkCore
	EntityFramework
	Microsoft.EntityFrameworkCore.Design
	Microsoft.EntityFrameworkCore.SqlServer
	Microsoft.EntityFrameworkCore.Tools
	Microsoft.Extensions.Configuration.Xml
	Microsoft.Extensions.Configuration
#### In the Api Project:
	AutoMapper.Extensions.Microsoft.DependencyInjection
##### To Setup and install all packages you must install the dotnet EF tools for either Linux or Windows based on your Interest. The project developed with .NET 6 version. But you can change it in the project carefully.
The EF Core tools must installed globally using the.NET Entity Framework Core tools (dotnet ef), which may be be updated using the run dotnet tool update -g dotnet-ef command if you are using Linux based envirnoments.
### This project contains 3 interconnected project solutions (sln):
	1.  Project.API,
	2.  Project.ApplicationCore,
	3.  Projec.Infrastructure.
### This project included the following features:
	1. Data import and verification: During registration, duration will be determined from departure and return datetime. If duration and distance with less than 10 in number will not be imported. Using an Android smartphone, the distance needs to be computed from the Map (GPS) (It is Under development)
	2.  Journey list view: In this feature I tried to address the following 
				2.1. Pagination
				2.2. Ordering per column
				2.3. Searching
				2.4.Filtering
	3. Station list: I create the code to only list stations that have pagination and searching capabilities.
	4. Single station view: This feature calculates the total number of trips departing from and arriving at the station.
	5. Additional: The top 5 most frequent departure and return stations are calculated, as well as the average distance of a trip that begins at a station and ends there.
	6. Ability to filter all the calculations per month.
# The Detail of the Installation and Usage are described in README.md file in the src folder.
