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
	
