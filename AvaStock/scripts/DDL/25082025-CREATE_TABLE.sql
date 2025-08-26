/*
    DDL das entidades de domínio (SQL Server)
    - Category
    - Supplier
    - Product
    - OrderItemSale
    - StockHistory

    Observações:
    - Todas as chaves primárias são criadas como constraints nomeadas (PK_*)
    - FKs, CHECKs e DEFAULTs também são nomeados para facilitar manutenção
    - Assumimos BIGINT para todas as PKs (Id/CategoryId/SupplierId/etc.)
      pois no domínio C# há mistura de long/int; aqui padronizamos BIGINT
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ========================================
   Category (Categoria de produto)
   ======================================== */
IF OBJECT_ID('TB_Category','U') IS NULL
BEGIN
    -- Criar tabela de categorias
    CREATE TABLE TB_Category
    (
        CategoryId   BIGINT        NOT NULL IDENTITY(1,1), -- PK (autonumeração)
        Name         NVARCHAR(120) NOT NULL,               -- Nome da categoria
        Description  NVARCHAR(500) NULL                    -- Descrição textual
    );

    -- PK nomeada
    ALTER TABLE TB_Category
        ADD CONSTRAINT PK_Category PRIMARY KEY CLUSTERED (CategoryId);

    -- Nome da categoria único (opcional; remova se não desejar restringir)
    CREATE UNIQUE INDEX UX_Category_Name ON TB_Category(Name);
END
GO

/* ========================================
   Supplier (Fornecedor)
   ======================================== */
IF OBJECT_ID('TB_Supplier','U') IS NULL
BEGIN
    -- Criar tabela de fornecedores
    CREATE TABLE TB_Supplier
    (
        SupplierId BIGINT         NOT NULL IDENTITY(1,1), -- PK
        Name       NVARCHAR(160)  NOT NULL,               -- Razão/Nome
        CNPJ       NVARCHAR(20)   NULL,                   -- CNPJ (opcional, pode vir formatado)
        Phone      NVARCHAR(30)   NULL,
        Email      NVARCHAR(256)  NULL
    );

    -- PK nomeada
    ALTER TABLE TB_Supplier
        ADD CONSTRAINT PK_Supplier PRIMARY KEY CLUSTERED (SupplierId);

    -- Unicidade de CNPJ quando informado (usa índice filtrado para permitir múltiplos NULLs)
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Supplier_CNPJ' AND object_id = OBJECT_ID('TB_Supplier'))
        CREATE UNIQUE INDEX UX_Supplier_CNPJ ON TB_Supplier(CNPJ) WHERE CNPJ IS NOT NULL;
END
GO

/* ========================================
   Product (Produto)
   ======================================== */
IF OBJECT_ID('TB_Product','U') IS NULL
BEGIN
    -- Criar tabela de produtos
    CREATE TABLE TB_Product
    (
        Id           BIGINT         NOT NULL IDENTITY(1,1), -- PK
        SupplierId   BIGINT         NOT NULL,               -- FK -> Supplier
        CategoryId   BIGINT         NOT NULL,               -- FK -> Category (padronizado para BIGINT)
        Name         NVARCHAR(200)  NOT NULL,
        Description  NVARCHAR(1000) NULL,
        PriceCost    DECIMAL(18,2)  NOT NULL,               -- Custo
        PriceSale    DECIMAL(18,2)  NOT NULL,               -- Venda
        CurrentStock INT            NOT NULL                -- Estoque atual (agregado simples)
    );

    -- PK nomeada
    ALTER TABLE TB_Product
        ADD CONSTRAINT PK_Product PRIMARY KEY CLUSTERED (Id);

    -- FK -> Supplier
    ALTER TABLE TB_Product
        ADD CONSTRAINT FK_Product_Supplier
            FOREIGN KEY (SupplierId)
        REFERENCES TB_Supplier(SupplierId);

    -- FK -> Category
    ALTER TABLE TB_Product
        ADD CONSTRAINT FK_Product_Category
            FOREIGN KEY (CategoryId)
        REFERENCES TB_Category(CategoryId);

    -- Regras de domínio (CHECK)
    ALTER TABLE TB_Product
        ADD CONSTRAINT CK_Product_PriceCost_NonNegative CHECK (PriceCost >= 0);

    ALTER TABLE TB_Product
        ADD CONSTRAINT CK_Product_PriceSale_NonNegative CHECK (PriceSale >= 0);

    ALTER TABLE TB_Product
        ADD CONSTRAINT CK_Product_CurrentStock_NonNegative CHECK (CurrentStock >= 0);

    -- Índices úteis
    CREATE INDEX IX_Product_Name ON TB_Product(Name);
    CREATE INDEX IX_Product_CategoryId ON TB_Product(CategoryId);
    CREATE INDEX IX_Product_SupplierId ON TB_Product(SupplierId);
