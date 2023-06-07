using System.Reflection;
using Assignment.Domain.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Application.Contact;

public static class ApplicationServiceCollectionExtensionForContact
{
    // todo: This extension is both in the Assignment.Application.Contact and Assignment.Application.Report.
    // It is a workaround. Didnt had time to figure out how to make it work with one extension.
    public static void ConfigureApplicationServicesForContact(this IServiceCollection services)
    {
        services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
            .Where(c => c.Name.EndsWith("AppService"))
            .AsPublicImplementedInterfaces();
    }
}
