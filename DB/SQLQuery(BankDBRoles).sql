USE LoanDb;
-- 1. Створення ролей
CREATE ROLE BankAdmin;    -- Повний доступ
CREATE ROLE BankOperator; -- Читання/Запис кредитних рахунків
CREATE ROLE BankGuest;    -- Тільки читання

-- 2. Призначення прав
GRANT CONTROL TO BankAdmin;

GRANT SELECT, INSERT, UPDATE ON Loans TO BankOperator;
GRANT SELECT, INSERT, UPDATE ON Transactions TO BankOperator;
GRANT SELECT ON CorporateClients TO BankOperator;

GRANT SELECT ON Loans TO BankGuest;
GRANT SELECT ON CorporateClients TO BankGuest;

-- 3. Створення користувачів та призначення ролей
CREATE LOGIN AdminUser WITH PASSWORD = 'StrongPassword123!';
CREATE USER AdminUser FOR LOGIN AdminUser;
ALTER ROLE BankAdmin ADD MEMBER AdminUser;

CREATE LOGIN OperatorIvan WITH PASSWORD = 'OperatorPass!123';
CREATE USER OperatorIvan FOR LOGIN OperatorIvan;
ALTER ROLE BankOperator ADD MEMBER OperatorIvan;

CREATE LOGIN GuestUser WITH PASSWORD = 'GuestPass!123';
CREATE USER GuestUser FOR LOGIN GuestUser;
ALTER ROLE BankGuest ADD MEMBER GuestUser;