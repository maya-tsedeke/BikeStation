## City Bike Backend Sample Code in C# Using Clean Architecture structure:

### To get started, follow the below steps:

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
##### To download the project, launch Windows PowerShell and enter the following command line:
         git clone https://github.com/maya-tsedeke/BikeStation.git
        
#####     In the Project.Api (appsetting.json) file, update the SQL server Connection string and create a database (Name it StationDB) if you want to continue with my database connection. Or change it to match your database configuration (I used an Azure database. It will work until you evaluate the project).
       
      "ConnectionStrings":
      {
      "DefaultConnection": "Server=localhost,1433;Initial Catalog=StationDB;Persist Security Info=False;User
      ID=user DB userName;Password=yourPassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
      },
###     Run the following commands in the terminal after configuring your database, such as CMD or Visual Studio's terminal.
       ```
       dotnet ef migrations add InitialCreate --context StationsContext --output-dir Migration (If not working Use --> dotnet ef migrations add InitialCreate)
       dotnet ef database update
       ```
## How to Browse the api using POSTMAN?
### Copy and Pest the your Docker Container Image URL  to your POSTMAN workspace envirnoment
    eg. I am using this Host URL from Azure container instances {Host url}: developer.e6a0f0heb6akgwef.swedencentral.azurecontainer.io/ 
## Available api endpoints


| Web Method | API Endpoint URL              | Description                                   | Example: With Azure Container instances URL
| :----------| ----------------------------- | ----------------------------------------------| -----------------------------
| GET        |/api/Additional               | get all popular stations                       | {Host url}/api/Additional
| GET        |/api/Journeys                 | search the station given a search key          | {Host url}/api/Journeys/?SearchKey=Viiskulma
| Get        |/api/Additional/FilterByMonth | Filter by using Month and Year for Departure   |{Host url}/api/Additional/FilterByMonth?PageNumber=1&PageSize=10&Month=6&Year=2021
| POST       |/api/Journeys/Filter          | Used to filter by starting word and distance   | {Host url}/api/Journeys/Filter?MaxDistance=5000&MinDistance=1000&Name=Viisku&PageNumber=1&PageSize=10&OrderBy=duration
| POST       |/api/Station                  | Used to search departure station (Use POST)    | {Host url}/api/Station/Search?SearchKey=Viiskulma&PageNumber=1&PageSize=5
|POST        |/api/Dataimport               |Used to import CSV file data                    | {Host url}/api/Dataimport
|POST        |/api/Create                   | add a new station to DB                        | {Host url}/api/Create
| PUT        |/api/Update/id                | modify an existing journey in storage          | {Host url}/api/Update/5
| DELETE     |/api/Delete/id                | delete an existing journey with given id       | {Host url}/api/Delete/5
## How to use the local web api with or without Docker running:
When you're ready to start your Docker image or localhost, navigate to the Project.Api directory, Shift + right-click, choose Windows PowerShell, and then type the following commands into the console.

```
dotnet clean
dotnet build
docker build -t YourdockerUserName/solita .
doccker image ls
docker run -p 5001:80 dockerUserName/solita
```
Docker userName = The name of your Docker hub. 
You must log into Docker Desktop in order to launch a Docker image.
If you want to run in cloud first you should stop and delete dockerUserName/solita from docker desktop and then you can run again 

		docker run -p 5001:80 dockerUserName/solita
		
