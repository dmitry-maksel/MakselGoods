
using FluentValidation;
using Identity.API.Core.Validators;

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
