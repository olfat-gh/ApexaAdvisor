## Apexa Advisor project
This project is an implementation of the Clean Architecture with CQRS Pattern using .NET 8. 
* Clean Architecture is an architecture pattern aimed at building applications that we can maintain, scale, and test easily by separating the application into different layers that have distinct responsibilities: Domain, Application, Infrastructure and Presentation Layer.
 
* CQRS (Command Query Responsibility Segregation) Pattern is a design pattern for segregating different responsibility types in a software application. The basic idea behind CQRS is to split an application’s operations into two groups:
    * Commands, which are responsible for changing the state of the application
    * Queries, which retrieve data without changing any state

## Business Requirements
**1. Create Advisor**\
Should be able to create and persist an Advisor in the system with the following fields:
```
Name (Length: Max 255 | Required)
SIN (Length: Exactly 9 | Required | Masked | Unique)
Address (Length: Max 255 | Optional)
Phone (Length: Exactly 8 | Optional | Masked)
Health Status (Green/Yellow/Red) - Randomly generated in the backend with the probabilities: Green=60% Yellow = 20% Red =20%)
```
**2. Get, Update, Delete, List Advisors**
```
Should be able to retrieve an Advisor from the system
Should be able to update an Advisor in the system
Should be able to delete an Advisor from the system
Should be able to list all Advisors in the system
```
**3. Cache**\
Implement a caching data structure. The cache must follow the constraints of a Most Recently Used cache . Implement the class:
```
Initialize - Takes in capacity as a parameter. Default to 5
Get - Return the value of the key if the key exists
Put - Update the value of the key if the key exists. Otherwise, add the key-value pair to the cache. If the number of keys exceeds the capacity from this operation, evict the most recently used key.
Delete – Remove key and value from cache
```
All functions must each run in O(1) average time complexity\
Note: keep in mind that each operation affects recency\
**Update the CRUD operations to leverage this new data structure to sit in front of the DB using any of the common caching patterns**

## Getting Started
### Prerequisites

* .NET 8 SDK
* Visual Studio 2022 or later
### Running

* First run the web api solution => Apexa.AdvisorApp.WebApi
* Then run client app at the ApexaAdvisor\src\UI\clientapp :
```csharp
npm install 
npm start 
```

## Technologies Used:
**Design**
* Single-Page Application
* Web + API
* Clean Architecture
  
**Backend**
* .NET 8 | ASP.NET Core | C# 12
* EF Core 8 for Data Access/ORM
* xUnit for Testing 
  
**Frontend**
* React & Typescript
  
**Database**
* In-Memory DB Persistence

<img width="1500" height="750" src=https://raw.githubusercontent.com/olfat-gh/ApexaAdvisor/main/img/api.png>
<img width="1500" height="750" src=https://raw.githubusercontent.com/olfat-gh/ApexaAdvisor/main/img/ui.png>

