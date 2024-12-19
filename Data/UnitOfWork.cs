using Microsoft.EntityFrameworkCore;
using TodoRestApiV2.Data.Interfaces;
namespace TodoRestApiV2.Data;


public class UnitOfWork(TodoRestApiContext context) : IUnitOfWork, IDisposable
{
    public DbContext Database { get; private set; } = context;

    public async Task Save()
    {
        await Database.SaveChangesAsync();
    }

    public void Dispose()
    {
        Database.Dispose();
    }
}