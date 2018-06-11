CREATE TABLE [dbo].[Categories]
(
	[Id] INT NOT NULL IDENTITY (1,1), 
    [Name] NVARCHAR(50) NOT NULL

	CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
)
