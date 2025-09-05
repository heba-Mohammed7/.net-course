using System.ComponentModel.DataAnnotations;

namespace authentication.Dtos;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public IFormFile Image { get; set; }
}