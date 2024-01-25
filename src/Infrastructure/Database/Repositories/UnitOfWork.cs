using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;

namespace Sample2.Infrastructure.Database.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public IProductRepository Products { get; private set; }
    public IProductBrandRepository ProductBrands { get; private set; }
    public IProductTypeRepository ProductTypes { get; private set; }

    public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;

        // Inital repositories
        Products = new ProductRepository(_context, _logger);
        ProductBrands = new ProductBrandRepository(_context, _logger);
        ProductTypes = new ProductTypeRepository(_context, _logger);
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Begin saving changes with contextId: {contextId}", _context.ContextId);
            
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("End saving changes");
        }
        catch (DbUpdateConcurrencyException concurrencyException)
        {
            _logger.LogError(concurrencyException, "Concurrency error occured: {message}", concurrencyException.Message);
            
            throw new ConflictException(errorMessage: "Resource conflicted", errorDescription: concurrencyException.Message);
        }
    }
}