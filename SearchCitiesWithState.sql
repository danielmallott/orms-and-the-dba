CREATE PROCEDURE dbo.SearchCitiesWithState
	@cityName NVARCHAR(50) = NULL
	,@stateName NVARCHAR(50) = NULL
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @sql NVARCHAR(MAX);
	SET @sql = 'SELECT C.CityID
					,C.CityName
					,C.StateProvinceID
					,C.Location
					,C.LatestRecordedPopulation
					,C.LastEditedBy
					,C.ValidFrom
					,C.ValidTo
				FROM [Application].Cities AS C ';

	DECLARE @whereParameters TABLE (WhereParameter VARCHAR(200) NOT NULL);

	IF(@cityName IS NOT NULL)
	BEGIN
		INSERT INTO @whereParameters (WhereParameter)
		SELECT N' C.CityName = @cityName '; 
	END;

	IF(@stateName IS NOT NULL)
	BEGIN
		INSERT INTO @whereParameters (WhereParameter)
		SELECT N' SP.StateProvinceName = @stateName ';

		SET @sql = @sql + ' INNER JOIN [Application].StateProvinces AS SP
								ON C.StateProvinceID = SP.StateProvinceID ';
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

	SET @params = N'@cityName NVARCHAR(50), @stateName NVARCHAR(50)';

	EXEC sp_executesql @sql, @params, @cityName, @stateName;
END;
GO