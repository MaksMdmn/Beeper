CREATE PROCEDURE UserSelectByIpAddress
@IpAddress VARCHAR(20) 
AS
SELECT  * FROM Users WHERE IpAddress = @IpAddress