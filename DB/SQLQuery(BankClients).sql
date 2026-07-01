-- 1. Создаем базу данных, если её нет
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LoanDb')
BEGIN
    CREATE DATABASE LoanDb;
END
GO

USE LoanDb;
GO

-- 2. Создаем таблицу, если её нет
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND type in (N'U'))
BEGIN
    CREATE TABLE Clients (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        Position NVARCHAR(100),
        Office NVARCHAR(100),
        Age INT,
        StartDate DATE DEFAULT GETDATE()
    );
END
GO

-- 3. Вывод всех данных для проверки
SELECT * FROM Clients;
GO
