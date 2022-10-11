using System.Reflection;
using Application.Commons.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commons.Extensions;

public static class InstallerExtensions
{
    public static IServiceCollection InstallServices(this IServiceCollection services)
    {
        var installersTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(IServiceInstaller).IsAssignableFrom(type) && type.IsClass);

        foreach (var type in installersTypes)
        {
            var installer = (IServiceInstaller)Activator.CreateInstance(type)!;
            installer.InstallServices(services);
        }
        return services;
    }

    public static T GetService<T>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<T>()!;
}
