using CommunityLibrarySystem.Domain.ValueObjects;

namespace CommunityLibrarySystem.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Senha SenhaHash { get; private set; }

        protected Usuario() { }

        public Usuario(string nome, Email email, Senha senha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome obrigatório");
            Nome = nome;
            Email = email ?? throw new ArgumentException("E-mail obrigatório");
            SenhaHash = senha ?? throw new ArgumentException("Senha obrigatória");
        }

        public bool VerificarSenha(string senha)
        {
            return SenhaHash.Verificar(senha);
        }
    }
}
