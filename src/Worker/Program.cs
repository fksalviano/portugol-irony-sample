using Application.Commons.Extensions;
using Application.UseCases.ExecuteExpression.Abstractions;
using Application.UseCases.ExecuteExpression.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Worker; 

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Initializing...");

        var services = new ServiceCollection()
            .InstallServices();

        var useCase = services
            .GetService<IExecuteExpressionUseCase>();

        Console.WriteLine("Executing Expression...");

        var input = args.ToInput();
        var output = await useCase.Execute(input);

        Console.WriteLine($"Expression Output: {output.Value}");
    }
}
