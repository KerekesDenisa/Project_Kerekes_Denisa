CREATE TABLE [dbo].[Order] (
    [OrderID]     INT IDENTITY (1, 1) NOT NULL,
    [CustomerID]  INT NOT NULL,
    [ChocolateID] INT NOT NULL,
    [OrderDate] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [FK_Order_Chocolate_ChocolateID] FOREIGN KEY ([ChocolateID]) REFERENCES [dbo].[Chocolate] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Order_Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Order_ChocolateID]
    ON [dbo].[Order]([ChocolateID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Order_CustomerID]
    ON [dbo].[Order]([CustomerID] ASC);

