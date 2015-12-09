using console_application.Structuremap;
using example.Gateways;
using example.Usecases;

namespace console_application
{
    class Program
    {
        static void Main()
        {
            DependencyInjection.Initialize();
            var controller = DependencyInjection.Container.GetInstance<AgentController>();
            
            controller.Contact(
                new ContactAgentRequestMessage
                {   
                    CustomerEmailAddress = "stephan@funda.nl", //comment to demonstrate validation
                    CustomerPhoneNumber = 555123456,
                    HouseId = 45474845
                }
            );
        }
    }
}
