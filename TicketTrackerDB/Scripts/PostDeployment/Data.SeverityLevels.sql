BEGIN
	IF NOT EXISTS (SELECT * FROM SeverityLevels)
	BEGIN
		INSERT INTO SeverityLevels (SeverityLevelCode, SeverityLevelName)
		VALUES
			('S1', 'Critical'),
			('S2', 'Major'),
			('S3', 'Moderate'),
			('S4', 'Low')
	END
END