using BugTrackerPro.BL.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace BugTrackerPro.BL;

public static class BusinessLogicExtension
{
    public static void AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IProjectManager, ProjectManager>();
        services.AddScoped<IBugManager, BugManager>();
        services.AddScoped<IAttachmentManager, AttachmentManager>();
    }
} 