USE LoanDb;
GO
CREATE PROCEDURE GetLoanAnalytics
AS
BEGIN
    SELECT 
        COUNT(*) AS TotalActiveLoans,
        SUM(Amount) AS TotalLoanedAmount,
        AVG(InterestRate) AS AverageRate,
        (SELECT COUNT(*) FROM Loans WHERE Status = 'Overdue') AS OverdueCount
    FROM Loans
    WHERE Status = 'Active';
END;