# IMPORTANT
Whenever a new conversation begins do NEVER just start to code (There is no other exceptions to this rule!) - Instead do the following steps:
1. Find `Available $skills` and `Available information tools` that are relevant for the task and read their details.
2. Investigate the code-base for existing implementation details.
3. Think about the question/task, and what might be unclear for the implementation.
4. Finally ask the user 3-10 clarifying questions in a **numbered list** (NEVER PRESENT THEM AS JUST BULLETS; EACH QUESTION NEED A NUMBER). 

Once these questions are answered, you can begin coding (for follow-ups, you should not do this)

---

# Project details
- OS: `Microsoft Windows 11`
- Command Line Tools: `codex`, `dotnet`, `git`, `python` and `powershell` [Other tools not allowed]
- Target Framework: `.NET 10.0` and `C# 14.0` [Downgrade NOT allowed | Use latest C# features]
- Type: `ASP.NET Core Blazor Web App`
- API style: `Minimal API`
- API docs: `OpenAPI` with `Scalar`
- MCP: `ModelContextProtocol.AspNetCore` over HTTP
- Database: `SQLite`
- Todo items persist a `CompletedAt` timestamp so the Home page can show completion stats by month and in total
- UI Framework: `Blazor Web App` with interactive server components [not WASM, prerender disabled, global interactivity]
- UI Component Libs: `MudBlazor`
- ORM: `Entity Framework Core`
- Code Style: `Warnings treated as errors`, `EditorConfig` enforced

# Build and Run Commands
- Build: `dotnet build TodoApp.slnx`
- Run App: `dotnet run --project TodoApp\TodoApp.csproj`
- Add NuGet Package: `dotnet add TodoApp\TodoApp.csproj package <PackageName>`

# Solution structure
```text
codex-course
|-- TodoApp/           # Main ASP.NET Core Blazor Web App project
|   |-- API/             # Minimal API endpoint mappings
|   |-- BusinessLogic/   # Request models, command results, and domain services
|   |-- Data/            # EF Core entity types and DbContext
|   |   \-- Migrations/  # EF Core SQLite migrations and model snapshot
|   |-- Layout/          # App shell, routing, navigation, and reconnect UI
|   |-- MCP/             # MCP contracts and tool implementations
|   |-- Pages/           # Routable Blazor pages
|   |-- Repositories/    # Query and command data access over EF Core
|   \-- wwwroot/         # Static web assets
```
- Note: The structure above intentionally includes only source-code folders. It omits repository metadata, tooling files, IDE state, build artifacts, runtime database files, launch-profile folders, and verification/output folders such as `.git`, `.github`, `.vs`, `build-verify`, `TodoApp\bin`, `TodoApp\obj`, and `TodoApp\Properties`.

# Code Generation
- At the end of code generation, make sure changes are reflected in Agents.md and if it is a bigger change in the README.md

# Code Generation Rules
- Always enforce `.editorconfig` rules.
- Use `CRLF` line endings.
- Follow standard .NET naming conventions (naming violations are treated as errors).
- Never use NuGet packages that are not MIT or Apache 2 unless specifically instructed to.
- Always use braces, even if it is not technically needed (example: Add braces for an if statement with a single line of code inside it).
- When initializing collections, prefer `[]` instead of `new List<>()` or `Array.Empty<>` (example: `[]` instead of `Array.Empty<Car>()` and `new List<Car>()`).
- Never use the `var` keyword (always use explicit types).
- Always use primary constructors over regular constructors.
- Always use simplified `new()` over explicit construction when the type is apparent (example: `Car c = new();` instead of `Car c = new Car();`).
- When the created type is not evident, use an explicit type in `new` expressions.
- Convert properties to auto-properties when a manual backing field is not needed.
- Avoid duplicated sequential `if` branches; merge or refactor them into a single branch.
- Merge compatible null/value/pattern checks into logical patterns using `or` / `and` when possible.
- Do not require `this.` qualification for fields, properties, methods, or events.
- Always make namespaces match the folder structure.
- Private fields should be prefixed with `_` to limit the need for `this`.
- Always place private classes at the bottom of parent classes.
- Always make methods that have the `async` keyword use the `Async` suffix (example: correct `async Task SaveAsync`, wrong `async Task Save`).
- Clean up temporary files you create while coding.
