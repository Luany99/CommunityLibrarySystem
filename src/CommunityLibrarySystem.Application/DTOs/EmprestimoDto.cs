using System.ComponentModel.DataAnnotations;

namespace CommunityLibrarySystem.Application.DTOs
{
    public class EmprestimoDto
    {
        [Required]
        public Guid LivroId { get; set; }
    }
}
