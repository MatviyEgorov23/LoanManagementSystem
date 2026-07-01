USE LoanDb; 
GO

CREATE PROCEDURE sp_SecurityAuditReport
AS
BEGIN
    PRINT '--- SECURITY AUDIT REPORT: SUSPICIOUS ACTIVITY ---';
    -- Вибірка з журналу аудиту дій, виконаних не адміністратором у неробочий час
    SELECT TableName, Action, ChangedBy, ChangedAt
    FROM AuditLog
    WHERE (CAST(ChangedAt AS TIME) < '08:00:00' OR CAST(ChangedAt AS TIME) > '19:00:00')
    ORDER BY ChangedAt DESC;
END;
GO