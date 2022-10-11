using Application.UseCases.ExecuteExpression.Abstractions;
using Application.UseCases.ExecuteExpression.Extensions;
using Application.UseCases.ExecuteExpression.Ports;

namespace Application.UseCases.ExecuteExpression;

public class ExecuteExpressionUseCase : IExecuteExpressionUseCase
{
    public async Task<ExpressionOutput> Execute(ExpressionInput input)
    {
        var outputValue = (object)"OUTPUT TEST"; 

        var output = input.ToOutput(outputValue);
        return await Task.FromResult(output);
    }
}
