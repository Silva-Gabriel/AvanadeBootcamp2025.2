/*
    Script de inserção de dados de teste para as tabelas TB_*
    Data: 27/08/2025
    Estrutura baseada no DDL das entidades
*/
SET NOCOUNT ON;
GO

-- Categoria
INSERT INTO TB_Category (Name, Description)
VALUES ('Eletrônicos', 'Produtos eletrônicos em geral'),
       ('Acessórios', 'Acessórios para dispositivos eletrônicos');
GO

-- Fornecedor
INSERT INTO TB_Supplier (Name, CNPJ, Phone, Email)
VALUES ('Fornecedor ABC', '12345678000199', '(11) 99999-0001', 'contato@abc.com'),
       ('Fornecedor XYZ', '98765432000188', '(21) 88888-0002', 'vendas@xyz.com');
GO

-- Produto
INSERT INTO TB_Product (SupplierId, CategoryId, Name, Description, PriceCost, PriceSale, CurrentStock)
VALUES (1, 1, 'Fone Bluetooth', 'Fone de ouvido sem fio', 50.00, 99.90, 100),
       (2, 2, 'Capa Protetora', 'Capa para smartphone', 10.00, 29.90, 200),
       (1, 1, 'Carregador USB-C', 'Carregador rápido USB-C 20W', 20.00, 49.90, 150);
GO

-- Item de pedido de venda
INSERT INTO TB_OrderItemSale (OrderId, ProductId, Quantity, UnityPrice)
VALUES (1001, 1, 2, 99.90),
       (1001, 2, 1, 29.90),
       (1002, 3, 3, 49.90);
GO

-- Histórico de estoque
INSERT INTO TB_StockHistory (ProductId, ReferenceId, Type, Quantity, CreatedAt, Origin)
VALUES (1, 1001, 2, 2, SYSUTCDATETIME(), 1), -- Saída (venda)
       (2, 1001, 2, 1, SYSUTCDATETIME(), 1), -- Saída (venda)
       (3, NULL, 1, 150, SYSUTCDATETIME(), 2), -- Entrada (compra)
       (1, NULL, 3, 5, SYSUTCDATETIME(), 3); -- Ajuste
GO

PRINT 'Dados de teste inseridos com sucesso.';
GO