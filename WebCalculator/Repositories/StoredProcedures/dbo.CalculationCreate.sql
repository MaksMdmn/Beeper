CREATE PROCEDURE CalculationCreate
@FirstOperand FLOAT,
@SecondOperand FLOAT,
@Result FLOAT,
@Operation INT,
@CreationDate DATETIME,
@UserId INT,
@CalculationId INT OUTPUT
AS
INSERT INTO dbo.Calculations
(
    FirstOperand,
    SecondOperand,
    Operation,
    UserId,
    Result,
	CreationDate
)
VALUES
(   
	@FirstOperand,
    @SecondOperand,
    @Operation,
    @UserId,
    @Result,
	@CreationDate
)
SET @CalculationId = SCOPE_IDENTITY()