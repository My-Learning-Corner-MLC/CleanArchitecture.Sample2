# CleanArchitecture-Sample2
 Learn clean architecture by practicing


## Getting Started

Launch the app:
```bash
cd src/API
dotnet run
```


## Database

To add a new migration from the root folder:

```bash
dotnet ef migrations add MigrationName --project src\Infrastructure --startup-project src\API --output-dir Database\Migrations
```

To update database from the root folder:
```bash
dotnet ef database update --project src\Infrastructure --startup-project src\API
```

## Unit Test

To run all test cases:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

To generate test report:

```bash
reportgenerator -reports:"TestResults\**\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```



# Technical references
## 1. Clean architecture
## 2. CQRS pattern
## 3. Mediator pattern (MediatR)
## 4. Unit of work & generic repository
## 5. Problem detail


