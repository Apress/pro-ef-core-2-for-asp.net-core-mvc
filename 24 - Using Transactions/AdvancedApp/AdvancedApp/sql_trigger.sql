USE AdvancedDb
GO

DROP TRIGGER IF EXISTS GeneratedValueTrigger
GO

CREATE TRIGGER GeneratedValueTrigger ON Employees
	AFTER  INSERT, UPDATE
AS
BEGIN
	DECLARE @Salary decimal(8,0), @SSN nvarchar(450), 
		@First nvarchar(450), @Family nvarchar(450)
	
	SELECT @Salary = INSERTED.Salary, @SSN = INSERTED.SSN, 
		@First = INSERTED.FirstName, @Family = INSERTED.FamilyName
	FROM INSERTED
          
	UPDATE dbo.Employees SET GeneratedValue = FLOOR(@Salary /2)
	WHERE SSN = @SSN AND FirstName = @First AND FamilyName = @Family
END
