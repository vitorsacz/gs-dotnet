
# 🛑 Alerta Cidadão — API de Gestão de Riscos Ambientais

**Slogan:** _Tecnologia que salva vidas em tempo real._

Este projeto é uma API RESTful desenvolvida em .NET, parte integrante do sistema Alerta Cidadão — uma solução inteligente e integrada para monitoramento de enchentes em áreas urbanas de risco. A API permite o gerenciamento de usuários, sensores IoT, registros de eventos ambientais, rotas de evacuação e alertas, além de oferecer autenticação e autorização com tokens JWT.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://learn.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-8) — Framework principal  
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/) — ORM para acesso a dados  
- [Oracle Database](https://www.oracle.com/br/database/) — Banco de dados relacional  
- [AutoMapper](https://automapper.org/) — Mapeamento de objetos  
- [FluentValidation](https://docs.fluentvalidation.net/) — Validações  
- [JWT (JSON Web Token)](https://jwt.io/) — Autenticação e autorização  
- [Swagger](https://swagger.io/) — Documentação da API  
- [xUnit](https://xunit.net/) — Testes automatizados  

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)  
- [Oracle Database](https://www.oracle.com/br/database/)  
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)  
- [Postman](https://www.postman.com/) (opcional, para testes de API)  

### Passos para rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/vitorsacz/gs-dotnet.git
   cd gs-dotnet
   ```

2. Configure o `appsettings.json`:  
   No projeto principal, edite o arquivo `appsettings.json` com os dados do seu Oracle DB:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/xe;"
   }
   ```

3. Execute as migrações e crie o schema no banco de dados:
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

### 👥 Usuários e Alertas

| Método | Endpoint              | Descrição                  |
|--------|-----------------------|----------------------------|
| GET    | `/api/usuarios`       | Listar todos os usuários   |
| POST   | `/api/alertas`        | Criar novo alerta          |
| GET    | `/api/sensores`       | Obter dados dos sensores   |

### 🌧️ Eventos Ambientais e Rotas de Fuga

```http
GET    /api/eventos
POST   /api/eventos
PUT    /api/eventos/{id}
DELETE /api/eventos/{id}

GET    /api/rotas
POST   /api/rotas
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

- O projeto segue os princípios de arquitetura em camadas (**API**, **Application**, **Domain**, **Infrastructure**, **Test**).  
- Utiliza **Clean Architecture** e princípios **SOLID**.  
- Ideal para projetos acadêmicos e iniciativas de impacto social com foco em cidades inteligentes.
