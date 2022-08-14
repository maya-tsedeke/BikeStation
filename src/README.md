# City Bike Backend Sample Code in C# Using Clean Architecture structure:
##  We applied three layers
###    1. Project.Api
###    2. Project.AplicationCore
###    3. Project.Infrastructure

## To get started, follow the below steps:

    1. Install .NET 6 SDK
    2. Install the latest SQL server or Use Azer databse  
    3. Install Docker Desktop for either Windows or Linux/Mac)
    4. Clone or download the Solution into your Local Directory
    5. In the project root you can find the docker-compose.yml file
    6. Run the below command to build and run the solution in Docker
    ```
    > docker-compose build --force-rm --no-cache && docker-compose up
    ```
    7. Once the containers start successfully navigate to http://localhost for the port 9000:80
    
    ## You can also just run the solution without Docker by following the steps below:
    1. Install .NET 6 SDK
    2. Install the SQL server
    3. Clone the Solution into your Local Directory
    4. Navigate to the Aproject.Api directory (./src) and run the below command to get the API running:

        ```
        > dotnet run --project ./Project.API
        ```
        Open a Postman, or you can enable swggare built in the project startup and navigate to http://localhost:5047 and you're all set!
####  NB: Before starting navigation you should change Database user name and Password as per your configration. The run the following dotnet commnd to set up and creating database. Migration already generated no need to use the first dotnet command but if not working properly you can remove the existing migration folder and rune the commands. Do not forget to Update command to create table in the Databse. 
####      Create Database: and change the confugration in the Project.Api (appsetting.json) file. (I used Azure database so change it as per your database configuration)
       
      "ConnectionStrings":
      {
      "DefaultConnection": "Server=localhost,1433;Initial Catalog=StationDB;Persist Security Info=False;User
      ID=user DB userName;Password=yourPassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
      },
###     After Configure Your Database run the following in the terminal like or CMD or you can use Visual studio terminal
       ```
      > dotnet ef migrations add InitialCreate --context StationsContext --output-dir Migration
      > dotnet ef database update
       ```
## How to Browse the api using POSTMAN?
### Copy and Pest the following Docker Container Image URL to your POSTMAN workspace envirnoment
    ...
    developer.e6a0f0heb6akgwef.swedencentral.azurecontainer.io/
    ...
### Then Copy and Pest the following frontend url:To Import data
        1. POST:  api/Dataimport
