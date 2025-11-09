using System.Text.Json.Serialization;

namespace CommunityLibrarySystem.Domain.Entities
{
    public class Livro
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int QuantidadeDisponivel { get; private set; }
        [JsonIgnore]
        public ICollection<Emprestimo> Emprestimos { get; set; }

        protected Livro() { }

        public Livro(string titulo, string autor, int anoPublicacao, int quantidadeDisponivel)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título é obrigatório.");
            if (string.IsNullOrWhiteSpace(autor))
                throw new ArgumentException("Autor é obrigatório.");
            if (quantidadeDisponivel < 0)
                throw new ArgumentException("Quantidade disponível não pode ser negativa.");

            Titulo = titulo;
            Autor = autor;
            AnoPublicacao = anoPublicacao;
            QuantidadeDisponivel = quantidadeDisponivel;
        }

        public void ReduzirQuantidade()
        {
            if (QuantidadeDisponivel <= 0)
                throw new InvalidOperationException("Não há exemplares disponíveis.");
            QuantidadeDisponivel--;
        }

        public void AumentarQuantidade()
        {
            QuantidadeDisponivel++;
        }
    }
}
