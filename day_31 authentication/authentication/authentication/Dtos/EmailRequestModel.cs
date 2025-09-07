using System.ComponentModel.DataAnnotations;

namespace authentication.Dtos;

public class EmailRequestModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}