using System.Diagnostics.Contracts;
using Application.UseCases.ExecuteExpression.Ports;

namespace Application.UseCases.ExecuteExpression.Extensions;

public static class ExecuteExpressionExtensions
{
    public static ExpressionOutput ToOutput(this ExpressionInput input, object value) =>
        new ExpressionOutput(input.ExecutionId, value);

    public static ExpressionInput ToInput(this string[] args) =>
        new ExpressionInput(Guid.NewGuid(), args[0]);
}
