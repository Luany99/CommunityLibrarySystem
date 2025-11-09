using CommunityLibrarySystem.Domain.Enums;

namespace CommunityLibrarySystem.Domain.Entities
{
    public class Emprestimo
    {
        public Guid Id { get; private set; }
        public Guid LivroId { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime? DataDevolucao { get; private set; }
        public StatusEmprestimo Status { get; private set; }

        protected Emprestimo() { }

        public Emprestimo(Guid livroId)
        {
            Id = Guid.NewGuid();
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
