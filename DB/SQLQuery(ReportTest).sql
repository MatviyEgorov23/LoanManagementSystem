USE LoanDb; 
GO
-- Тимчасово додаємо запис для тесту
INSERT INTO AuditLog (TableName, Action, ChangedBy, ChangedAt)
VALUES ('CorporateClients', 'SELECT', 'User', '2026-04-18 03:15:00');
EXECUTE sp_SecurityAuditReport;