# 📚 Community Library System

O **Community Library System** é uma API RESTful que permite gerenciar uma biblioteca comunitária, possibilitando o cadastro de livros, controle de empréstimos e devoluções, além de autenticação segura de usuários.

### 🎯 Funcionalidades Principais

- ✅ **Autenticação e Autorização**
  - Registro de novos usuários com validação de senha forte
  - Login com geração de token JWT
  - Proteção de rotas com autorização baseada em token

- ✅ **Gerenciamento de Livros**
  - Cadastro de livros (título, autor, ano de publicação, quantidade)
  - Listagem completa e paginada de livros
  - Consulta de livro por ID
  - Controle automático de quantidade disponível

- ✅ **Gerenciamento de Empréstimos**
  - Solicitação de empréstimo com validação de disponibilidade
  - Redução automática da quantidade disponível
  - Devolução de livros com atualização de status
  - Incremento automático da quantidade ao devolver
  - Listagem e consulta de empréstimos

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - API RESTful
- **Entity Framework Core 9.0** - ORM para acesso a dados
- **SQL Server** - Banco de dados relacional
- **JWT (JSON Web Token)** - Autenticação e autorização
- **BCrypt.Net** - Hashing seguro de senhas
- **Swagger/OpenAPI** - Documentação interativa da API

### Testes
- **xUnit** - Framework de testes
- **Moq** - Library para mocking
- **FluentAssertions** - Assertions fluentes e legíveis

## 🚀 Como Executar o Projeto

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (ou Docker)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### 1️⃣ Clonar o Repositório

```bash
git clone https://github.com/Luany99/CommunityLibrarySystem.git
cd CommunityLibrarySystem
```

### 2️⃣ Configurar o Banco de Dados

Edite o arquivo `src/CommunityLibrarySystem.Api/appsettings.json` e configure a connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CommunityLibrarySystem;User Id={seu usuário};Password={sua senha};TrustServerCertificate=True;"
  }
}
```

### 3️⃣ Executar as Migrations

```bash
cd src/CommunityLibrarySystem.Api
dotnet ef database update
```

### 4️⃣ Executar a API

```bash
dotnet run
```

### 5️⃣ Autenticação

### Registrar Usuário

```bash
POST https://localhost:7001/api/auth/registrar
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao@email.com",
  "senha": "Senha@123"
}
```

### Fazer Login

```bash
POST https://localhost:7001/api/auth/login
Content-Type: application/json

{
  "email": "joao@email.com",
  "senha": "Senha@123"
}
```

### 6️⃣ Executar os Testes

```bash
cd tests/CommunityLibrarySystem.Test
dotnet test
```

**Resultado esperado**: ✅ 87 testes passando com 100% de sucesso!