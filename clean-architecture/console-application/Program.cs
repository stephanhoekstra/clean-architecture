using System;
using example.Usecases;

namespace console_application
{
    class Program
    {
        static void Main(string[] args)
        {
            var interactor = new ContactAgentInteractor();
            var responseMessage = 
                interactor.Handle(
                    new ContactAgentRequestMessage
                    {
                        //CustomerEmailAddress = "stephan@funda.nl",
                        CustomerPhoneNumber =  555123456,
                        ObjectId = 45474845 
                    });

            var presenter = new ContactAgentResponsePresenter();
            var viewModel = presenter.Handle(responseMessage);

            Console.WriteLine(viewModel.Text);

            Console.WriteLine(Texts.press_any_key_to_exit);
            Console.ReadLine();
        }
    }
}
