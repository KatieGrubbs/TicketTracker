CREATE PROCEDURE [dbo].[GetResolvedTickets]
AS

BEGIN 
	SELECT
		t.Id,
		t.Subject,
		t.Description,
		t.SeverityLevelId,
		s.Code AS [SeverityLevelCode],
		s.Description AS [SeverityLevelDescription],
		t.CategoryId,
		c.Name AS [CategoryName],
		t.DateCreated,
		t.ReporterId,
		u.Email AS [ReporterName],
		t.IsResolved,
		t.IsDeleted
	FROM Tickets AS t
	JOIN SeverityLevels AS s ON t.SeverityLevelId = s.Id
	JOIN Categories AS c ON t.CategoryId = c.Id
	JOIN AspNetUsers AS u ON t.ReporterId = u.Id
	WHERE t.IsResolved = 1 AND t.IsDeleted = 0
	ORDER BY t.DateCreated DESC
END