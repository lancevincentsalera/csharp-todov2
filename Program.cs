using Microsoft.EntityFrameworkCore;
using TodoRestApiV2.Data;
using TodoRestApiV2.Data.Interfaces;
using TodoRestApiV2.Data.Repository;
using TodoRestApiV2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddDbContext<TodoRestApiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/todoItems", async (ITodoItemRepository _repository) => await _repository.GetTodoItemsAsync());
app.MapPost("/api/add/todoItem", async (ITodoItemRepository _repository, AddTodoItem todoItem) =>
{
    var todo = new TodoItem { Todo = todoItem.Todo };
    await _repository.AddTodoItemAsync(todo);

    return Results.Created($"/api/todoItems/{todo.Id}", new TodoItemResponse(todo.Todo, todo.IsComplete));
});
app.MapPut("/api/update/todoItem/{id}", async (ITodoItemRepository _repository, int id, UpdateTodoItem todoItem) =>
{
    var todo = await _repository.FindTodoItemAsync(id);
    if (todo == null) return Results.NotFound();

    todo.Todo = todoItem.Todo;
    todo.IsComplete = todoItem.IsComplete;

    await _repository.UpdateTodoItemAsync(todo);
    return Results.NoContent();
});
app.MapDelete("/api/delete/todoItem/{id}", async (ITodoItemRepository _repository, int id) =>
{
    var todo = await _repository.FindTodoItemAsync(id);
    if (todo == null) return Results.NotFound();

    await _repository.DeleteTodoItemAsync(todo);

    return Results.NoContent();
});
app.MapDelete("/api/delete/todoItem/completed", async (ITodoItemRepository _repository) =>
{
    var todoItems = await _repository.GetTodoItemsAsync();
    if (!todoItems.Any()) return Results.NotFound("No todo items found");

    var completedTodoItems = todoItems.Where(x => x.IsComplete).ToList();
    await _repository.DeleteAllCompletedTodoItemsAsync(completedTodoItems);

    return Results.Ok("All completed todo items have been deleted");
});

app.Run();


record AddTodoItem(string Todo);
record UpdateTodoItem(string Todo, bool IsComplete);
record TodoItemResponse(string Todo, bool IsComplete);