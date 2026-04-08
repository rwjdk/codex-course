---
name: add-blazor-page
description: Repo-specific workflow for adding or updating a routable Blazor page in this BlazorApp solution. Use when creating a new page under `BlazorApp/Pages`, adding a new route, wiring page navigation, or following the repo's `.razor` plus optional `.razor.cs` and `.razor.css` page pattern.
---

# Blazor Page implementation details

- All GUI should be implemented with `MudBlazor`
- Use Tool `Context7` to gain information about `MudBlazor` NuGet Package
- Use Tool `MicrosoftLearn` to gain information about `Blazor`
- Add routable pages in the `BlazorApp/Pages` folder.
- Use one feature name across the page files. Template: `BlazorApp/Pages/<Feature>.razor`, optional `BlazorApp/Pages/<Feature>.razor.cs`, optional `BlazorApp/Pages/<Feature>.razor.css`.
- Prefer `@attribute [Route(PageRoutes.<Feature>)]` over inline route strings.
- Add new route constants in `BlazorApp/Constants.cs` under `PageRoutes` when a page needs a stable URL.
- Use `PageTitle` in the `.razor` file so browser title text stays explicit.
- Keep markup in the `.razor` file and move injected services, state, and event handlers into the `.razor.cs` partial class when the page has non-trivial behavior.
- Add a `.razor.css` file only when the page needs isolated styling that should not live in shared layout styles.
- Update `BlazorApp/Layout/MainLayout.razor` when the new page should be reachable from the app drawer navigation.
- Always Use MudBlazor components so the page matches the rest of the app shell.
- Always use code-behind files instead of the @code { ... } section at the bottom of pages
- Keep data access and orchestration in existing business or repository layers; the page should compose UI behavior rather than talk directly to EF Core.
