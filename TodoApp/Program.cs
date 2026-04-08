using BlazorApp;
using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using Scalar.AspNetCore;
using TodoApp;
using TodoApp.API;
using TodoApp.BusinessLogic;
using TodoApp.Data;
using TodoApp.Layout;
using TodoApp.MCP;
using TodoApp.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices(configuration =>
{
    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});
builder.Services.AddOpenApi();
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<TodoMcpTools>();
builder.Services.AddDbContextFactory<TodoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TodoApp")));
builder.Services.AddScoped<TodoQuery>();
builder.Services.AddScoped<TodoCommand>();
builder.Services.AddScoped<TodoService>();
WebApplication app = builder.Build();

await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
{
    IDbContextFactory<TodoDbContext> dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TodoDbContext>>();
    await using TodoDbContext dbContext = await dbContextFactory.CreateDbContextAsync();
    await dbContext.Database.MigrateAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(PageRoutes.Error, createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute(PageRoutes.NotFound, createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapOpenApi();
app.MapMcp(ApiEndpoints.Mcp);
app.MapScalarApiReference(ApiEndpoints.ScalarUi, options =>
{
    options.WithTitle("Todo API")
        .WithOpenApiRoutePattern(ApiEndpoints.OpenApiDocument);
});
app.MapTodoApi();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
