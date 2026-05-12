CREATE PROCEDURE [dbo].[sp_GetMobileSuitById]
	@id int = 0
AS
BEGIN
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
	WHERE Id = @id
    and Deleted = 0;
END;
