CREATE PROCEDURE dbo.SearchCities
	@cityName NVARCHAR(50) = NULL
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @sql NVARCHAR(MAX);
	SET @sql = 'SELECT CityID
					,CityName
					,StateProvinceID
					,Location
					,LatestRecordedPopulation
					,LastEditedBy
					,ValidFrom
					,ValidTo
				FROM [Application].Cities ';

	DECLARE @whereParameters TABLE (WhereParameter VARCHAR(200) NOT NULL);

	IF(@cityName IS NOT NULL)
	BEGIN
		INSERT INTO @whereParameters (WhereParameter)
		SELECT N' CityName = @cityName '; 
	END;

	IF(SELECT COUNT(1) FROM @whereParameters) > 0
	BEGIN
		DECLARE @whereClause NVARCHAR(MAX) = '';

		SELECT @whereClause = COALESCE(@whereClause + WhereParameter + ' AND ', '')
		FROM @whereParameters;

		SET @whereClause = 'WHERE' + LEFT(@whereClause, LEN(@whereClause) - 4);
		SET @sql = @sql + @whereClause;
	END;

	DECLARE @params NVARCHAR(MAX);

	SET @params = N'@cityName NVARCHAR(50)';

	EXEC sp_executesql @sql, @params, @cityName;
END;
GO