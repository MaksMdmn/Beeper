﻿CREATE PROCEDURE UserSelectById
@UserId INT
AS
SELECT  * FROM Users WHERE UserId = @UserId