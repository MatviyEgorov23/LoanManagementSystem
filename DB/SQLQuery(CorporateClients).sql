USE LoanDb;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporateClients]') AND type in (N'U'))
BEGIN
    CREATE TABLE CorporateClients (
        Id INT PRIMARY KEY IDENTITY(1,1),
        CompanyName NVARCHAR(200) NOT NULL,
        Industry NVARCHAR(100),              -- Отрасль
        RegistrationNumber NVARCHAR(50),     -- ЕГРПОУ / ИНН
        City NVARCHAR(100),
        Address NVARCHAR(500),
        ContactPhone NVARCHAR(20),
        Email NVARCHAR(100),
        
        -- Финансовые показатели
        AssetsValue DECIMAL(18,2),           -- Активы
        AnnualTurnover DECIMAL(18,2),        -- Товарооборот
        NetProfit DECIMAL(18,2),             -- Чистая прибыль
        TaxDebt DECIMAL(18,2) DEFAULT 0,     -- Задолженность по налогам
        EmployeeCount INT,                   -- Кол-во сотрудников
        
        -- Кредитные параметры
        RequestedAmountMin DECIMAL(18,2),    -- Мин. сумма
        RequestedAmountMax DECIMAL(18,2),    -- Макс. сумма
        ProposedInterestRate DECIMAL(5,2),   -- Процентная ставка
        LoanPurpose NVARCHAR(MAX),           -- Цель кредита
        
        -- Юридические детали
        FinancialReportSummary NVARCHAR(MAX),-- Краткий фин. отчет
        IsBankrupt BIT DEFAULT 0,            -- Статус банкротства
        CEO_Name NVARCHAR(200),              -- ФИО Директора
        
        -- Служебные
        ApplicationDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Pending' -- Статус заявки
    );
END
GO

SELECT * FROM CorporateClients;
GO