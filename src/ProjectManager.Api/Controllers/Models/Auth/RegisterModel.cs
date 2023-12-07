using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Api.Controllers.Models.Auth;

public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;

    public string? Name { get; set; }
}
