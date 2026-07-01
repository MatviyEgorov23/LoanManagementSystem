USE LoanDb;
GO
-- 1. СТВОРЮЄМО ТАБЛИЦЮ АУДИТУ (якщо її ще немає)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog]') AND type in (N'U'))
BEGIN
    CREATE TABLE AuditLog (
        LogID INT PRIMARY KEY IDENTITY(1,1),
        TableName NVARCHAR(50),
        Action NVARCHAR(10),
        ChangedBy NVARCHAR(100),
        ChangedAt DATETIME DEFAULT GETDATE(), 
        RecordID INT
    );
END
GO
-- 2. СТВОРЮЄМО ТРИГЕР
-- Якщо тригер вже був створений з помилкою, додаємо DROP або міняємо на CREATE OR ALTER
CREATE OR ALTER TRIGGER trg_Loans_Audit
ON Loans
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Action NVARCHAR(10);
    
    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted) SET @Action = 'UPDATE';
    ELSE IF EXISTS(SELECT * FROM inserted) SET @Action = 'INSERT';
    ELSE SET @Action = 'DELETE';

    INSERT INTO AuditLog (TableName, Action, ChangedBy, ChangedAt, RecordID)
    SELECT 'Loans', @Action, SYSTEM_USER, GETDATE(), ISNULL(i.Id, d.Id)
    FROM inserted i FULL OUTER JOIN deleted d ON i.Id = d.Id;
END;
GO