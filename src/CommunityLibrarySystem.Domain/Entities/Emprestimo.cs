using CommunityLibrarySystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace CommunityLibrarySystem.Domain.Entities
{
    public class Emprestimo
    {
        public int Id { get; private set; }
        public int LivroId { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime? DataDevolucao { get; private set; }
        public StatusEmprestimo Status { get; private set; }
        [JsonIgnore]
        public Livro Livro { get; set; }

        protected Emprestimo() { }

        public Emprestimo(int livroId)
        {
            LivroId = livroId;
            DataEmprestimo = DateTime.UtcNow;
            Status = StatusEmprestimo.Ativo;
        }

        public void Devolver()
        {
            if (Status == StatusEmprestimo.Devolvido)
                throw new InvalidOperationException("Empréstimo já devolvido.");
            DataDevolucao = DateTime.UtcNow;
            Status = StatusEmprestimo.Devolvido;
        }
    }
}
