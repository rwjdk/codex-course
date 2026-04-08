using TodoApp.BusinessLogic;
using TodoApp.Data;

namespace TodoApp.API;

public static class TodoApi
{
    public static IEndpointRouteBuilder MapTodoApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup(ApiEndpoints.Todos)
            .WithTags("Todos");

        group.MapGet("/", GetTodosAsync);
        group.MapGet("/{id:int}", GetTodoByIdAsync);
        group.MapPost("/", CreateTodoAsync);
        group.MapPut("/{id:int}", UpdateTodoAsync);
        group.MapDelete("/{id:int}", DeleteTodoAsync);

        return endpoints;
    }

    private static Task<IReadOnlyList<TodoItem>> GetTodosAsync(TodoService todoService, CancellationToken cancellationToken) =>
        todoService.GetAllAsync(cancellationToken);

    private static async Task<IResult> GetTodoByIdAsync(int id, TodoService todoService, CancellationToken cancellationToken)
    {
        TodoItem? todo = await todoService.GetByIdAsync(id, cancellationToken);
        return todo is null ? TypedResults.NotFound() : TypedResults.Ok(todo);
    }

    private static async Task<IResult> CreateTodoAsync(TodoUpsertRequest request, TodoService todoService, CancellationToken cancellationToken)
    {
        TodoCommandResult result = await todoService.CreateAsync(request, cancellationToken);
        return ToHttpResult(result, todo => TypedResults.Created($"{ApiEndpoints.Todos}/{todo.Id}", todo));
    }

    private static async Task<IResult> UpdateTodoAsync(int id, TodoUpsertRequest request, TodoService todoService, CancellationToken cancellationToken)
    {
        TodoCommandResult result = await todoService.UpdateAsync(id, request, cancellationToken);
        return ToHttpResult(result, TypedResults.Ok);
    }

    private static async Task<IResult> DeleteTodoAsync(int id, TodoService todoService, CancellationToken cancellationToken)
    {
        bool deleted = await todoService.DeleteAsync(id, cancellationToken);
        return deleted ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    private static IResult ToHttpResult(TodoCommandResult result, Func<TodoItem, IResult> onSuccess)
    {
        if (result is { Succeeded: true, Todo: not null })
        {
            return onSuccess(result.Todo);
        }

        if (result.Status == TodoCommandStatus.ValidationFailed)
        {
            Dictionary<string, string[]> errors = new()
            {
                ["todo"] = [.. (result.Errors ?? [])]
            };

            return TypedResults.ValidationProblem(errors);
        }

        return TypedResults.NotFound();
    }
}
