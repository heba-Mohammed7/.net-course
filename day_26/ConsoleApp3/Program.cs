// See https://aka.ms/new-console-template for more information


using ConsoleApp3.Data;
using ConsoleApp3.Services;
Console.WriteLine("Hello, World!");

var context = new ApplicationDbContext();
var service = new ProductCategoryService(context);

// Create a Category
int categoryId = await service.CreateCategoryAsync("Electronics 2");
Console.WriteLine($"Created Category with ID: {categoryId}");

// Create a Product
int productId = await service.CreateProductAsync("Laptop 2", 999.99m, categoryId);
Console.WriteLine($"Created Product with ID: {productId}");

// Update a Product
await service.UpdateProductAsync(productId, "Updated Laptop 2", 1099.99m, categoryId);

// Get All Products
var products = await service.GetAllProductsAsync();
foreach (var product in products)
{
    Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, CategoryId: {product.CategoryId}");
}

// Get Total Value of Category
decimal totalValue = await service.GetCategoryTotalValueAsync(categoryId);
Console.WriteLine($"Total Value of Category {categoryId}: {totalValue}");

// Get Data from View
var viewData = await service.GetProductCategoryViewAsync();
foreach (var item in viewData)
{
    Console.WriteLine($"Product: {item.ProductName}, Category: {item.CategoryName}, Price: {item.Price}");
}

// Delete Product
await service.DeleteProductAsync(productId);
Console.WriteLine("Product deleted.");

// Delete Category
await service.DeleteCategoryAsync(categoryId);
Console.WriteLine("Category deleted.");


