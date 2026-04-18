---
name: add-repository-query-command
description: Use when add/edit of Repository Queries or Commands
---

# Repository Query and Command implementation details

- Add repository types in the `TodoApp/Repositories` folder.
- Keep read operations in `TodoApp/Repositories/<Feature>Query.cs` and write operations in `TodoApp/Repositories/<Feature>Command.cs`.
- Add methods to an existing repository pair when the data access belongs to the same feature; create a new query/command pair when introducing a new feature.
- Inject `IDbContextFactory<TodoDbContext>` into repository primary constructors.
- Create a fresh `TodoDbContext` inside each repository method with `CreateDbContextAsync(cancellationToken)`.
- Use `AsNoTracking()` for query methods unless the method must intentionally return tracked entities from the same context.
- Keep filtering, ordering, includes, and projection in the query layer.
- Keep validation, request mapping, and multi-step orchestration in `TodoApp/BusinessLogic/<Feature>Service.cs`.
- Register each new repository in `TodoApp/Program.cs` with `builder.Services.AddScoped<<Feature>Query>();` and `builder.Services.AddScoped<<Feature>Command>();`.
