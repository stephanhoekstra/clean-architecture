using console_application.Presentation;
using example.Gateways;
using example.Usecases;

namespace console_application
{
    class Program
    {
        static void Main()
        {
            var controller = 
                new AgentController(
                    new ContactAgentInteractor(
                        new ContactAgentRequestMessageValidator(), 
                        Factory.CreateDummyInMemoryHouseRepository()),
                    new ContactAgentResponsePresenter());

            controller.Contact(
                new ContactAgentRequestMessage
                {   
                    CustomerEmailAddress = "stephan@funda.nl",
                    CustomerPhoneNumber = 555123456,
                    HouseId = 45474845
                }
            );
        }
    }

    /// <remarks>
    /// In an MVC web application, this class would derive from a standard MVC Controller,
    /// This is a simplified console application, but I have tried to use the same names as MVC does, 
    /// to make it easier to map these concepts to the MVC world.
    /// </remarks>
    public class AgentController
    {
        private readonly ContactAgentInteractor _interactor;
        private readonly ContactAgentResponsePresenter _presenter;

        public AgentController(ContactAgentInteractor interactor, ContactAgentResponsePresenter presenter)
        {
            _interactor = interactor;
            _presenter = presenter;
        }

        /// <param name="requestMessage">
        /// In an MVC application, the requestMessage object would be constructed based on user input. 
        /// You could use a ModelBinder pattern to achieve this with a minimum ammount of code.
        /// </param>
        public void Contact(ContactAgentRequestMessage requestMessage)
        {
            var response = _interactor.Handle(requestMessage);

            var viewModel = _presenter.Handle(response);

            var view = new ConsoleView(viewModel);
            view.Render();
        }
    }
}
