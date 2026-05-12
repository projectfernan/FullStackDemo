CREATE FUNCTION [dbo].[GetMobileSuitsCount]
(
	@search VARCHAR(120) = ''
)
RETURNS INT
AS
BEGIN
	DECLARE @totalCount int = 0;

	SELECT @totalCount = COUNT(*)
	FROM MobileSuits
	WHERE (ModelCode LIKE '%' + @search + '%'
	OR ModelName LIKE '%' + @search + '%'
	OR OperatingSystem LIKE '%' + @search + '%'
	OR PowerOutput LIKE '%' + @search + '%'
	OR Armor LIKE '%' + @search + '%'
	OR Height LIKE '%' + @search + '%'
	OR [Weight] LIKE '%' + @search + '%'
	OR Manufacturer LIKE '%' + @search + '%')
	AND Deleted = 0;

	RETURN @totalCount;
END
