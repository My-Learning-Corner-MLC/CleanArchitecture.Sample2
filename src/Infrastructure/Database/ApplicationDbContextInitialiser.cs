using Sample2.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Sample2.Infrastructure.Database;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        ApplicationDbContextInitialiser initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.ProductItems.Any())
        {
            var contentRootPath = $@"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName}\src\Infrastructure\Database";
            string sourcePath = Path.Combine(contentRootPath, "Setup", "Product.json");
            string sourceJson = File.ReadAllText(sourcePath);
            ProductSourceEntry[]? sourceItems = JsonSerializer.Deserialize<ProductSourceEntry[]>(sourceJson);

            if (sourceItems is null || sourceItems.Length == 0)
            {
                _logger.LogError("Cannot read source items from json file.");
                return;
            }

            // Seeding product brands
            _context.ProductBrands.RemoveRange(_context.ProductBrands);
            IEnumerable<ProductBrand> productBrands = sourceItems
                .Select(x => x.Brand)
                .Distinct()
                .Select(brandName => new ProductBrand { Brand = brandName });
            await _context.ProductBrands.AddRangeAsync(productBrands);
            _logger.LogInformation("Seeded Product with {NumBrands} brands", _context.ProductBrands.Count());

            // Seeding product types
            _context.ProductTypes.RemoveRange(_context.ProductTypes);
            IEnumerable<ProductType> productTypes = sourceItems
                .Select(x => x.Type)
                .Distinct()
                .Select(typeName => new ProductType { Type = typeName });
            await _context.ProductTypes.AddRangeAsync(productTypes);
            _logger.LogInformation("Seeded Product with {NumTypes} types", _context.ProductTypes.Count());

            _ = await _context.SaveChangesAsync();

            // Seeding product items
            Dictionary<string, int> brandIdsByName = await _context.ProductBrands.ToDictionaryAsync(x => x.Brand, x => x.Id);
            Dictionary<string, int> typeIdsByName = await _context.ProductTypes.ToDictionaryAsync(x => x.Type, x => x.Id);

            await _context.ProductItems.AddRangeAsync(sourceItems.Select((source, index) => new ProductItem
            {
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                ProductBrandId = brandIdsByName[source.Brand],
                ProductTypeId = typeIdsByName[source.Type],
                PictureFileName = $"{index + 1}.webp",
                PictureUri = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?q=80&w=1770&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            }));

            _logger.LogInformation("Seeded Product with {NumItems} items", _context.ProductItems.Count());
            _ = await _context.SaveChangesAsync();
        }
    }

    private sealed class ProductSourceEntry
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
