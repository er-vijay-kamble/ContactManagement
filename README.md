# ContactManagement

## Introduction

This is a web application which can be used to manage contact details.
User can create, update, delete and view the contacts through this web application.

## Getting Started

### Pre-requisites
  * Visual Studio
  * .Net core 2.2 need to be install.
  * Git repo: https://github.com/er-vijay-kamble/ContactManagement

### Installing

  ##### Database
  * Install Microsoft SQL Server. Please find more information [here](https://docs.microsoft.com/en-gb/sql/database-engine/install-windows/install-sql-server?view=sql-server-2017).
  * Create new table by executing [this](https://github.com/er-vijay-kamble/ContactManagement/blob/dev/Data/ContactTable.sql) script on your desired database.
  * Please update your config files in code(described below) as follows,
	* "DefaultConnection": "server=_your_server_name_;database=_your_database_name_;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;Trusted_Connection=true"
  
  ##### Code
  * Clone the git repository
  * Create a branch for your changes.
  * Open ContactManagement.sln
  * Set ContactManagement.Web.Client, ContactManagement.Web.Api, or both, as your startup project(s).
  * Set your app settings in the config files
  * Click Debug
  
### Running the tests
  * The tests for this solution are located in ContactManagement.Tests project.
  * Build the solution.
  * Open Visual Studio's Test Explorer.
  * Run tests as desired. 

### Hosting
  * Build all the projects and run ContactManagement solution. 
  * Then host it in IIS or run it using IISExpress. If you are unaware of hosting please take a look at [MSDN](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/visual-studio-web-deployment/deploying-to-iis).
  * **Note:**
      *To run ContactManagement.Web.Client, ContactManagement.Web.Api should be running.

### Logs
  * Web API
    * C:\Logs\ContactManagement.Web.Api__date__.serilog
  * Web Client
    * C:\Logs\ContactManagement.Web.Client__date__.serilog

## Technology stack
  * Tried to Achieve micro-service architecture
  * Used EntityFrameworkCore by implemented Repository Pattern.  
  * Resolved dependencies using Microsoft.Extensions.DependencyInjection.
  * Configured Swagger for ContactManagement.Web.Api (url: https://localhost/ContactManagement.Web.Api/swagger/index.html)
  * Used Serilog and configured file logging for 
			1. ContactManagement.Web.Api
			2. ContactManagement.Web.Client
  * AutoMapper for mapping profiles
  * Make used of 
			* ExceptionFilterAttribute
			* ActionFilterAttribute
			* Mvc DataAnnotations
  * Used XUnit for Unit test cases
  
## TO DO:
  * Pagination in Web.Clent
  * Add database deploy project
  * Authentication 