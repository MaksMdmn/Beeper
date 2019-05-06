CREATE PROCEDURE CalculationSelectByUserIpAddress
@IpAddress VARCHAR(20)
AS
SELECT * FROM dbo.Calculations
INNER JOIN dbo.Users ON Users.UserId = Calculations.UserId
WHERE IpAddress = @IpAddress