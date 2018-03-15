CREATE TABLE [dbo].[Categories]
(
	[CategoryId] INT NOT NULL IDENTITY (1,1), 
    [CategoryName] NVARCHAR(50) NOT NULL

	CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
)
