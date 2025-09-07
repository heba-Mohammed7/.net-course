using System.ComponentModel.DataAnnotations.Schema;

namespace authentication.Models;

public class OtpSession
{
    public int Id { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }
    public string OtpCode { get; set; } 
    public string SessionId { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public string Purpose { get; set; } 
}