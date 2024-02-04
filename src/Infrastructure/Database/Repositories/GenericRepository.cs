using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Application.Common.Models;
using Sample2.Domain.Common;

namespace Sample2.Infrastructure.Database.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseAuditableEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly ILogger<UnitOfWork> _logger;

    public GenericRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger) 
        => (_dbSet, _logger) = (context.Set<TEntity>(), logger);

    public virtual void Add(TEntity entity)
    {
        _logger.LogInformation("Adding a new {recordType} record", entity.GetType().Name);
        _dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entites)
    {
        _logger.LogInformation("Adding {recordCount} new {recordType} records", entites.Count(), entites.First().GetType().Name);

        _dbSet.AddRange(entites);
    }

    public virtual async Task<int> CountAll(Expression<Func<TEntity, bool>>? filterExpression = null, bool includeDeleted = false, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Counting all records");

        IQueryable<TEntity> queryable = _dbSet;
        if (!includeDeleted) queryable = queryable.Where(r => !r.IsDeleted);

        if (filterExpression is not null)
        {
            queryable = queryable.Where(filterExpression);
        }

        return await queryable.CountAsync(cancellationToken);
    }

    public virtual void Delete(TEntity entity)
    {
        _logger.LogInformation("Deleting {recordType} record: {recordId}", entity.GetType().Name, entity.Id);
        entity.IsDeleted = true;
        entity.LastModified = DateTime.Now;
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        _logger.LogInformation(
            "Deleting {recordCount} {recordType} records: {recordIds}", 
            entities.Count(),
            entities.First().GetType().Name,
            string.Join(", ", entities.Select(e => e.Id).ToArray())
        );

        entities.ToList().ForEach(e => 
        {
            e.IsDeleted = true;
            e.LastModified = DateTime.Now;
        });
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(GetAllParameters<TEntity> parameters)
    {
        _logger.LogInformation("Getting all records with page {pageNumber} and size {sizeNumber}", parameters.Page, parameters.Size);

        IQueryable<TEntity> queryable = _dbSet;
        if (!parameters.IncludeDeleted) queryable = queryable.Where(r => !r.IsDeleted);

        if (parameters.FilterExpression is not null)
        {
            queryable = queryable.Where(parameters.FilterExpression);
        }

        if (parameters.Page is not 0 && parameters.Size is not 0)
        {
            var skip = GetSkippingRecord(parameters.Page, parameters.Size);
            queryable = queryable.Skip(skip).Take(parameters.Size);
        }

        if (parameters.IncludeProperties is not null)
        {
            var properties = parameters.IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in properties)
            {
                queryable = queryable.Include(property);
            }
        }

        if (!parameters.TrackingChanges) queryable = queryable.AsNoTracking();

        return parameters.OrderByFunc is not null 
            ? await parameters.OrderByFunc(queryable).ToListAsync(parameters.CancellationToken) 
            : (IEnumerable<TEntity>)await queryable.ToListAsync(parameters.CancellationToken);
    }

    public virtual async Task<TEntity?> GetBy(
        Expression<Func<TEntity, bool>> predicateExpression, 
        bool includeDeleted = false,
        string? includeProperties = null,
        bool trackingChanges = false,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting record with predicate condition");

        IQueryable<TEntity> queryable = _dbSet;
        if (!includeDeleted) queryable = queryable.Where(r => !r.IsDeleted);
        if (includeProperties is not null)
        {
            var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in properties)
            {
                queryable = queryable.Include(property);
            }
        }

        if (!trackingChanges) queryable = queryable.AsNoTracking();

        return await queryable
            .FirstOrDefaultAsync(predicateExpression, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        _logger.LogInformation("Updating {recordType} record: {recordId}", entity.GetType().Name, entity.Id);
        entity.LastModified = DateTime.Now;
    }

    private static int GetSkippingRecord(int page, int size)
    {
        return (page - 1) * size;
    }
}