using System.ComponentModel.DataAnnotations;

namespace CommunityLibrarySystem.Application.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
