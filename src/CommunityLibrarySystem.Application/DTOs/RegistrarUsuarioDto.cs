using System.ComponentModel.DataAnnotations;

namespace CommunityLibrarySystem.Application.DTOs
{
    public class RegistrarUsuarioDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
