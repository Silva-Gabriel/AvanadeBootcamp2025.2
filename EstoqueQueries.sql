-- Selecionar produto
SELECT *  FROM TB_Product;

-- Produto -> Categoria
SELECT * FROM TB_Product product
INNER JOIN TB_Category category ON product.CategoryId = category.CategoryId;

-- Produto > Categoria > Fornecedor > Pedido > Histórico
-- Name, Description, UnityPrice - PriceSale  -> Lucro?,  CurrentStock, Name (Categoria), Description (Categoria), Name (Fornecedor), Phone (Fornecedor), Email (Fornecedor), OrderId, Quantity (Pedido)
SELECT product.Name AS Produto, product.Description AS Descricao, (product.PriceSale - product.PriceCost) AS Lucro,  product.CurrentStock AS QuantidadeEstoque, category.Name AS Categoria, category.Description DescricaoCategoria, supplier.Name AS NomeFornecedor, supplier.Phone AS CelularFornecedor, supplier.Email EmailFornecedor, orderItem.Quantity AS QuantidadeVendida FROM TB_Product product
INNER JOIN TB_Category category ON product.CategoryId = category.CategoryId
INNER JOIN TB_Supplier supplier ON product.SupplierId = supplier.SupplierId
INNER JOIN TB_OrderItemSale orderItem ON product.Id = orderItem.ProductId;

-- Produto > Categoria > Fornecedor > Pedido > Histórico
SELECT * FROM TB_Product product
INNER JOIN TB_Category category ON product.CategoryId = category.CategoryId
INNER JOIN TB_Supplier supplier ON product.SupplierId = supplier.SupplierId
INNER JOIN TB_OrderItemSale orderItem ON product.Id = orderItem.ProductId
INNER JOIN TB_StockHistory history ON product.Id = history.ProductId;