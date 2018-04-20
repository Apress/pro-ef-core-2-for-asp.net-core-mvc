USE ZoomShoesDb

CREATE TABLE Fittings (
	Id bigint IDENTITY(1,1) NOT NULL,
	Name nvarchar(max) NOT NULL,
CONSTRAINT PK_Fittings PRIMARY KEY (Id));
GO

SET IDENTITY_INSERT Fittings ON 
INSERT Fittings (Id, Name) 
	VALUES (1, N'Narrow'), 
			(2, N'Standard'),
			(3, N'Wide'),
			(4, N'Big Foot')
SET IDENTITY_INSERT Fittings OFF
GO

ALTER TABLE Shoes 
    ADD FittingId bigint
ALTER TABLE Shoes
    ADD CONSTRAINT FK_Shoes_Fittings FOREIGN KEY(FittingId) REFERENCES Fittings (Id)
GO

UPDATE Shoes SET FittingId = 2
GO

SELECT * from Shoes
