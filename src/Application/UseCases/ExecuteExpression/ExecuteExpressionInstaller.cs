using Application.Commons.Abstractions;
using Application.UseCases.ExecuteExpression.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.ExecuteExpression;

public class ExecuteExpressionInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services) =>
        services
            .AddSingleton<IExecuteExpressionUseCase, ExecuteExpressionUseCase>();
}
