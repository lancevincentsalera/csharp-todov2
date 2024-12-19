using Microsoft.EntityFrameworkCore;
using TodoRestApiV2.Models;
namespace TodoRestApiV2.Data;

public class TodoRestApiContext(DbContextOptions<TodoRestApiContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}