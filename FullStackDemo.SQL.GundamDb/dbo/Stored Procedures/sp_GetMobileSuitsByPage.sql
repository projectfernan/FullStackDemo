CREATE PROCEDURE [dbo].[sp_GetMobileSuitsByPage]
	@start INT,
    @length INT,
    @search VARCHAR(120) = ''
AS
BEGIN
    -- Get paginated result
    SELECT Id,
           ModelCode,
           ModelName,
           OperatingSystem,
           PowerOutput,
           Armor,
           Height,
           [Weight],
           Manufacturer,
           DateCreated,
           CreatedBy,
           DateEdited,
           EditedBy
	FROM MobileSuits
	WHERE (ModelCode LIKE '%' + @search + '%'
	OR ModelName LIKE '%' + @search + '%'
	OR OperatingSystem LIKE '%' + @search + '%'
	OR PowerOutput LIKE '%' + @search + '%'
	OR Armor LIKE '%' + @search + '%'
	OR Height LIKE '%' + @search + '%'
	OR [Weight] LIKE '%' + @search + '%'
	OR Manufacturer LIKE '%' + @search + '%')
    AND Deleted = 0
    ORDER BY Id
    OFFSET @start ROWS FETCH NEXT @length ROWS ONLY;

    -- Get the total count in a separate query
    SELECT dbo.GetMobileSuitsCount(@search) AS TotalMobilSuitsCount;
END;
