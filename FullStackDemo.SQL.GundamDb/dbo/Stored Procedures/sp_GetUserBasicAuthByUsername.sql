CREATE PROCEDURE [dbo].[sp_GetUserBasicAuthByUsername]
	@username varchar(50) = 0
AS
BEGIN
	SELECT Username,
           [Password]
	FROM UserBasicAuth
	WHERE Username = @username
	and Deleted = 0;
END;