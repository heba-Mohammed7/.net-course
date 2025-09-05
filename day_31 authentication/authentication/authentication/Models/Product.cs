using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public string ImagePath { get; set; }
    public bool IsApproved { get; set; }
    public string CreatorId { get; set; }
    [ForeignKey("CreatorId")]
    public ApplicationUser Creator { get; set; }
    public DateTime CreatedAt { get; set; }
}