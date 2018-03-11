CREATE PROCEDURE [dbo].[GetOpenTickets]
AS

BEGIN 
	SELECT
		t.TicketId,
		t.Subject,
		t.Description,
		t.SeverityLevelId,
		s.SeverityLevelName,
		t.CategoryId,
		c.CategoryName,
		t.DateCreated,
		--u.Email,
		t.IsResolved
	FROM Tickets AS t
	JOIN SeverityLevels AS s ON t.SeverityLevelId = s.SeverityLevelId
	JOIN Categories AS c ON t.CategoryId = c.CategoryId
	--JOIN AspNetUsers AS u ON t.ReporterId = u.Id
	WHERE t.IsResolved = 0
END