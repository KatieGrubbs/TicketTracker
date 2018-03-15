CREATE TABLE [dbo].[SeverityLevels]
(
	[SeverityLevelId] INT NOT NULL IDENTITY (1,1), 
    [SeverityLevelCode] VARCHAR(5) NOT NULL, 
    [SeverityLevelName] VARCHAR(25) NOT NULL

	CONSTRAINT [PK_SeverityLevels] PRIMARY KEY ([SeverityLevelId])
)
