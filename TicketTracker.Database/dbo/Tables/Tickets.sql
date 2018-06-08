CREATE TABLE [dbo].[Tickets]
(
	[TicketId] INT NOT NULL IDENTITY (1,1), 
    [Subject] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
	[SeverityLevelId] INT NOT NULL DEFAULT 3, -- Moderate severity by default
    [CategoryId] INT NOT NULL, 
    [DateCreated] DATETIME NULL DEFAULT GETDATE(), 
    [ReporterId] NVARCHAR(128) NULL , 
    [IsResolved] BIT NOT NULL DEFAULT 0,
	[IsDeleted] BIT NOT NULL DEFAULT 0, 

	CONSTRAINT [PK_Tickets] PRIMARY KEY ([TicketId]),
    CONSTRAINT [FK_Tickets_SeverityLevels] FOREIGN KEY ([SeverityLevelId])
		REFERENCES dbo.SeverityLevels ([SeverityLevelId]),
	CONSTRAINT [FK_Tickets_Categories] FOREIGN KEY ([CategoryId])
		REFERENCES dbo.Categories ([CategoryId]),
	CONSTRAINT [FK_Tickets_AspNetUsers] FOREIGN KEY ([ReporterId])
		REFERENCES dbo.AspNetUsers ([Id])
)
