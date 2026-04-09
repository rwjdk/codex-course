---
name: add-blazor-page
description: Use when adding or updating a Blazor page.
---

# Blazor Page implementation details
- All GUI should be implemented with `MudBlazor`
- Add routable pages in the `TodoApp/Pages` folder.
- Use <feature-name> across the page files (`TodoApp/Pages/<Feature>.razor` + `TodoApp/Pages/<Feature>.razor.cs`).
- Add new route constants in `TodoApp/Constants.cs` under `PageRoutes` for the page URL.
- Prefer `@attribute [Route(PageRoutes.<Feature>)]` over inline route strings.
- Always use code-behind files instead of the @code { ... } section at the bottom of pages
- Update `TodoApp/Layout/MainLayout.razor` when the new page should be reachable from the app drawer navigation (Default True).
- Keep data access and orchestration in existing business or repository layers; the page should compose UI behavior rather than talk directly to EF Core.

# Blazor Information
- Use Tool `MicrosoftLearn` to gain information about `Blazor`

# MudBlazor Information
- Use Tool `Context7` to gain information about `MudBlazor` NuGet Package
