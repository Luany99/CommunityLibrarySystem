using System.ComponentModel.DataAnnotations;

namespace CommunityLibrarySystem.Application.DTOs
{
    public class LivroDto
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantidadeDisponivel { get; set; }
    }
}
