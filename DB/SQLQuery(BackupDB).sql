USE master;
GO
-- Створення повного бекапу
BACKUP DATABASE [LoanDb] 
TO DISK = N'C:\SQLBackups\LoanDb_Full.bak' 
WITH NOFORMAT, NOINIT,  
NAME = N'LoanDb-Full Database Backup', 
SKIP, NOREWIND, NOUNLOAD, STATS = 10;
GO