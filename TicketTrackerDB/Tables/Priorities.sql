CREATE TABLE [dbo].[Priorities]
(
	[PriorityId] INT NOT NULL IDENTITY (1,1), 
    [PriorityCode] VARCHAR(10) NOT NULL, 
    [PriorityName] VARCHAR(50) NOT NULL

	CONSTRAINT [PK_Priorities] PRIMARY KEY ([PriorityId])
)
