---
name: add-api-endpoint
description: Use when add/edit of API Endpoints
---

# API Endpoint implementation details

- Add API Endpoints should be placed in the API Folder
- Each file in the API represent an API Endpoint Group, so based on the requested endpoint add it to an existing group or create a new group
  - Template: `BlazorApp/API/<Feature>Api.cs`
- Reuse `ApiEndpoints.ApiRoot` and keep shared route strings centralized instead of scattering literal `"/api/..."` values.
- Add top-level route constants in `BlazorApp/Constants.cs` when a new API group needs a stable path.
- Keep route names plural when they represent collections. Example `ApiEndpoints.Todos`.
- Update `BlazorApp/MCP` only when the user explicitly asks for the same capability over MCP. A plain HTTP endpoint does not require MCP changes.
- Add request DTOs and command result types in `BlazorApp/BusinessLogic/<Feature>Contracts.cs`.
- Use data annotations on request DTOs when the endpoint accepts body input.
- Implement orchestration, validation, and mapping in `BlazorApp/BusinessLogic/<Feature>Service.cs`.
- Return a result record or entity value instead of `IResult` from the service layer.
- Use `MapGroup(...).WithTags(...)` so the endpoint appears under a coherent OpenAPI tag in Scalar.
- Accept services, request DTOs, primitive route values, and `CancellationToken` directly in handler parameters.
- Return `TypedResults` from handlers.
- Map each new API slice in `BlazorApp/Program.cs` with `app.Map<Feature>Api();`.
