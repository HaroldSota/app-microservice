# app-weather-microservice

This is a simplified version of https://github.com/HaroldSota/app-weather/tree/master/app-api.
In order to scale from clean arthitecture to a microservice arthitecture the following changes are made:

1. Replace the Message Bus infrastructure with MediatR
2. Remove interface segregation for RestClient
3. Remove the bootstrapping strategy per layer
4. Layers defined in different projects now are located in Api project
5. Moved the mapping for persistence entities in DBContext
6. Added caching for query in external services 
7. Added Swagger for manual testing


# The app desing
 The project has 4 abstract layers: api, domain, external serices and persistence.

 Each api controller inherit from BaseApiController. In BaseApiController it is defined the routing, and how to interpret the 
 response from the handler in terms of HTTP Status.
   
 Each controller action is sent to handler via the MediatR bus, the handler defines the logic to be performed, and validations per
 request. The handler and the api model for each action are located at AppWeather.Api.Messaging.
 
 Each external service is located at AppWeather.Api.ExternalServices, the service class inherits from RestClient (responsible to wrap
 the logic for rest calls), and each response is a ApiResponse object. ApiResponse it is used to avoid handling the exceptions through
 try/catch but in a linear execution. The external serices enpoing resources, content type are defined in the configuration file. Each
 service has a service interface in order to be used in DI in a conventional manner. 
 
 In the persistence layer it is used the repository pattern, each repository inherit from BaseRepository. The BaseRepository contains
 the basic operations (since it is required only to insert and read data from the database). 


 # Setup
 To run the application, you need:

 1. Create a MS SQL database schema
 2. Set the connection string in the config file 'app-api/Presentation/AppWeather.Api/appsettings.json'.
 3. In Package Manager Console chose the AppWeather.Api project and run the commands:
     >Add-Migration InitialCreate
     
     >Update-Database

 4. To run unit testing you can use the EF Core in-memory database provider or your testing db, the default configuration is to use in
     memory database, but you can change it at 'app-api/Tests/AppWeather.Tests/appsettings.Tests.json' by changing IsTesting to false.

