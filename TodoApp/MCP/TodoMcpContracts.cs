namespace TodoApp.MCP;

public sealed record TodoMcpTask(
    int Id,
    string Title,
    DateTime? StartDate,
    DateTime? DueDate,
    bool Completed);

public sealed record TodoMcpCommandResponse(
    bool Succeeded,
    string Message,
    TodoMcpTask? Task = null,
    IReadOnlyList<string>? Errors = null);

public sealed record TodoMcpDeleteResponse(
    bool Succeeded,
    string Message,
    int Id);
