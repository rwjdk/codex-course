---
name: add-repository-query-command
description: Use when add/edit of Repository Queries or Commands
---

# Repository Query and Command implementation details

- Add repository types in the `BlazorApp/Repositories` folder.
- Keep read operations in `BlazorApp/Repositories/<Feature>Query.cs` and write operations in `BlazorApp/Repositories/<Feature>Command.cs`.
- Add methods to an existing repository pair when the data access belongs to the same feature; create a new query/command pair when introducing a new feature.
- Inject `IDbContextFactory<TodoDbContext>` into repository primary constructors.
- Create a fresh `TodoDbContext` inside each repository method with `CreateDbContextAsync(cancellationToken)`.
- Use `AsNoTracking()` for query methods unless the method must intentionally return tracked entities from the same context.
- Keep filtering, ordering, includes, and projection in the query layer.
- Keep inserts, updates, and deletes in the command layer and call `SaveChangesAsync(cancellationToken)` after the mutation work is complete.
- For update and delete operations, load the current entity first and return a clear result such as `bool` when the target record does not exist.
- Return entities, projections, or simple result values from repositories instead of HTTP-specific types.
- Keep validation, request mapping, and multi-step orchestration in `BlazorApp/BusinessLogic/<Feature>Service.cs`.
- Register each new repository in `BlazorApp/Program.cs` with `builder.Services.AddScoped<<Feature>Query>();` and `builder.Services.AddScoped<<Feature>Command>();`.
