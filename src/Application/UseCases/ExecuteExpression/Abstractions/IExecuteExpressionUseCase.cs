using Application.UseCases.ExecuteExpression.Ports;

namespace Application.UseCases.ExecuteExpression.Abstractions;

public interface IExecuteExpressionUseCase
{
    Task<ExpressionOutput> ExecuteAsync(ExpressionInput input);
}
