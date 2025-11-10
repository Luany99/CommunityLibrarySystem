using System.Text.RegularExpressions;

namespace CommunityLibrarySystem.Domain.ValueObjects
{
    public sealed partial class Senha
    {
        public string Hash { get; }

        private Senha(string hash)
        {
            Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        }

        public static Senha Criar(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha obrigatória");

            if (!Regex.IsMatch(senha, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$"))
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres, uma letra maiúscula, uma minúscula, um número e um caractere especial.");

            var hash = BCrypt.Net.BCrypt.HashPassword(senha);
            return new Senha(hash);
        }

        public static Senha Reidratar(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Hash inválido");

            return new Senha(hash);
        }

        public bool Verificar(string senha) => BCrypt.Net.BCrypt.Verify(senha, Hash);
    }
}
