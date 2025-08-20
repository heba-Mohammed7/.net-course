using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public class ProductWithCategory 
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
}

class Program
{
    static async Task Main()
    {
        // Connection string
        var cs = "Server=localhost\\SQLEXPRESS;Database=Dapper_Task;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";

        using var conn = new SqlConnection(cs);
        await conn.OpenAsync();

        // ---------------------------
        // A) STORED PROCEDURE: Category CRUD
        // ---------------------------
        Console.WriteLine("[Category CRUD]");
        
        // Create Category
        var catParams = new DynamicParameters();
        catParams.Add("@Name", "Electronics", DbType.String, ParameterDirection.Input);
        catParams.Add("@NewCategoryId", dbType: DbType.Int32, direction: ParameterDirection.Output);
        await conn.ExecuteAsync("dbo.usp_CreateCategory", catParams, commandType: CommandType.StoredProcedure);
        int newCatId = catParams.Get<int>("@NewCategoryId");
        Console.WriteLine($"[SP] New Category Id = {newCatId}");

        // Get Category By Id
        var category = await conn.QueryFirstOrDefaultAsync<Category>(
            "dbo.usp_GetCategoryById",
            new { Id = newCatId },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"[SP] Got Category: {category?.Name}");

        // Update Category
        await conn.ExecuteAsync(
            "dbo.usp_UpdateCategory",
            new { Id = newCatId, Name = "Updated Electronics" },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine("[SP] Category Updated");

        // Get All Categories
        var categories = await conn.QueryAsync<Category>(
            "dbo.usp_GetAllCategories",
            commandType: CommandType.StoredProcedure
        );
        foreach (var cat in categories)
        {
            Console.WriteLine($"[SP] Category: {cat.Id} - {cat.Name}");
        }

        // ---------------------------
        // B) STORED PROCEDURE: Product CRUD
        // ---------------------------
        Console.WriteLine("\n[Product CRUD]");
        
        // Create Product
        var prodParams = new DynamicParameters();
        prodParams.Add("@Name", "Laptop", DbType.String, ParameterDirection.Input);
        prodParams.Add("@Price", 999.99m, DbType.Decimal, ParameterDirection.Input);
        prodParams.Add("@CategoryId", newCatId, DbType.Int32, ParameterDirection.Input);
        prodParams.Add("@NewProductId", dbType: DbType.Int32, direction: ParameterDirection.Output);
        await conn.ExecuteAsync("dbo.usp_CreateProduct", prodParams, commandType: CommandType.StoredProcedure);
        int newProdId = prodParams.Get<int>("@NewProductId");
        Console.WriteLine($"[SP] New Product Id = {newProdId}");

        // Get Product By Id
        var product = await conn.QueryFirstOrDefaultAsync<Product>(
            "dbo.usp_GetProductById",
            new { Id = newProdId },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"[SP] Got Product: {product?.Name} - ${product?.Price}");

        // Update Product
        await conn.ExecuteAsync(
            "dbo.usp_UpdateProduct",
            new { Id = newProdId, Name = "Updated Laptop", Price = 1099.99m, CategoryId = newCatId },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine("[SP] Product Updated");

        // Get All Products
        var products = await conn.QueryAsync<Product>(
            "dbo.usp_GetAllProducts",
            commandType: CommandType.StoredProcedure
        );
        foreach (var prod in products)
        {
            Console.WriteLine($"[SP] Product: {prod.Id} - {prod.Name} - ${prod.Price}");
        }

        // ---------------------------
        // C) VIEW: Products with Categories
        // ---------------------------
        Console.WriteLine("\n[VIEW]");
        var productsWithCats = await conn.QueryAsync<ProductWithCategory>(
            "SELECT * FROM dbo.vw_ProductsWithCategories WHERE CategoryId = @CategoryId",
            new { CategoryId = newCatId }
        );
        foreach (var item in productsWithCats)
        {
            Console.WriteLine($"[VIEW] Product: {item.ProductName} in {item.CategoryName} - ${item.Price}");
        }

        // ---------------------------
        // D) FUNCTION: Total Price by Category
        // ---------------------------
        Console.WriteLine("\n[FUNCTION]");
        var totalPrice = await conn.ExecuteScalarAsync<decimal>(
            "SELECT dbo.fn_TotalPriceByCategory(@CategoryId)",
            new { CategoryId = newCatId }
        );
        Console.WriteLine($"[FUNCTION] Total Price in Category {newCatId}: ${totalPrice}");

        // ---------------------------
        // E) CLEANUP: Delete Product and Category
        // ---------------------------
        Console.WriteLine("\n[CLEANUP]");
        await conn.ExecuteAsync(
            "dbo.usp_DeleteProduct",
            new { Id = newProdId },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"[SP] Deleted Product Id: {newProdId}");

        await conn.ExecuteAsync(
            "dbo.usp_DeleteCategory",
            new { Id = newCatId },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"[SP] Deleted Category Id: {newCatId}");
    }
}