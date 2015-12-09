using console_application.Presentation;
using example.Usecases;
using MediatR;

namespace console_application
{
    /// <remarks>
    /// In an MVC web application, this class would derive from a standard MVC Controller,
    /// This is a simplified console application, but I have tried to use the same names as MVC does, 
    /// to make it easier to map these concepts to the MVC world.
    ///
    /// mediator is a framework that makes it easy to pass messages around and do cross cutting concerns,
    /// see https://lostechies.com/jimmybogard/2014/09/09/tackling-cross-cutting-concerns-with-a-mediator-pipeline/
    /// </remarks>
    public class AgentController
    {
        private readonly ContactAgentResponsePresenter _presenter;

        private readonly IMediator _mediator;

        public AgentController(IMediator mediator, 
            ContactAgentResponsePresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <param name="requestMessage">
        /// In an MVC application, the requestMessage object would be constructed based on user input. 
        /// You could use a ModelBinder pattern to achieve this with a minimum ammount of code.
        /// </param>
        public void Contact(ContactAgentRequestMessage requestMessage)
        {
            var response = _mediator.Send(requestMessage);

            var viewModel = _presenter.Handle(response);//this could also be done with mediator

            var view = new ConsoleView(viewModel);
            view.Render();
        }
    }
}