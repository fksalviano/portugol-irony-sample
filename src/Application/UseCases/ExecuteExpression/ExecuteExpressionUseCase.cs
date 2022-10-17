using Application.Commons.PortugolLanguage;
using Application.UseCases.ExecuteExpression.Abstractions;
using Application.UseCases.ExecuteExpression.Extensions;
using Application.UseCases.ExecuteExpression.Ports;
using Irony.Interpreter;
using Irony.Parsing;

namespace Application.UseCases.ExecuteExpression;

public class ExecuteExpressionUseCase : IExecuteExpressionUseCase
{
    private readonly Parser _parser;
    private readonly ScriptApp _scriptApp;

    public ExecuteExpressionUseCase()
    {
        var grammar =  new PortugolGrammar();
        _parser = new Parser(grammar);
        _scriptApp = new ScriptApp(_parser.Language);

    }

    public async Task<ExpressionOutput> ExecuteAsync(ExpressionInput input)
    {
        var tree = _parser.Parse(input.Expression);
        
        if (tree.HasErrors())
        {
            var errorOutput = input.ToOutput(tree.ParserMessages.FirstOrDefault());
            return errorOutput;
        }
        
        var result = await Task.Run(() => _scriptApp.Evaluate(tree));

        var output = input.ToOutput(result);
        return output;
    }
}
