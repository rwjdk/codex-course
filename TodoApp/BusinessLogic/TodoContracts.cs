using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TodoApp.Data;

namespace TodoApp.BusinessLogic;

public sealed class TodoUpsertRequest
{
    [Required]
    [StringLength(120)]
    public string Title { get; init; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? StartDate { get; init; }

    [DataType(DataType.Date)]
    public DateTime? DueDate { get; init; }

    [DefaultValue(false)]
    public bool Completed { get; init; } = false;
}

public enum TodoCommandStatus
{
    Success,
    ValidationFailed,
    NotFound
}

public sealed record TodoCommandResult(
    TodoCommandStatus Status,
    TodoItem? Todo = null,
    IReadOnlyList<string>? Errors = null)
{
    public bool Succeeded => Status == TodoCommandStatus.Success;

    public static TodoCommandResult Success(TodoItem todo) =>
        new(TodoCommandStatus.Success, todo, []);

    public static TodoCommandResult Validation(IReadOnlyList<string> errors) =>
        new(TodoCommandStatus.ValidationFailed, null, errors);

    public static TodoCommandResult NotFound() =>
        new(TodoCommandStatus.NotFound, null, []);
}
