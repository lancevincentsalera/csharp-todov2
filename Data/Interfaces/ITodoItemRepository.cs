

using TodoRestApiV2.Models;

namespace TodoRestApiV2.Data.Interfaces;
public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
    Task AddTodoItemAsync(TodoItem todoItem);
    Task UpdateTodoItemAsync(TodoItem todoItem);
    Task DeleteTodoItemAsync(TodoItem todoItem);
    Task DeleteAllCompletedTodoItemsAsync(List<TodoItem> todoItems);
    Task<TodoItem?> FindTodoItemAsync(int id);
}