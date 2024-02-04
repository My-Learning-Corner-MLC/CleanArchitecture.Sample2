using System.Linq.Expressions;
using Sample2.Application.Constants;
using Sample2.Domain.Common;

namespace Sample2.Application.Common.Models;

public class GetAllParameters<TEntity> where TEntity : BaseAuditableEntity
{
    public Expression<Func<TEntity, bool>>? FilterExpression { get; set; }

    public int Page { get; set; } = RepositoryConstant.DEFAULT_PAGE_NUMBER;

    public int Size { get; set; } = RepositoryConstant.DEFAULT_SIZE_PER_PAGE;

    public bool IncludeDeleted { get; set; } = false;

    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderByFunc { get; set; }

    public string? IncludeProperties { get; set; }

    public bool TrackingChanges { get; set; } = false;

    public CancellationToken CancellationToken { get; set; } = default;
}