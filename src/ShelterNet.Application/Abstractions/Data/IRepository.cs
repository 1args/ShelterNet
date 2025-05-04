using System.Linq.Expressions;

namespace ShelterNet.Application.Abstractions.Data;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task DeleteAsync(TEntity[] entities, CancellationToken cancellationToken);

    Task<TEntity> GetByIdAsync(
        Expression<Func<TEntity, bool>> expression, 
        CancellationToken cancellationToken,
        bool tracking = true);

    Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? expression = null,
        bool tracking = true);
    
    IQueryable<TEntity> AsQueryable(bool tracking = true);
    
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

    Task<TEntity?> SingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken,
        bool tracking = true,
        Expression<Func<TEntity, object>>? include = null);

    Task<TEntity?> SingleOrDefaultWithIncludesAsync<TPreviousProperty, TProperty>(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken,
        Expression<Func<TEntity, TPreviousProperty>> include1,
        Expression<Func<TPreviousProperty, TProperty>> include2,
        bool tracking = true);
    
    Task<List<TEntity>> ToListAsync(
        CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? expression = null,
        bool tracking = true);
}