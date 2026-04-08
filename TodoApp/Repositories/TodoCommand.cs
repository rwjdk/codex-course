using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Repositories;

public sealed class TodoCommand(IDbContextFactory<TodoDbContext> dbContextFactory)
{
    public async Task<TodoItem> AddAsync(TodoItem todoItem, CancellationToken cancellationToken = default)
    {
        await using TodoDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        dbContext.TodoItems.Add(todoItem);
        await dbContext.SaveChangesAsync(cancellationToken);

        return todoItem;
    }

    public async Task<bool> UpdateAsync(TodoItem todoItem, CancellationToken cancellationToken = default)
    {
        await using TodoDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        TodoItem? existingTodo = await dbContext.TodoItems
            .SingleOrDefaultAsync(existing => existing.Id == todoItem.Id, cancellationToken);

        if (existingTodo is null)
        {
            return false;
        }

        existingTodo.Title = todoItem.Title;
        existingTodo.StartDate = todoItem.StartDate;
        existingTodo.DueDate = todoItem.DueDate;
        existingTodo.Completed = todoItem.Completed;

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await using TodoDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        TodoItem? existingTodo = await dbContext.TodoItems
            .SingleOrDefaultAsync(existing => existing.Id == id, cancellationToken);

        if (existingTodo is null)
        {
            return false;
        }

        dbContext.TodoItems.Remove(existingTodo);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
