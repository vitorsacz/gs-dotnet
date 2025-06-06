
# 🦷 GS-DotNet — API de Gestão Odontológica

Este projeto é uma API RESTful desenvolvida em .NET, com o objetivo de gerenciar dados relacionados a um sistema odontológico. A aplicação permite o cadastro e gerenciamento de usuários, profissionais da saúde, pacientes, consultas, especialidades e diagnósticos, além de oferecer recursos de autenticação e autorização com tokens JWT.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://learn.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-8) — Framework principal  
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/) — ORM para acesso a dados  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server) — Banco de dados relacional  
- [AutoMapper](https://automapper.org/) — Mapeamento de objetos  
- [FluentValidation](https://docs.fluentvalidation.net/) — Validações  
- [JWT (JSON Web Token)](https://jwt.io/) — Autenticação e autorização  
- [Swagger](https://swagger.io/) — Documentação da API  
- [xUnit](https://xunit.net/) — Testes automatizados  

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server)  
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)  
- [Postman](https://www.postman.com/) (opcional, para testes de API)  

### Passos para rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/vitorsacz/gs-dotnet.git
   cd gs-dotnet
   ```

2. Configure o `appsettings.json`:  
   No projeto principal, edite o arquivo `appsettings.json` com os dados do seu SQL Server:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=ClinicaOdonto;User Id=SEU_USUARIO;Password=SUA_SENHA;"
   }
   ```

3. Execute as migrações e crie o banco de dados:
   ```bash
   dotnet ef database update
   ```

4. Rode o projeto:
   ```bash
   dotnet run --project Clinica.API
   ```

5. Acesse a documentação da API:
   ```
   https://localhost:<porta>/swagger
   ```

---

## 📌 Documentação dos Endpoints

A documentação completa está disponível via **Swagger** assim que a API é executada. Exemplos de rotas:

### 🔐 Autenticação

| Método | Endpoint       | Descrição                     |
|--------|----------------|-------------------------------|
| POST   | `/api/login`   | Autenticação e geração de token |
| POST   | `/api/register`| Cadastro de novo usuário       |

### 👤 Usuários

| Método | Endpoint              | Descrição              |
|--------|-----------------------|------------------------|
| GET    | `/api/usuarios`       | Listar todos os usuários |
| GET    | `/api/usuarios/{id}`  | Obter usuário por ID     |
| PUT    | `/api/usuarios/{id}`  | Atualizar usuário        |
| DELETE | `/api/usuarios/{id}`  | Deletar usuário          |

### 🦷 Dentistas, Pacientes, Consultas, Diagnósticos

Endpoints seguem a mesma estrutura REST:

```http
GET    /api/[entidade]
POST   /api/[entidade]
PUT    /api/[entidade]/{id}
DELETE /api/[entidade]/{id}
```

> Consulte o Swagger para detalhes de payloads, validações e respostas.

---

## 🧪 Instruções de Testes

1. Navegue até o projeto de testes:
   ```bash
   cd Clinica.Test
   ```

2. Execute os testes com o comando:
   ```bash
   dotnet test
   ```

3. Resultados dos testes serão exibidos no terminal.  
   Os testes cobrem validações, regras de negócio e endpoints principais.

---

## 📌 Observações Finais

- O projeto está organizado em camadas (**API**, **Application**, **Domain**, **Infrastructure**, **Test**).  
- Utiliza princípios **SOLID** e práticas de **Clean Architecture**.  
- Ideal para fins acadêmicos e aprendizado sobre construção de APIs seguras com .NET.
