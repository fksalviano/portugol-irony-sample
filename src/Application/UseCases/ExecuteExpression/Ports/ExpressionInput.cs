namespace Application.UseCases.ExecuteExpression.Ports;

public record ExpressionInput
(
    Guid ExecutionId,
    string Expression
);
