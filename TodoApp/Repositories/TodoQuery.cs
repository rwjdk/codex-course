using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Repositories;

public sealed class TodoQuery(IDbContextFactory<TodoDbContext> dbContextFactory)
{
    public async Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using TodoDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.TodoItems
            .AsNoTracking()
            .OrderBy(todo => todo.Completed)
            .ThenBy(todo => todo.DueDate ?? DateTime.MaxValue)
            .ThenBy(todo => todo.StartDate ?? DateTime.MaxValue)
            .ThenBy(todo => todo.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using TodoDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.TodoItems
            .AsNoTracking()
            .SingleOrDefaultAsync(todo => todo.Id == id, cancellationToken);
    }
}
