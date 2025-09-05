using System.ComponentModel.DataAnnotations.Schema;

namespace authentication.Models;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }
    public int Quantity { get; set; }
}