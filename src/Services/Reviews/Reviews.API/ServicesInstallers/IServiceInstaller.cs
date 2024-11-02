namespace Reviews.API.ServicesInstallers
{
    public interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
