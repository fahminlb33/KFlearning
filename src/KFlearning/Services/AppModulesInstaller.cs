// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : AppModulesInstaller.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFlearning.Views;

namespace KFlearning.Services
{
    public class AppModulesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                // views
                Classes.FromThisAssembly()
                    .InSameNamespaceAs<StartupForm>()
                    .LifestyleTransient(),

                // services
                Component.For<CustomApplicationContext>().LifestyleSingleton(),
                Classes.FromThisAssembly()
                    .InSameNamespaceAs<AppModulesInstaller>()
                    .WithServiceDefaultInterfaces()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton(),

                // services - template
                Classes.FromThisAssembly()
                    .BasedOn<ITemplateProvider>()
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