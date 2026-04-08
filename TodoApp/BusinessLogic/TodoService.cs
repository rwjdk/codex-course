using System.ComponentModel.DataAnnotations;
using TodoApp.Data;
using TodoApp.Repositories;

namespace TodoApp.BusinessLogic;

public sealed class TodoService(TodoQuery todoQuery, TodoCommand todoCommand)
{
    public Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        todoQuery.GetAllAsync(cancellationToken);

    public Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        todoQuery.GetByIdAsync(id, cancellationToken);

    public async Task<TodoCommandResult> CreateAsync(TodoUpsertRequest request, CancellationToken cancellationToken = default)
    {
        TodoItem todo = CreateTodoItem(request);
        IReadOnlyList<string> validationErrors = Validate(todo);

        if (validationErrors.Count > 0)
        {
            return TodoCommandResult.Validation(validationErrors);
        }

        TodoItem createdTodo = await todoCommand.AddAsync(todo, cancellationToken);
        return TodoCommandResult.Success(createdTodo);
    }

    public async Task<TodoCommandResult> UpdateAsync(int id, TodoUpsertRequest request, CancellationToken cancellationToken = default)
    {
        TodoItem todo = CreateTodoItem(request);
        todo.Id = id;
        IReadOnlyList<string> validationErrors = Validate(todo);

        if (validationErrors.Count > 0)
        {
            return TodoCommandResult.Validation(validationErrors);
        }

        bool updated = await todoCommand.UpdateAsync(todo, cancellationToken);

        return updated
            ? TodoCommandResult.Success(todo)
            : TodoCommandResult.NotFound();
    }

    public async Task<TodoCommandResult> SetCompletedAsync(int id, bool completed, CancellationToken cancellationToken = default)
    {
        TodoItem? existingTodo = await todoQuery.GetByIdAsync(id, cancellationToken);

        if (existingTodo is null)
        {
            return TodoCommandResult.NotFound();
        }

        TodoUpsertRequest request =
            new()
            {
                Title = existingTodo.Title,
                StartDate = existingTodo.StartDate,
                DueDate = existingTodo.DueDate,
                Completed = completed
            };

        return await UpdateAsync(id, request, cancellationToken);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        todoCommand.DeleteAsync(id, cancellationToken);

    private static TodoItem CreateTodoItem(TodoUpsertRequest request) =>
        new()
        {
            Title = request.Title.Trim(),
            StartDate = request.StartDate?.Date,
            DueDate = request.DueDate?.Date,
            Completed = request.Completed
        };

    private static IReadOnlyList<string> Validate(TodoItem todo)
    {
        List<ValidationResult> validationResults = [];
        ValidationContext validationContext = new(todo);

        bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, validateAllProperties: true);

        return isValid
            ? []
            : validationResults
                .Select(result => result.ErrorMessage)
                .Where(message => !string.IsNullOrWhiteSpace(message))
                .Cast<string>()
                .ToArray();
    }
}
