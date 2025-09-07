using System.ComponentModel.DataAnnotations;

namespace authentication.Dtos;
public class VerifyOtpModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Otp { get; set; }
}