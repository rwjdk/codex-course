---
name: add-mcp-tool
description: Use when add/edit of MCP Tools
---

# MCP Tool implementation details

- Add MCP tool implementations in the `BlazorApp/MCP` folder.
- Add a tool to an existing `BlazorApp/MCP/<Feature>McpTools.cs` file when it belongs to the same feature; create a new `<Feature>McpTools.cs` file when introducing a new MCP feature surface.
- Keep MCP-specific response and payload records in `BlazorApp/MCP/<Feature>McpContracts.cs`.
- Mark each tool container with `[McpServerToolType]`.
- Add `[McpServerTool(Name = McpToolNames.<ToolName>)]` to each exposed tool method and include a clear `Description(...)` for the tool and its parameters.
- Keep stable MCP tool names centralized in `BlazorApp/Constants.cs` under `McpToolNames` instead of scattering string literals across tool classes.
- Accept app services, simple tool inputs, and `CancellationToken` directly in MCP tool method parameters.
- Use the existing business logic services for validation and orchestration instead of duplicating domain rules inside MCP tools.
- Map entities and command results into MCP-specific contract records before returning them from tool methods.
- Return typed values such as records, primitives, or `IReadOnlyList<T>` from tool methods instead of HTTP-specific results.
- Register each new MCP tool class in `BlazorApp/Program.cs` with `.WithTools<<Feature>McpTools>()`.
- Update `BlazorApp/API` only when the user explicitly asks for the same capability over HTTP. A plain MCP tool does not require API changes.
