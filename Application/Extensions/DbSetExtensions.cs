
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> GetEntityAsync<T>(this DbSet<T> set, Guid id, CancellationToken cancellationToken)
        where T : class
    {
        var entity = await set.FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
            throw new Exception();

        return entity;
    }
    
    public static async Task<T> GetEntityAsync<T>(this DbSet<T> set, string name, CancellationToken cancellationToken)
        where T : class
    {
        var entity = await set.FindAsync(new object[] { name }, cancellationToken);

        if (entity is null)
            throw new Exception();

        return entity;
    }
    
    public static async Task<T> GetEntityAsync<T>(this DbSet<T> set, int id, CancellationToken cancellationToken)
        where T : class
    {
        var entity = await set.FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
            throw new Exception();

        return entity;
    }
    
    public static async Task<T> GetEntityAsync<T>(this DbSet<T> set, decimal id, CancellationToken cancellationToken)
        where T : class
    {
        var entity = await set.FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
            throw new Exception();

        return entity;
    }
}