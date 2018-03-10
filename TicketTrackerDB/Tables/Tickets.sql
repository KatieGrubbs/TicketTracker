CREATE TABLE [dbo].[Tickets]
(
	[TicketId] INT NOT NULL IDENTITY (1,1), 
    [PriorityId] INT NOT NULL, 
    [Subject] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [CategoryId] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [CreatedBy] NVARCHAR(256) NOT NULL DEFAULT SUSER_NAME(), 
    [IsResolved] BIT NOT NULL DEFAULT 0

	CONSTRAINT [PK_Tickets] PRIMARY KEY ([TicketId]),
    CONSTRAINT [FK_Tickets_Priorities] FOREIGN KEY ([PriorityId])
		REFERENCES dbo.Priorities ([PriorityId]),
	CONSTRAINT [FK_Tickets_Categories] FOREIGN KEY ([CategoryId])
		REFERENCES dbo.Categories ([CategoryId]),
	--CONSTRAINT [FK_Tickets_AspNetUsers] FOREIGN KEY ([CreatedBy])
	--	REFERENCES dbo.AspNetUsers ([Id])
)
