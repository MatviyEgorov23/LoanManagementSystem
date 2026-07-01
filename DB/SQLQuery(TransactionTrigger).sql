USE LoanDb;
GO

CREATE OR ALTER TRIGGER trg_Transactions_Audit
ON Transactions
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Action NVARCHAR(10);
    
    -- Визначаємо тип операції
    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted) SET @Action = 'UPDATE';
    ELSE IF EXISTS(SELECT * FROM inserted) SET @Action = 'INSERT';
    ELSE SET @Action = 'DELETE';

    -- Записуємо дію в журнал аудиту
    INSERT INTO AuditLog (TableName, Action, ChangedBy, ChangedAt, RecordID)
    SELECT 'Transactions', @Action, SYSTEM_USER, GETDATE(), ISNULL(i.Id, d.Id)
    FROM inserted i FULL OUTER JOIN deleted d ON i.Id = d.Id;
END;
GO