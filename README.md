This lab was very interesting in the material it include. In this readme i will summarize all what i did in the lab.
**Lab 5 submission**
**Instructur**: Andrea Jabbour
# University Management System in ASP.NET CORE (Lab 5)

## Overview

In this lab the required is to design a University Management System in ASP.NET CORE that applies the concepts of Domain-Driven Design (DDD), which in summary focus on making applications maintainable, modular, and apply concept-seperation perfectly. 

## Project Structure

### Domain Layer
The Domain was holding all entities that was migrated to the db (here im using PostgreSQL, running in a docker container) and it do not communicate with any other layer.

### Application Layer
The Application layer came in the outer region of the domain layer, and with more breadth capabilities, it can communicate with domain, persistence, and infra layers. In the application i included a handler folder containing command and queries folders. This approach is known as CQRS, which seperate Get method from Post, Delete, and Put methods, following the seperation of concerns concept.

### Persistence Layer
After this layer, the Persistence layer is created, which is responsible of handling DB calls via UniversityDbContext class, as will as handling caching (using redis here) and migrations.

### Presentation Layer
Finally, comes the Presentation layer, which is the highest level that the user see and interact with. This layer only refer to the application layer and can request db calls onlly through it.

## Features Implemented

### OData Filtering
OData is applied as a seperate filtering controller, and the queries can be applied via the Route as we learned.

### Teacher Grading Privilege
The teacher grading previlige was applied simply by adding a grade attribute to student entity, and the teacher will add a grade out of 20 that will count on the student grade as average grade each time the teacher adds a grade.

### Profile Picture Upload
The last requirement, is to implement an endpoint to post a profile pic for users, which is applied using best-practice approaches, and the image is saved in wwwroot as required.

## Conclusion
In conclusion I have fully learned how to implement DDD applications, and leverage libraries as MediatR to facilitate secure and seperate communication between layers. In addition, i have applied all what i learned in previous labs and overcome the problems that i have previously faced.

## **Notes**
- `IFormFile` type caused an error when attempting to save in the database, so it is **ignored**.
- **Redis** and **PostgreSQL** images were **pulled from the official Docker website**.

## **References**
1. [Stack Overflow - Newtonsoft.Json Deserialize Issue](https://stackoverflow.com/questions/41272524/newtonsoft-json-deserialize-issue-error-converting-value-to-type)  
2. [Redis Caching in .NET Core](https://redis.io/learn/develop/dotnet/aspnetcore/caching/basic-api-caching)  
3. [Redis Cache in .NET Core - Beginner’s Guide](https://dotnetfullstackdev.medium.com/redis-cache-in-net-core-a-beginners-end-to-end-guide-8584379f6f0e)  
4. [Scoped Services in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service)  
5. [CQRS - Command Query Responsibility Segregation](https://www.geeksforgeeks.org/cqrs-command-query-responsibility-segregation/)  


