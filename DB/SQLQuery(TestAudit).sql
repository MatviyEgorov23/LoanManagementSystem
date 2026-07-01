USE LoanDb;
GO
-- 1. Фіксуємо час початку тесту
DECLARE @StartTime DATETIME = GETDATE();
DECLARE @Counter INT = 1;
PRINT 'Starting Stress Test: Inserting 1000 transactions...';
-- 2. Цикл масової вставки
WHILE @Counter <= 1000
BEGIN
    INSERT INTO Transactions (LoanId, Amount, PaymentDate, PaymentMethod, Note)
    VALUES (
        (SELECT TOP 1 Id FROM Loans ORDER BY NEWID()), -- Випадковий кредит
        RAND() * 1000,                                 -- Випадкова сума
        GETDATE(), 
        'StressTest', 
        'Batch automated entry'
    );
    SET @Counter = @Counter + 1;
END
-- 3. Фіксуємо час завершення
DECLARE @EndTime DATETIME = GETDATE();
SELECT 
    DATEDIFF(ms, @StartTime, @EndTime) AS Duration_Milliseconds,
    (SELECT COUNT(*) FROM AuditLog WHERE TableName = 'Transactions') AS Total_Audit_Records;