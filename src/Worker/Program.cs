[assembly: ExcludeFromCodeCoverage]

var services = new ServiceCollection()
    .InstallServices();

var useCase = services
    .GetService<IExecuteExpressionUseCase>();

var input = args.ToInput();
var output = await useCase.Execute(input);

Console.WriteLine($"Expression Output: {output.Value}");