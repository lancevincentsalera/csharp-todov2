using Microsoft.EntityFrameworkCore;
using TodoRestApiV2.Data.Interfaces;

namespace TodoRestApiV2.Data.Repository;

public class BaseRepository(IUnitOfWork unitOfWork)
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly TodoRestApiContext _context = (TodoRestApiContext)unitOfWork.Database;

    protected DbSet<T> GetDbSet<T>() where T : class
    {
        return _context.Set<T>();
    }
}