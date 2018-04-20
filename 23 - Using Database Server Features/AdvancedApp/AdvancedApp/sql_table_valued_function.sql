USE AdvancedDb
GO

DROP FUNCTION IF EXISTS GetSalaryTable 
GO

CREATE FUNCTION GetSalaryTable(@SalaryFilter decimal)
RETURNS @employeeInfo TABLE 
(
    SSN nvarchar(450),
    FirstName nvarchar(450),
    FamilyName nvarchar(450),
    Salary decimal(8, 2),
    LastUpdated datetime2(7),
    SoftDeleted bit
) AS
    BEGIN
        INSERT INTO @employeeInfo
        SELECT SSN, FirstName, FamilyName, Salary, LastUpdated, SoftDeleted
        FROM Employees
        WHERE Salary > @SalaryFilter AND SoftDeleted = 0
            ORDER BY Salary DESC
        RETURN
    END
GO
