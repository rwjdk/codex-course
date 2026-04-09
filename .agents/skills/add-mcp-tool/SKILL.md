---
name: add-mcp-tool
description: Use when add/edit of MCP Tools
---

# MCP Tool implementation details

- Add MCP tool implementations in the `TodoApp/MCP` folder.
- Add a tool to an existing `TodoApp/MCP/<Feature>McpTools.cs` file when it belongs to the same feature; create a new `<Feature>McpTools.cs` file when introducing a new MCP feature surface.
- Keep MCP-specific response and payload records in `TodoApp/MCP/<Feature>McpContracts.cs`.
- Mark each tool container with `[McpServerToolType]`.
- Add `[McpServerTool(Name = McpToolNames.<ToolName>)]` to each exposed tool method and include a clear `Description(...)`.
- Keep MCP tool names centralized in `TodoApp/Constants.cs` under `McpToolNames` instead of magic strings.
- Use the existing business logic services for validation and orchestration instead of duplicating domain rules inside MCP tools.
- Map entities and command results into MCP-specific contract records before returning them from tool methods.
- Return typed values such as records, primitives, or `IReadOnlyList<T>` from tool methods instead of HTTP-specific results.
- Register each new MCP tool class in `TodoApp/Program.cs` with `.WithTools<<Feature>McpTools>()`.
- Update `TodoApp/API` only when the user explicitly asks for the same capability over HTTP.
