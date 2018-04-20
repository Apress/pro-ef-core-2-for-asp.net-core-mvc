USE AdvancedDb
GO

DROP PROCEDURE IF EXISTS RestoreSoftDelete
DROP PROCEDURE IF EXISTS PurgeSoftDelete
GO

CREATE PROCEDURE RestoreSoftDelete
AS
    BEGIN
        UPDATE Employees
	 SET SoftDeleted = 0 WHERE SoftDeleted = 1
    END
GO

CREATE PROCEDURE PurgeSoftDelete
AS 
    BEGIN
        DELETE from SecondaryIdentity WHERE Id IN 
	    ( SELECT Id from Employees emp
            INNER JOIN SecondaryIdentity ident on ident.PrimarySSN = emp.SSN
            AND ident.PrimaryFirstName = emp.FirstName
            AND ident.PrimaryFamilyName = emp.FamilyName
            WHERE SoftDeleted = 1)
    END
    BEGIN
        DELETE FROM Employees 
        WHERE SoftDeleted = 1
    END
