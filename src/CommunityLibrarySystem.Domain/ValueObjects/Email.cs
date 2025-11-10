using System.Text.RegularExpressions;

namespace CommunityLibrarySystem.Domain.ValueObjects
{
    public partial class Email
    {
        public string Endereco { get; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("E-mail obrigatório");

            if (!EmailValido(endereco))
                throw new ArgumentException("E-mail em formato inválido");

            Endereco = endereco;
        }

        [GeneratedRegex(@"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,10}$", RegexOptions.IgnoreCase)]
        private static partial Regex EmailRegex();

        private bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex().IsMatch(email);
        }

        public override bool Equals(object obj) =>
            obj is Email other && Endereco == other.Endereco;

        public override int GetHashCode() =>
            Endereco.GetHashCode();

        public override string ToString() => Endereco;
    }
}
