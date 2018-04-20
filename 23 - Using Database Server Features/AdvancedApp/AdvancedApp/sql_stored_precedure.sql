USE AdvancedDb
GO

DROP PROCEDURE IF EXISTS GetBySalary;
GO

CREATE PROCEDURE GetBySalary
	@SalaryFilter decimal
AS
	SELECT * from Employees 
	WHERE Salary > @SalaryFilter AND SoftDeleted = 0
	ORDER BY Salary DESC
GO
