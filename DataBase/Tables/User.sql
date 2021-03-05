CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
    [firstName] VARCHAR(50) NOT NULL, 
    [lastName] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Admin] BIT NOT NULL 
)
