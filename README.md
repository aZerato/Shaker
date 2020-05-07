# Shaker

Backend (check [Shaker-angular](https://github.com/aZerato/shaker-angular) for the frontend)

- Dotnet Core 3 application

## 01 - Presentation

> The user interface layer

- MVC app for this Backend with Admin console

- [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) : WebSocket ([MessagePack](https://msgpack.org/))

- Rest API

- [Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio) implementation (with liteDB)
    - UserManager of Identity
    - Jwt Token Bearer (works with SignalR & Api parts)
    - Cookies auth for Admin

- The [Inversion Of Control](https://msdn.microsoft.com/en-us/library/ff921087.aspx).

- Dependancy Injection (DI) & [LifeTimeManager](https://msdn.microsoft.com/en-us/library/ff647854.aspx).

## 02 - Domain

> The data management layer

- The global app logic

- DTO : Data Transfer Object (sample: shaker.domain.dto).

## 03 - Data

> The data access layer 

- [LiteDB](https://github.com/mbdavid/LiteDB) implementation

- The [Unit Of Work (UnitOfWork / UoW) Pattern](https://martinfowler.com/eaaCatalog/unitOfWork.html) add transactions for the resolution of concurrency problems (shaker.data.core.IUnitOfWork).

- The [Repository Pattern](https://msdn.microsoft.com/en-us/library/ff649690.aspx) add a layer between DbSet (return IQueryable) and the data used in "Domain" layer. 
Queryable manipulation is sensible, a Repository return an Enumerable or an Entity. (shaker.data.core.Repository)


## 04 - Infrastructure

> The app management layer

- CrossCutting : a layer can be used in all others.
