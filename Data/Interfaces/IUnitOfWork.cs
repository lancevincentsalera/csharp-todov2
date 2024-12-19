using Microsoft.EntityFrameworkCore;

namespace TodoRestApiV2.Data.Interfaces;

public interface IUnitOfWork
{
    DbContext Database { get; }
    Task Save();
}