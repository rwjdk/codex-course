using Microsoft.AspNetCore.Components;
using MudBlazor;
using TodoApp.BusinessLogic;
using TodoApp.Data;

namespace TodoApp.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    private TodoService TodoService { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    private MudDataGrid<TodoItem>? TodoGrid { get; set; }

    private List<TodoItem> Todos { get; set; } = [];

    private IEnumerable<TodoItem> VisibleTodos =>
        ShowCompletedTasks
            ? Todos
            : Todos.Where(todo => !todo.Completed);

    private TodoItem? PendingNewTodo { get; set; }

    private bool IsLoading { get; set; }

    private bool IsSaving { get; set; }

    private bool ShowCompletedTasks { get; set; }

    private string CompletedToggleLabel => ShowCompletedTasks ? "Hide Completed" : "Show Completed";

    private DialogOptions TodoDialogOptions { get; } =
        new()
        {
            CloseButton = true,
            CloseOnEscapeKey = true,
            FullWidth = true,
            MaxWidth = MaxWidth.Small
        };

    private int TotalCount => Todos.Count;

    private string NoRecordsMessage =>
        TotalCount == 0
            ? "No todos yet. Use the New task button to create the first one."
            : "No open todos. Turn on Show completed to view finished tasks.";

    protected override async Task OnInitializedAsync() => await LoadTodosAsync();

    private async Task LoadTodosAsync()
    {
        IsLoading = true;

        try
        {
            Todos = [.. await TodoService.GetAllAsync()];
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task OpenCreateDialogAsync()
    {
        if (TodoGrid is null)
        {
            return;
        }

        if (PendingNewTodo is not null)
        {
            await TodoGrid.SetEditingItemAsync(PendingNewTodo);
            return;
        }

        PendingNewTodo =
            new();

        Todos.Insert(0, PendingNewTodo);
        await InvokeAsync(StateHasChanged);
        await TodoGrid.SetEditingItemAsync(PendingNewTodo);
    }

    private async Task OpenEditDialogAsync(TodoItem todo)
    {
        if (TodoGrid is null)
        {
            return;
        }

        await TodoGrid.SetEditingItemAsync(todo);
    }

    private async Task<DataGridEditFormAction> CommitItemChangesAsync(TodoItem item)
    {
        IsSaving = true;

        try
        {
            TodoCommandResult result = item.Id == 0
                ? await TodoService.CreateAsync(CreateRequest(item))
                : await TodoService.UpdateAsync(item.Id, CreateRequest(item));

            if (result.Status == TodoCommandStatus.ValidationFailed)
            {
                Snackbar.Add(string.Join(" ", result.Errors ?? []), Severity.Error);
                return DataGridEditFormAction.KeepOpen;
            }

            if (result.Status == TodoCommandStatus.NotFound)
            {
                Snackbar.Add("That task no longer exists.", Severity.Warning);
                PendingNewTodo = null;
                await LoadTodosAsync();
                return DataGridEditFormAction.Close;
            }

            Snackbar.Add(item.Id == 0 ? "Task created." : "Task updated.", Severity.Success);

            PendingNewTodo = null;
            await LoadTodosAsync();
            return DataGridEditFormAction.Close;
        }
        finally
        {
            IsSaving = false;
        }
    }

    private async Task ToggleCompletedAsync(TodoItem todo)
    {
        TodoCommandResult result = await TodoService.SetCompletedAsync(todo.Id, !todo.Completed);

        if (result.Status == TodoCommandStatus.NotFound)
        {
            Snackbar.Add("That task could not be updated.", Severity.Warning);
            await LoadTodosAsync();
            return;
        }

        await LoadTodosAsync();
        Snackbar.Add(result.Todo?.Completed == true ? "Task marked completed." : "Task reopened.");
    }

    private async Task DeleteTodoAsync(int id, string title)
    {
        bool? confirmed = await DialogService.ShowMessageBoxAsync(
            "Delete task",
            $"Delete '{title}' from the TodoApp database?",
            yesText: "Delete",
            cancelText: "Cancel");

        if (confirmed != true)
        {
            return;
        }

        bool deleted = await TodoService.DeleteAsync(id);

        if (!deleted)
        {
            Snackbar.Add("That task was already removed.", Severity.Info);
            await LoadTodosAsync();
            return;
        }

        if (PendingNewTodo is not null && PendingNewTodo.Id == id)
        {
            PendingNewTodo = null;
        }

        await LoadTodosAsync();
        Snackbar.Add("Task deleted.", Severity.Warning);
    }

    private Task HandleCanceledEditingItem(TodoItem item)
    {
        if (PendingNewTodo is not null && PendingNewTodo.Id == 0 && item.Id == 0)
        {
            Todos.Remove(PendingNewTodo);
            PendingNewTodo = null;
            return InvokeAsync(StateHasChanged);
        }

        return Task.CompletedTask;
    }

    private static TodoUpsertRequest CreateRequest(TodoItem item) =>
        new()
        {
            Title = item.Title,
            StartDate = item.StartDate,
            DueDate = item.DueDate,
            Completed = item.Completed
        };
}
