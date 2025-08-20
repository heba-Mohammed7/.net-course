using ConsoleApp3.Data;
using ConsoleApp3.Models;
using Microsoft.Data.SqlClient;
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
        var nameParam = new SqlParameter("@Name", name);
        var idParam = new SqlParameter
        {
            ParameterName = "@Id",
            SqlDbType = System.Data.SqlDbType.Int,
            Direction = System.Data.ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC sp_CreateCategory @Name, @Id OUTPUT", nameParam, idParam);
        return (int)idParam.Value;
    }

    public async Task UpdateCategoryAsync(int id, string name)
    {
        var parameters = new[]
        {
            new SqlParameter("@Id", id),
            new SqlParameter("@Name", name)
        };
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateCategory @Id, @Name", parameters);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var parameter = new SqlParameter("@Id", id);
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteCategory @Id", parameter);
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var parameter = new SqlParameter("@Id", id);
        return await _context.Categories
            .FromSqlRaw("EXEC sp_GetCategoryById @Id", parameter)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .FromSqlRaw("EXEC sp_GetAllCategories")
            .ToListAsync();
    }

    // Product CRUD
    public async Task<int> CreateProductAsync(string name, decimal price, int categoryId)
    {
        var nameParam = new SqlParameter("@Name", name);
        var priceParam = new SqlParameter("@Price", price);
        var categoryIdParam = new SqlParameter("@CategoryId", categoryId);
        var idParam = new SqlParameter
        {
            ParameterName = "@Id",
            SqlDbType = System.Data.SqlDbType.Int,
            Direction = System.Data.ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC sp_CreateProduct @Name, @Price, @CategoryId, @Id OUTPUT",
            nameParam, priceParam, categoryIdParam, idParam);
        return (int)idParam.Value;
    }

    public async Task UpdateProductAsync(int id, string name, decimal price, int categoryId)
    {
        var parameters = new[]
        {
            new SqlParameter("@Id", id),
            new SqlParameter("@Name", name),
            new SqlParameter("@Price", price),
            new SqlParameter("@CategoryId", categoryId)
        };
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateProduct @Id, @Name, @Price, @CategoryId", parameters);
    }

    public async Task DeleteProductAsync(int id)
    {
        var parameter = new SqlParameter("@Id", id);
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteProduct @Id", parameter);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var parameter = new SqlParameter("@Id", id);
        return await _context.Products
            .FromSqlRaw("EXEC sp_GetProductById @Id", parameter)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .FromSqlRaw("EXEC sp_GetAllProducts")
            .ToListAsync();
    }

    // Function: Get Total Value of Products in a Category
    public async Task<decimal> GetCategoryTotalValueAsync(int categoryId)
    {
        try
        {
            var parameter = new SqlParameter("@CategoryId", categoryId);
            var result = await _context.Database
                .SqlQueryRaw<decimal>("SELECT dbo.fn_GetCategoryTotalValue(@CategoryId) AS Value", parameter)
                .SingleAsync();
            return result;
        }
        catch (SqlException ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }
    // View: Get Product and Category Details
    public async Task<List<ProductCategoryView>> GetProductCategoryViewAsync()
    {
        return await _context.Set<ProductCategoryView>()
            .FromSqlRaw("SELECT * FROM vw_ProductCategory")
            .ToListAsync();
    }
}