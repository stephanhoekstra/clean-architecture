using System;

namespace console_application.Presentation
{
    /// <remarks>
    /// For the purposes of this demonstration, I'm just outputting the viewmodel to the console,
    /// in an MVC app, this would probably represent a razor view;
    /// </remarks>
    public class ConsoleView
    {
        private readonly ContactAgentResponseViewModel _viewModel;

        public ConsoleView(ContactAgentResponseViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Render()
        {
            Console.WriteLine(_viewModel.Text);
            Console.WriteLine(Texts.press_any_key_to_exit);
            Console.ReadLine();
        }
    }
}