using System;
using System.IO;
using console_application.MediatR;
using example.Entities;
using example.Usecases;
using FluentValidation;
using MediatR;
using StructureMap;

namespace console_application.Structuremap
{
    public static class DependencyInjection
    {
        public static IContainer Container { get; private set; }

        public static void Initialize()
        {
            Container = new Container(_ =>
            {
                _.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<House>();
                    scanner.AssemblyContainingType<IMediator>();
                    scanner.WithDefaultConventions();
                    scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                });

                var handlerType = _.For(typeof(IRequestHandler<,>));
                handlerType.DecorateAllWith(typeof(MediatorPipeline<,>));
                handlerType.DecorateAllWith(typeof(ValidatorHandler<,>));

                _.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                _.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
                _.For<TextWriter>().Use(Console.Out);

                _.For<IValidator<ContactAgentRequestMessage>>().Use<ContactAgentRequestMessageValidator>();
                _.For<IRepository<int, House>>().Use(Factory.CreateDummyInMemoryHouseRepository());
            });
        }
    }
}