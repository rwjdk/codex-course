namespace TodoApp;

public static class PageRoutes
{
    public const string Home = "";
    public const string Error = "/Error";
    public const string NotFound = "/NoFound";
}

public static class ApiEndpoints
{
    public const string ApiRoot = "/api";
    public const string Todos = "/api/todos";
    public const string OpenApiDocument = "/openapi/{documentName}.json";
    public const string ScalarUi = ApiRoot;
    public const string Mcp = "/mcp";
}

public static class McpToolNames
{
    public const string GetTasks = "get_tasks";
    public const string AddTask = "add_task";
    public const string UpdateTask = "update_task";
    public const string RemoveTask = "remove_task";
}
