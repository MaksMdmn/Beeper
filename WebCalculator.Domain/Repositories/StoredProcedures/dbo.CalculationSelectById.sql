CREATE PROCEDURE CalculationSelectById
@CalculationId INT
AS
SELECT  * FROM Calculations WHERE CalculationId = @CalculationId