🚀 Como Executar o Projeto
Pré-requisitos

.NET 8 SDK - Download
SQL Server (LocalDB, Express ou completo)
Visual Studio 2022 ou VS Code com extensão C#

Passos para Executar

Clone ou extraia o projeto
Configure a Connection String
Edite o arquivo appsettings.json e ajuste a connection string conforme seu ambiente:

Exemplos de connection strings:

SQL Server LocalDB: Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=True;
SQL Server Express: Server=localhost\\SQLEXPRESS;Database=LojaDB;Trusted_Connection=True;TrustServerCertificate=True;
SQL Server com usuário/senha: Server=localhost;Database=LojaDB;User Id=sa;Password=SuaSenha;TrustServerCertificate=True;


Restaurar os pacotes NuGet => dotnet restore

Criar o banco de dados (Migrations)
O sistema aplica as migrations automaticamente ao iniciar, mas você pode fazer manualmente:
dotnet ef migrations add InitialCreate
dotnet ef database update
Executar o projeto
dotnet run

📋 Funcionalidades Implementadas
✅ Produtos

Cadastro de produtos (nome, descrição, preço)
Listagem de todos os produtos
Edição de produtos
Inativação de produtos
Validação de dados (campos obrigatórios, preço positivo)

✅ Pedidos

Criação de pedidos com múltiplos produtos
Seleção de produtos com quantidade
Cálculo automático de valores
Listagem de todos os pedidos
Visualização detalhada de cada pedido com itens

✅ Técnico

DDD (Domain-Driven Design) com camadas separadas
Padrão MVC para organização do código
Entity Framework Core com SQL Server
Razor Pages para o frontend
Validação de dados no backend e frontend
Migrations automáticas
Design responsivo e moderno

🎨 Navegação do Sistema

Página Inicial (/)

Apresentação do sistema
Links rápidos para Produtos e Pedidos


Produtos (/Produtos)

Lista todos os produtos cadastrados
Botão "Novo Produto" para cadastro
Botões "Editar" e "Inativar" em cada produto


Novo Produto (/Produtos/Create)

Formulário de cadastro
Validação em tempo real


Editar Produto (/Produtos/Edit/{id})

Formulário preenchido com dados atuais
Atualização dos dados


Pedidos (/Pedidos)

Lista todos os pedidos realizados
Botão "Ver Detalhes" para cada pedido


Novo Pedido (/Pedidos/Create)

Seleção de produtos disponíveis
Adição de múltiplos itens
Cálculo automático do total


Detalhes do Pedido (/Pedidos/Details/{id})

Informações completas do pedido
Lista de todos os itens



🛠️ Tecnologias Utilizadas

.NET 8.0
ASP.NET Core MVC
Entity Framework Core 8.0
SQL Server
Razor Pages
HTML5 / CSS3
JavaScript

📝 Padrões e Arquitetura

DDD (Domain-Driven Design)

Entidades com lógica de negócio encapsulada
Repositórios para acesso a dados
Services para orquestração de operações


MVC (Model-View-Controller)

Controllers para gerenciar requisições
Views Razor para apresentação
Models (DTOs) para transferência de dados


Repository Pattern

Abstração do acesso a dados
Facilita testes e manutenção
