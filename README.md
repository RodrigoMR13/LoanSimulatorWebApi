# 📦 Nome da API

API REST desenvolvida com ASP.NET Core para [descrever o propósito da API, ex: gerenciamento de contas bancárias].

## 🚀 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [Entity Framework Core (SQL Server)](https://learn.microsoft.com/ef/core/)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [EventHub](https://learn.microsoft.com/azure/event-hubs/)
- [Docker](https://www.docker.com/)
- [OpenTelemetry](https://opentelemetry.io/)
- [Keycloak (SSO)](https://www.keycloak.org/)
- [AutoMapper](https://automapper.org/)
- [Serilog](https://serilog.net/)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)

## 🏗️ Arquitetura do Projeto

A arquitetura escolhida para o projeto foi o Clean Architecture, conforme determinação da Arquitetura de Referência da CAIXA.

Mais informações: [Clean Architecture](https://www.macoratti.net/21/10/net_cleanarch1.htm)

## 📁 Estrutura do Projeto

```
Application/
Domain/
Infrastructure/
LoanSimulatorWebAPI/
```

## ⚙️ Configuração e Execução

### Pré-requisitos

- .NET SDK 9.x instalado
- Docker instalado (para subir os serviços do docker-compose)
- WSL instalado (dispensado caso utilize Linux)

### 1. Configurar variáveis de ambiente

No arquivo appsettings.json, preencha com os valores das variáveis de ambiente

### 2. Subir os Serviços

Execute os comandos abaixo no CMD/PowerShell

```bash
# Navegue até a raiz do projeto
# Criação do certificado
dotnet dev-certs https -ep "./src/LoanSimulatorWebAPI/certs/localhost.pfx" -p "123"
dotnet dev-certs https --trust

# Suba os serviços do docker-compose (API, SQL Server, Keycloak e Jaeger)
docker compose up -d
```

Agora execute os comandos abaixo no WSL

```bash
# Navegue até a raiz do projeto
# Execute o init_db.sh para criar as tabelas no banco de dados e popular com os dados dos produtos
./init_db.sh
```

### 3. Acessando os serviços

A API estará disponível em:
`https://localhost:8443/swagger` ou `http://localhost:8080/swagger` (use https de preferência)

O Keycloak estará disponível em:
`http://localhost:18080` (user: admin / password: admin)

O Jaeger (tracing) estará disponível em:
`http://localhost:16686`

Por último, o SQL Server estará disponível em:
`localhost,1433` (user: sa / password: Password123!)
Use um SGBD de sua preferência para conectar.

### 4. Fazendo uma requisição para a API

Use o Swagger para fazer as requisições ou uma ferramenta como Postman ou Insomnia.

1. Para realizar a autenticação no swagger, utilize o ícone "Authorize" no canto direito.
2. Preencha o campo "client_id" com "loan-simulator-api" e marque os 2 scopes (openid e profile)
3. Clique em "Authorize"
4. Você será redirecionado para a tela de login do SSO, se já tiver um usuário, preencha o usuário e a senha, se não, clique em "register"
5. Após realizar o registro/login, você será redirecionado para a API novamente, basta clicar em close, pois você já está autenticado.
6. Agora você pode fazer as requisições para os endpoints da API.

## 🧪 Testes

```bash
dotnet test
```

## 🛠️ Endpoints Principais

| Método | Rota                              | Descrição                                      |
| ------ | --------------------------------- | ---------------------------------------------- |
| GET    | /emprestimos/v1/produtos          | Lista todos os produtos de empréstimo          |
| GET    | /emprestimos/v1/produtos/{id}     | Busca um produto de empréstimo por id          |
| POST   | /emprestimos/v1/simulacoes        | Simula um empréstimo                           |
| GET    | /emprestimos/v1/simulacoes        | Retorna todas as simulações de empréstimos     |
| GET    | /emprestimos/v1/simulacoes/volume | Retorna o volume de simulações de empréstimos  |
| GET    | /emprestimos/v1/telemetry/summary | Retorna as métricas de telemetria da aplicação |

## 🔐 Autenticação

A API usa Keycloak OAuth2 (Password Grant).

### 1. Obter Token

```
POST {KEYCLOAK_URL}/realms/{REALM}/protocol/openid-connect/token
Content-Type: application/x-www-form-urlencoded

client_id={CLIENT_ID}
username={USUARIO}
password={SENHA}
grant_type=password
```

Exemplo:

```
curl -X POST "http://localhost:18080/realms/hackathon/protocol/openid-connect/token" \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "client_id=loan-simulator-api" \
  -d "username=testuser" \
  -d "password=123" \
  -d "grant_type=password"
```

### 2. Usar o Token

Inclua no header das requisições:

```
Authorization: Bearer {access_token}
```

Exemplo:

```
curl -X GET "https://localhost:8443/emprestimos/v1/telemetry/summary" \
  -H "Authorization: Bearer eyJhbGciOi..."
```
