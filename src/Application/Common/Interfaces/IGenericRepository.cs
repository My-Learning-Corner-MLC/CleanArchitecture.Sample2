using System.Linq.Expressions;
using Sample2.Application.Constants;
using Sample2.Domain.Common;

namespace Sample2.Application.Common.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseAuditableEntity
{
    Task<IEnumerable<TEntity>> GetAll(
        Expression<Func<TEntity, bool>>? filterExpression = default,
        int page = RepositoryConstant.DEFAULT_PAGE_NUMBER,
        int size = RepositoryConstant.DEFAULT_SIZE_PER_PAGE,
        bool includeDeleted = false,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderByFunc = default,
        string? includeProperties = default,
        CancellationToken cancellationToken = default
    );

    Task<TEntity?> GetBy(
        Expression<Func<TEntity, bool>> predicateExpression,
        bool includeDeleted = false,
        string? includeProperties = default,
        CancellationToken cancellationToken = default
    );

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entites);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    Task<int> CountAll(
        Expression<Func<TEntity, bool>>? filterExpression = default,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default
    );
}