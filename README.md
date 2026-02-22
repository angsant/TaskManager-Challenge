# Task Manager API

## 📋 Sobre o Projeto

Este projeto é uma API RESTful desenvolvida em C# para o gerenciamento de tarefas. Ele foi criado como parte de um desafio técnico , implementando operações completas de CRUD (Create, Read, Update, Delete).

O projeto segue estritamente os princípios de **Clean Architecture**, **SOLID** e **Clean Code**, garantindo desacoplamento entre regras de negócio, persistência de dados e interface de usuário.

## 🚀 Tecnologias Utilizadas

* **Linguagem:** C# (.NET 6/8)
* 
**Framework:** ASP.NET Core Web API 


* 
**Banco de Dados:** SQL Server 


* 
**ORM:** Entity Framework Core 


* **Documentação:** Swagger (OpenAPI)

## 🏗️ Arquitetura

A solução está dividida em 4 camadas principais para respeitar a separação de responsabilidades:

1. 
**Domain:** Contém a entidade `TaskItem` e as regras de negócio (validações de data, regras de status).


2. 
**Application:** Contém as interfaces (`ITaskService`), DTOs e a lógica de aplicação.


3. 
**Infrastructure:** Implementação do Repositório (`TaskRepository`) e contexto do Banco de Dados (EF Core).


4. 
**API (Presentation):** Controllers responsáveis pela comunicação HTTP.



⚙️ Configuração do Ambiente (Local) 

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download) instalado.
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB ou Docker).
* Git instalado.

### 1. Clone o Repositório

```bash
git clone https://github.com/SEU-USUARIO/TaskManager.git
cd TaskManager

```

2. Configuração do Banco de Dados 

O projeto utiliza **SQL Server**. A string de conexão padrão está configurada para usar o `LocalDB`.
Verifique o arquivo `src/TaskManager.API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

```

*Se você estiver usando Docker ou uma instância completa do SQL Server, atualize esta string de conexão.*

### 3. Aplicar Migrations

Para criar o banco de dados e as tabelas necessárias, execute os comandos abaixo na raiz do projeto:

```bash
# Restaura as dependências
dotnet restore

# Aplica as migrações (Cria o banco TaskManagerDb)
dotnet ef database update --project src/TaskManager.Infrastructure --startup-project src/TaskManager.API

```

▶️ Como Rodar a Aplicação 

Para iniciar a API, execute o seguinte comando:

```bash
dotnet run --project src/TaskManager.API

```

A aplicação estará disponível em:

* **Swagger (Documentação):** `https://localhost:7001/swagger` (ou a porta indicada no terminal).
* **API Base:** `https://localhost:7001/api/tasks`

✅ Como Rodar os Testes 

O projeto inclui testes automatizados (se implementados na pasta `tests`). Para executá-los:

```bash
dotnet test

```

## 📚 Documentação da API (Endpoints)

A API fornece os seguintes endpoints para gerenciamento de tarefas:

* `GET /api/tasks`: Retorna todas as tarefas.
* `GET /api/tasks/{id}`: Retorna uma tarefa específica.
* `POST /api/tasks`: Cria uma nova tarefa.
* *Regra:* Título obrigatório, máx 100 caracteres.


* `PUT /api/tasks/{id}`: Atualiza uma tarefa existente.
* *Regra:* Ao mudar status para "Concluída", a data de conclusão é gerada automaticamente.


* `DELETE /api/tasks/{id}`: Remove uma tarefa.

---

**Desenvolvido por:** angsant

---
