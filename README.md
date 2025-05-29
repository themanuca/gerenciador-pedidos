# Gerenciador de Pedidos
O sistema deve permitir que os funcionários cadastrem clientes, produtos e registrem novos pedidos, associando produtos a um cliente.
## Stacks:
- Backend: C#, Asp.Net Core MVC
- Banco de Dados: SQL Server
- ORM: Dapper
- FrontEnd: HTML5, CSS3, Bootstrap, JQuery

## Sql Query
Criação da base ```CREATE DATABASE GerenciadorPedidoDB;``` 

Criação das tabelas:
```SQL
CREATE TABLE Cliente (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100),
    Email NVARCHAR(100),
    Telefone NVARCHAR(20),
    DataCadastro DATETIME DEFAULT GETDATE()
);

CREATE TABLE Pedido (
    Id INT PRIMARY KEY IDENTITY,
    ClienteId INT,
    DataPedido DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20),
    ValorTotal DECIMAL(10,2),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id)
);
CREATE TABLE Produto (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100),
    Descricao NVARCHAR(255),
    Preco DECIMAL(10,2),
    QuantidadeEstoque INT
);

CREATE TABLE PedidoItem (
    Id INT PRIMARY KEY IDENTITY,
    PedidoId INT,
    ProdutoId INT,
    Quantidade INT,
    PrecoUnitario DECIMAL(10,2),
    FOREIGN KEY (PedidoId) REFERENCES Pedido(Id),
    FOREIGN KEY (ProdutoId) REFERENCES Produto(Id)
);
```
## Testes
Testes unitarios com XUnit.
Se houvesse mais tempo teria sido melhor.
![image](https://github.com/user-attachments/assets/f409d749-605d-4698-8c84-e86ccc592ce4)
## Swagger
![image](https://github.com/user-attachments/assets/7b3fce4e-4f14-486a-adbb-01f696d9e674)


Popular a tabela de Produto para facilitar o desenvolvimento e testes
```
INSERT INTO Produto (Nome, Descricao, Preco, QuantidadeEstoque) VALUES
('Teclado', 'Teclado mecânico ABNT2', 199.90, 50),
('Mouse', 'Mouse gamer 7200 DPI', 129.90, 30);
```
