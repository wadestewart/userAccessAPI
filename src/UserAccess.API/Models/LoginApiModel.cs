using System.ComponentModel.DataAnnotations;

namespace UserAccess.API.Models
{
    public class LoginApiModel
    {
        [Required] public string Email { get; set; }
        [Required] public string  Password { get; set; }
    }
}