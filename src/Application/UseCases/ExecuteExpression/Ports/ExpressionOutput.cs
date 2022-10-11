namespace Application.UseCases.ExecuteExpression.Ports;

public record ExpressionOutput
(
    Guid ExecutionId,
    object Value
);
