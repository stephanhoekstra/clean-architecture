using example.Entities;
using example.Usecases;
using FluentValidation;
using StructureMap;
using StructureMap.Graph;

namespace console_application
{
    public static class DependencyInjection
    {
        public static IContainer Container { get; private set; }

        public static void Initialize()
        {
            Container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                });

                _.For<IValidator<ContactAgentRequestMessage>>().Use<ContactAgentRequestMessageValidator>();
                _.For<IRepository<int, House>>().Use(Factory.);
            });
        }
    }
}