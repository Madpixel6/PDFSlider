using Autofac;
using Autofac.Core;
using PDFSlider.Services.Abstract;
using PDFSlider.ViewModels.Abstract;
using System;
using System.Reflection;

namespace PDFSlider
{
    public static class Bootstrap
    {
        private static IContainer Container { get; set; }
        private static ContainerBuilder builder;

        public static void Build()
        {
            if (Container != null)
                throw new InvalidOperationException(message: "Autofac container is already built!");
            builder = new ContainerBuilder();
            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            builder.RegisterAssemblyTypes(assemblies)
                  .Where(t => typeof(IService).IsAssignableFrom(t))
                  .SingleInstance()
                  .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(IViewModel).IsAssignableFrom(t))
                .AsImplementedInterfaces();

            Container = builder.Build();
        }
        public static T Resolve<T>()
        {
            if (Container is null)
            {
                throw new Exception("Bootstrap container is empty");
            }

            return Container.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (Container is null)
            {
                throw new Exception("Bootstrap container is empty");
            }

            return Container.Resolve<T>(parameters);
        }

        public static void Dispose()
        {
            Container.Dispose();
        }
    }
}