You must read "Unable to find image 'DockerUserName/solita:latest' locally!".Running the aforementioned command. Afterward, you can use the Docker Hub to locally run the Docker image. You may choose to host in the cloud. In my scenario, I made use of an Azure database and container instance.
[image](https://user-images.githubusercontent.com/32611349/184603495-99b59a07-1962-4ecc-bbf8-c1b776552518.png)
Before running in localhost, don't forget to build the StationDB database and change the URL's User ID and Password. Then, use the aforementioned commands to perform Migration and Update database.```


After that copy and pest your localhost url with its Posrt Number (5047/7014): example:[ http://localhost:7014/swagger/index.html] to run testing envirnoment.
```
Then use the local urls to access the aforementioned web API endpoints in the above table: The Api Controller will automatically load the endpoint URL if you are using the Swagger UI, there is no need to copy and pest the endpoint. 
```
### Use the following DOCKER running URL in any testing tools to run on my Azure container instance. To check whether the URL is functional or not, first utilize the following url in your computer browser. If the database connection is unsuccessful, please contact me at tsedeke2018@gmail.com or In the project comment line, type your remark.
        developer.e6a0f0heb6akgwef.swedencentral.azurecontainer.io/api/Additional
The result should be as follows:

            Code	Details
            200	
            Response body
            Download
            {
              "isSuccess": true,
              "message": "Successful",
              "top5PopularDepartureStationn": [
                {
                  "popular_Departure_stations": "DaadDaad",
                  "average_distance_startingFrom_station": 0,
                  "numberOfStartingJourney": 4
                },
                {
                  "popular_Departure_stations": "Kalevankatu",
                  "average_distance_startingFrom_station": 2256,
                  "numberOfStartingJourney": 2
                },
                {
                  "popular_Departure_stations": "Käpylän asema",
                  "average_distance_startingFrom_station": 3328,
                  "numberOfStartingJourney": 2
                },
                {
                  "popular_Departure_stations": "Linnanmäki",
                  "average_distance_startingFrom_station": 6592,
                  "numberOfStartingJourney": 2
                },
                {
                  "popular_Departure_stations": "Viiskulma",
                  "average_distance_startingFrom_station": 4327,
                  "numberOfStartingJourney": 2
                }
              ],
              "top5PopularReturnStationn": [
                {
                  "popular_return_stations": "",
                  "average_distance_ending_at_station": 0,
                  "numberOfEndingJourney": 6
                },
                {
                  "popular_return_stations": "Oulunkylän asema",
                  "average_distance_ending_at_station": 3328,
                  "numberOfEndingJourney": 2
                },
                {
                  "popular_return_stations": "RRRRRRR67",
                  "average_distance_ending_at_station": 18,
                  "numberOfEndingJourney": 2
                },
                {
                  "popular_return_stations": "Välimerenkatu",
                  "average_distance_ending_at_station": 2256,
                  "numberOfEndingJourney": 2
                },
                {
                  "popular_return_stations": "Melkonkuja",
                  "average_distance_ending_at_station": 2656,
                  "numberOfEndingJourney": 1
                }
              ]
            }
            Response headers
             content-type: application/json; charset=utf-8 
             date: Mon,15 Aug 2022 07:13:08 GMT 
             server: Kestrel 
             transfer-encoding: chunked 
            Responses
            Code	Description	Links
            200	
            Success
##### The database and URL are functional if this URL can successfully retrieve some data from the database in your browser. You can carry on testing with POSTMAN or any other testing software.
![image](https://user-images.githubusercontent.com/32611349/184603247-2399e410-b36c-44c1-86f4-3b147c67adbd.png)
##### To filter the database with Month, Year, and Pagenation, execute this URL next.
developer.e6a0f0heb6akgwef.swedencentral.azurecontainer.io/Additional/FilterByMonth?PageNumber=1&PageSize=10&Month=7&Year=2021
 Request: 
 
        {
          "pageNumber": 1,
          "pageSize": 10,
          "month": 7,
          "year": 2021
        }
        
Result:
![image](https://user-images.githubusercontent.com/32611349/184603940-86f89469-4172-4bc3-99f3-dda224c56e8b.png)

![image](https://user-images.githubusercontent.com/32611349/184604157-bdcfef41-8c83-4e11-91d6-d474cfbdba51.png)
You can use this to import data from a CSV file. developer.e6a0f0heb6akgwef.swedencentral.azurecontainer.io/api/Dataimport
 
 Result:
![image](https://user-images.githubusercontent.com/32611349/184642818-074d1e6d-b159-4b94-ae9b-23636801568d.png)
 
To Update thr return departure use the following endpoint in your url again.
![image](https://user-images.githubusercontent.com/32611349/184605370-d5065692-9331-446b-98be-bc962db2b04b.png)

        Request: 
        {
          "return_station_id": "0909",
          "return_station_name": "Kateskamentie",
          "covered_distance": 567,
          "duration": 0
        }
You can get in touch with me at any time, excluding Wednesday, Thursday, Friday, and Saturday, if there is any issue. 
##### NB: To incorporate Frontend I'm creating an android-based CRUD application. Within the next three days, I'll push.
