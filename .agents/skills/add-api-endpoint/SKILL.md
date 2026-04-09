---
name: add-api-endpoint
description: Use when add/edit of API Endpoints
---

# API Endpoint implementation details

- Add API Endpoints should be placed in the `TodoApp/API` Folder
- Each file in the API represents an API Endpoint Group, so based on the requested endpoint add it to an existing group or create a new group (`TodoApp/API/<Feature>Api.cs`)
- Reuse `ApiEndpoints.ApiRoot` and keep shared route strings centralized instead of magic strings.
- Add top-level route constants in `TodoApp/Constants.cs` when a new API group needs a stable path.
- Keep route names plural when they represent collections. Example `ApiEndpoints.Todos`.
- Update `TodoApp/MCP` only when the user explicitly asks for the same capability over MCP.
- Add request DTOs and command result types in `TodoApp/BusinessLogic/<Feature>Contracts.cs`.
- Use data annotations on request DTOs when the endpoint accepts body input.
- Implement orchestration, validation, and mapping in `TodoApp/BusinessLogic/<Feature>Service.cs`.
- Return a result record or entity value instead of `IResult` from the service layer.
- Use `MapGroup(...).WithTags(...)` so the endpoint appears under a coherent OpenAPI tag in Scalar.
- Return `TypedResults` from handlers.
- Map each new API slice in `TodoApp/Program.cs` with `app.Map<Feature>Api();`.
