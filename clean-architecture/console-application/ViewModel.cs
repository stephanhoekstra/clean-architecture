namespace console_application
{
    public class ViewModel
    {
        public string Text { get; private set; }

        public ViewModel(string text)
        {
            Text = text;
        }
    }
}