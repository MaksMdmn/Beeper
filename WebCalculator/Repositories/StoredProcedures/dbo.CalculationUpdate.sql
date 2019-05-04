CREATE PROCEDURE CalculationUpdate
@CalculationId INT,
@FirstOperand FLOAT,
@SecondOperand FLOAT,
@Result FLOAT,
@Operation INT,
@CreationDate DATETIME,
@UserId INT
AS
UPDATE dbo.Calculations
SET FirstOperand= @FirstOperand,
	SecondOperand= @SecondOperand,
	Result= @Result,
	Operation= @Operation,
	CreationDate= @CreationDate,
	UserId= @UserId
WHERE CalculationId= @CalculationId