END
GO

/* ========================================
   OrderItemSale (Item de pedido de venda)
   ======================================== */
IF OBJECT_ID('TB_OrderItemSale','U') IS NULL
BEGIN
    -- Criar tabela de itens de pedido (não há tabela Order anexada neste modelo)
    CREATE TABLE TB_OrderItemSale
    (
        ItemId     BIGINT         NOT NULL IDENTITY(1,1), -- PK
        OrderId    BIGINT         NOT NULL,               -- Referência a pedido externo (sem FK aqui)
        ProductId  BIGINT         NOT NULL,               -- FK -> Product
        Quantity   INT            NOT NULL,               -- Quantidade > 0
        UnityPrice DECIMAL(18,2)  NOT NULL                -- Preço unitário >= 0
    );

    -- PK nomeada
    ALTER TABLE TB_OrderItemSale
        ADD CONSTRAINT PK_OrderItemSale PRIMARY KEY CLUSTERED (ItemId);

    -- FK -> Product
    ALTER TABLE TB_OrderItemSale
        ADD CONSTRAINT FK_OrderItemSale_Product
            FOREIGN KEY (ProductId)
        REFERENCES TB_Product(Id);

    -- Regras de domínio
    ALTER TABLE TB_OrderItemSale
        ADD CONSTRAINT CK_OrderItemSale_Quantity_Positive CHECK (Quantity > 0);

    ALTER TABLE TB_OrderItemSale
        ADD CONSTRAINT CK_OrderItemSale_UnityPrice_NonNegative CHECK (UnityPrice >= 0);

    -- Índices úteis
    CREATE INDEX IX_OrderItemSale_OrderId ON TB_OrderItemSale(OrderId);
    CREATE INDEX IX_OrderItemSale_ProductId ON TB_OrderItemSale(ProductId);
END
GO

/* ========================================
   StockHistory (Histórico de movimentação de estoque)
   ======================================== */
IF OBJECT_ID('TB_StockHistory','U') IS NULL
BEGIN
    -- Criar tabela de histórico (ledger simples)
    CREATE TABLE TB_StockHistory
    (
        HistoryId   BIGINT         NOT NULL IDENTITY(1,1), -- PK
        ProductId   BIGINT         NOT NULL,               -- FK -> Product
        ReferenceId BIGINT         NULL,                   -- ID externo (pedido, nota, etc.)
        Type        TINYINT        NOT NULL,               -- 1=Entrada, 2=Saída, 3=Ajuste (exemplo)
        Quantity    INT            NOT NULL,               -- Quantidade > 0 (sinal é representado por Type)
        CreatedAt   DATETIME2(0)   NOT NULL CONSTRAINT DF_StockHistory_CreatedAt DEFAULT (SYSUTCDATETIME()),
        Origin      INT            NULL                    -- Origem (sistema/integração), livre conforme domínio
    );

    -- PK nomeada
    ALTER TABLE TB_StockHistory
        ADD CONSTRAINT PK_StockHistory PRIMARY KEY CLUSTERED (HistoryId);

    -- FK -> Product
    ALTER TABLE TB_StockHistory
        ADD CONSTRAINT FK_StockHistory_Product
            FOREIGN KEY (ProductId)
        REFERENCES TB_Product(Id);

    -- Regras de domínio (ajuste conforme sua enumeração real)
    ALTER TABLE TB_StockHistory
        ADD CONSTRAINT CK_StockHistory_Type_Valid CHECK (Type IN (1,2,3));

    ALTER TABLE TB_StockHistory
        ADD CONSTRAINT CK_StockHistory_Quantity_Positive CHECK (Quantity > 0);

    -- Índices úteis para consultas por produto e data
    CREATE INDEX IX_StockHistory_Product ON TB_StockHistory(ProductId);
    CREATE INDEX IX_StockHistory_CreatedAt ON TB_StockHistory(CreatedAt);
END
GO