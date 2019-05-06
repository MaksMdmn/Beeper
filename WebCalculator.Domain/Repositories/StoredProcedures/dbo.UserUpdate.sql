CREATE PROCEDURE UserUpdate
@UserId INT,
@IpAddress VARCHAR(20)
AS
UPDATE dbo.Users
SET IpAddress = @IpAddress
WHERE UserId = @UserId