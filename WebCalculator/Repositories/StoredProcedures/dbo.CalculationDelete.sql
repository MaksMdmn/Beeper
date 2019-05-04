CREATE PROCEDURE CalculationDelete
@CalculationId INT
AS
DELETE FROM dbo.Calculations WHERE CalculationId=@CalculationId