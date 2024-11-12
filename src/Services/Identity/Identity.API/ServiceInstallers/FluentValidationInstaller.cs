
using FluentValidation;
using Identity.API.Core.CQRS.Commands.Login;

namespace Identity.API.ServiceInstallers
{
    public class FluentValidationInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<LoginQueryValidator>();
        }
    }
}
