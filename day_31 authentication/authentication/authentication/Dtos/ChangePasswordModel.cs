using System.ComponentModel.DataAnnotations;

namespace authentication.Dtos;

public class ChangePasswordModel
{
    public string SessionId { get; set; }
    public string NewPassword { get; set; }
}