USE AdvancedDb
GO

DROP VIEW IF EXISTS NotDeletedView
GO

CREATE VIEW NotDeletedView
AS
	SELECT * FROM Employees
	WHERE SoftDeleted = 0
GO
