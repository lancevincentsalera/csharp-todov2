
using Microsoft.EntityFrameworkCore;
using TodoRestApiV2.Data.Interfaces;
using TodoRestApiV2.Models;

namespace TodoRestApiV2.Data.Repository;

public class TodoItemRepository : BaseRepository, ITodoItemRepository
{
    public TodoItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    public async Task AddTodoItemAsync(TodoItem todoItem)
    {
        GetDbSet<TodoItem>().Add(todoItem);
        await _unitOfWork.Save();
    }

    public async Task DeleteAllCompletedTodoItemsAsync(List<TodoItem> todoItems)
    {
        GetDbSet<TodoItem>().RemoveRange(todoItems);
        await _unitOfWork.Save();
    }

    public async Task DeleteTodoItemAsync(TodoItem todoItem)
    {
        GetDbSet<TodoItem>().Remove(todoItem);
        await _unitOfWork.Save();
    }

    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
    {
        return await GetDbSet<TodoItem>().ToListAsync();
    }

    public async Task UpdateTodoItemAsync(TodoItem todoItem)
    {
        GetDbSet<TodoItem>().Update(todoItem);
        await _unitOfWork.Save();
    }

    public async Task<TodoItem?> FindTodoItemAsync(int id)
    {
        return await GetDbSet<TodoItem>().FindAsync(id);
    }
}