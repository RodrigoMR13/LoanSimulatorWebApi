# 📦 Nome da API

API REST desenvolvida com ASP.NET Core para [descrever o propósito da API, ex: gerenciamento de contas bancárias].

## 🚀 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core (SQL Server)](https://learn.microsoft.com/ef/core/)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [AutoMapper](https://automapper.org/)*
- [Serilog](https://serilog.net/)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)

## Arquitetura do Projeto

A arquitetura escolhida para o projeto foi o Clean Architecture, conforme determinação da Arquitetura de Referência da CAIXA.

Mais informações: [Clean Architecture](https://www.macoratti.net/21/10/net_cleanarch1.htm)

## 📁 Estrutura do Projeto

```
Application/
Domain/
Infrastructure/
LoanSimulatorWebAPI/
UnitTests/
```

## ⚙️ Configuração e Execução

### Pré-requisitos

- .NET SDK 9.x instalado
- Banco de dados configurado (ex: SQL Server, PostgreSQL)

### Subindo o Banco de Dados de simulações

```bash
# Navegue até a raiz do projeto
docker compose up -d

# Obs.: para derrubar o banco de dados, execute:
docker compose down
```

### Executando localmente

```bash
# Navegue até a raiz do projeto
# Restaurar pacotes
dotnet restore

# Executar a aplicação
dotnet run --project ./LoanSimulatorWebAPI
```

A API estará disponível em: `https://localhost:8081` ou `http://localhost:8080`

### Executando via docker

```bash
# Navegue até a pasta onde está o Dockerfile do projeto
# Build da imagem
docker build -t loansimulatorwebapi:latest .

# Subindo container (HTTP)
docker run --rm -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development --name loansimulatorwebapi loansimulatorwebapi:latest
```

A API estará disponível em: `http://localhost:8080`

### Swagger

A documentação interativa estará disponível em:

```
http://localhost:8080/swagger
https://localhost:8081/swagger
```

## 🧪 Testes

```bash
dotnet test
```

## 🛠️ Endpoints Principais

| Método | Rota | Descrição |
|--------|------|-----------|
| GET    | /api/v1/emprestimos/produtos       | Lista todos os produtos de empréstimo |
| GET    | /api/v1/emprestimos/produtos/{id}  | Busca um produto de empréstimo por id |
| POST   | /api/v1/emprestimos/simular        | Simula um empréstimo |

## 🔐 Autenticação

[Descreva o método de autenticação, ex: JWT, OAuth2, etc.]