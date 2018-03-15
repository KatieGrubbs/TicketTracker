CREATE PROCEDURE [dbo].[GetOpenTickets]
AS

BEGIN 
	SELECT
		t.TicketId,
		t.Subject,
		t.Description,
		t.SeverityLevelId,
		s.SeverityLevelCode,
		s.SeverityLevelName,
		t.CategoryId,
		c.CategoryName,
		t.DateCreated,
		t.ReporterId,
		u.Email AS [ReporterName],
		t.IsResolved,
		t.IsDeleted
	FROM Tickets AS t
	JOIN SeverityLevels AS s ON t.SeverityLevelId = s.SeverityLevelId
	JOIN Categories AS c ON t.CategoryId = c.CategoryId
	JOIN AspNetUsers AS u ON t.ReporterId = u.Id
	WHERE t.IsResolved = 0 AND t.IsDeleted = 0
	ORDER BY t.DateCreated DESC
END