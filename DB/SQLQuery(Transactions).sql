USE LoanDb;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
BEGIN
    CREATE TABLE Transactions (
        Id INT PRIMARY KEY IDENTITY(1,1),
        LoanId INT NOT NULL,                -- К какому кредиту относится платеж
        Amount DECIMAL(18,2) NOT NULL,      -- Сумма платежа
        PaymentDate DATETIME DEFAULT GETDATE(),
        PaymentMethod NVARCHAR(50),         -- Card, Cash, Swift
        Note NVARCHAR(255),                 -- Комментарий (например, "Штраф" или "Досрочно")
        
        -- Внешний ключ, связывающий платеж с конкретным кредитом
        CONSTRAINT FK_Transactions_Loans FOREIGN KEY (LoanId) 
        REFERENCES Loans(Id) ON DELETE CASCADE
    );
END
GO

-- Проверка структуры таблицы
SELECT * FROM Transactions;
GO

SELECT 
    T.Id AS TransactionId,
    T.PaymentDate,
    T.Amount AS PaidAmount,
    T.PaymentMethod,
    
    ISNULL(C.Name, CC.CompanyName) AS BorrowerName,
    L.Id AS LoanNumber
FROM Transactions T
JOIN Loans L ON T.LoanId = L.Id
LEFT JOIN Clients C ON L.ClientId = C.Id
LEFT JOIN CorporateClients CC ON L.ClientId = CC.Id
ORDER BY T.PaymentDate DESC;