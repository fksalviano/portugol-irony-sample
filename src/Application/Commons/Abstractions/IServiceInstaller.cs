using Microsoft.Extensions.DependencyInjection;

namespace Application.Commons.Abstractions;

public interface IServiceInstaller
{
    void InstallServices(IServiceCollection services);
}