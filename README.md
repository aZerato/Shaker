# Shaker

Backend (check [Shaker-angular](https://github.com/aZerato/shaker-angular) for the frontend)

- Dotnet Core 3 application

## 01 - Presentation

> The user interface layer

- Basic MVC app for this Backend with Admin console

- SignalR : WebSocket (MessagePack)

- Rest API

- Identity implementation (with liteDB)
    - UserManager of Identity
    - Jwt Token Bearer (works with SignalR & Api parts)
    - Cookies auth for Admin

- The [Inversion Of Control](https://msdn.microsoft.com/en-us/library/ff921087.aspx).

- Dependancy Injection (DI) & [LifeTimeManager](https://msdn.microsoft.com/en-us/library/ff647854.aspx).

## 02 - Domain

> The data management layer

- Use "module" conception : services interfaces with their implementation (Module : aggregates/services [DDD])

- DTO : Data Transfer Object (sample: MyApp.Domain.DTO.SampleDataDTO).

- The [Specification Pattern](https://github.com/jnicolau/NSpecifications) (soon).

- "Select Builder" : Thanks to LINQ you are able to create custom expression for return directly an DTO (sample: MyApp.Domain.SampleModule.Aggregates.SampleDataSelectBuilder).

## 03 - Data

> The data access layer 

- [LiteDB](https://github.com/mbdavid/LiteDB) implementation

- The [Unit Of Work (UnitOfWork / UoW) Pattern](https://martinfowler.com/eaaCatalog/unitOfWork.html) add transactions for the resolution of concurrency problems (MyApp.Data.UnitOfWorkContext).

- The [Repository Pattern](https://msdn.microsoft.com/en-us/library/ff649690.aspx) add a layer between DbSet (return IQueryable) with UoW and the data used in "Domain" layer. 
Queryable manipulation is sensible, a Repository return an Enumerable or an Entity. (MyApp.Data.Core.Repository && MyApp.Domain.Core.IRepository)


## 04 - Infrastructure

> The app management layer

- CrossCutting : a layer can be used in all others.
