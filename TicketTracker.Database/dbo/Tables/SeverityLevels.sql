CREATE TABLE [dbo].[SeverityLevels]
(
	[Id] INT NOT NULL IDENTITY (1,1), 
    [Code] VARCHAR(5) NOT NULL, 
    [Description] VARCHAR(25) NOT NULL

	CONSTRAINT [PK_SeverityLevels] PRIMARY KEY ([Id])
)
