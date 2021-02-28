using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFmaintenance.Views;

namespace KFmaintenance.Services
{
    public class KFmaintenanceModulesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                // views
                Classes.FromThisAssembly()
                    .InSameNamespaceAs<StartupForm>()
                    .LifestyleTransient(),

                // services
                Component.For<KFmaintenanceApplicationContext>().LifestyleSingleton(),
                Classes.FromThisAssembly()
                    .InSameNamespaceAs<KFmaintenanceModulesInstaller>()
                    .WithServiceDefaultInterfaces()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton(),

                // services - core
                Classes.FromAssemblyNamed("KFlearning.Core")
                    .Pick()
                    .WithServiceDefaultInterfaces()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton()
            );
        }
    }
}