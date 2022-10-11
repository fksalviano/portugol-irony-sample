using Application.Commons.PortugolLanguage;
using Application.UseCases.ExecuteExpression.Abstractions;
using Application.UseCases.ExecuteExpression.Extensions;
using Application.UseCases.ExecuteExpression.Ports;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Application.UseCases.ExecuteExpression;

public class ExecuteExpressionUseCase : IExecuteExpressionUseCase
{
    private readonly Parser _parser;

    public ExecuteExpressionUseCase()
    {
        var portugol =  new PortugolGrammar();
        _parser = new Parser(portugol);
    }

    public async Task<ExpressionOutput> Execute(ExpressionInput input)
    {
        var tree = _parser.Parse(input.Expression);

        object? result = null;
        
        if (!tree.HasErrors() && tree.Root.AstNode != null)
        {
            var astNode = (AstNode)tree.Root.AstNode;
            result = astNode.Evaluate(null);
        }

        var output = input.ToOutput(result);

        return await Task.FromResult(output);
    }
}
