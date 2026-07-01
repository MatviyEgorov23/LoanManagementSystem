USE LoanDb;
GO

-- 4. Создаем таблицу кредитов, если ее нет
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Loans]') AND type in (N'U'))
BEGIN
    CREATE TABLE Loans (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ClientId INT NOT NULL, 
        Amount DECIMAL(18, 2) NOT NULL,
        InterestRate DECIMAL(5, 2) NOT NULL, 
        DurationMonths INT NOT NULL,
        StartDate DATE DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Active',
        CONSTRAINT FK_Loans_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE
    );
END
GO

SELECT * FROM Loans;
GO