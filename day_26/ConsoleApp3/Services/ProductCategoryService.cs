using ConsoleApp3.Data;
using ConsoleApp3.Models;
using Microsoft.EntityFrameworkCore;


namespace ConsoleApp3.Services;

public class ProductCategoryService
{
    private readonly ApplicationDbContext _context;

    public ProductCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Category CRUD
    public async Task<int> CreateCategoryAsync(string name)
    {
        var category = new Category { Name = name };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category.Id;
    }

    public async Task UpdateCategoryAsync(int id, string name)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new Exception($"Category with ID {id} not found");

        category.Name = name;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new Exception($"Category with ID {id} not found");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .ToListAsync();
    }

    // Product CRUD
    public async Task<int> CreateProductAsync(string name, decimal price, int categoryId)
    {
        var product = new Product 
        { 
            Name = name, 
            Price = price, 
            CategoryId = categoryId 
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task UpdateProductAsync(int id, string name, decimal price, int categoryId)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new Exception($"Product with ID {id} not found");

        product.Name = name;
        product.Price = price;
        product.CategoryId = categoryId;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new Exception($"Product with ID {id} not found");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .ToListAsync();
    }

    // Function: Get Total Value of Products in a Category
    public async Task<decimal> GetCategoryTotalValueAsync(int categoryId)
    {
        try
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .SumAsync(p => p.Price);
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    // View: Get Product and Category Details
    public async Task<List<ProductCategoryView>> GetProductCategoryViewAsync()
    {
        return await _context.Products
            .Join(_context.Categories,
                product => product.CategoryId,
                category => category.Id,
                (product, category) => new ProductCategoryView
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    CategoryId = category.Id,
                    CategoryName = category.Name
                })
            .ToListAsync();
    }
}