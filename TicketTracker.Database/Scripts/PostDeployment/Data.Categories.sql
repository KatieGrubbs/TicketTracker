BEGIN
	IF NOT EXISTS (SELECT * FROM Categories)
	BEGIN
		INSERT INTO Categories(Name)
		VALUES
			('Bug'),
			('Usability'),
			('Performance'),
			('Polish'),
			('Security issue'),
			('Feature'),
			('Maintenance'),
			('Think/Check'),
			('Design'),
			('Visuals'),
			('Infrastructure')
	END
END