using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
