﻿CREATE PROCEDURE UserDelete
@UserId INT
AS
DELETE FROM dbo.Users WHERE UserId = @UserId