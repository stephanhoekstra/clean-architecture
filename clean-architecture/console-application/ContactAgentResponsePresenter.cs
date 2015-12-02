using example.Usecases;

namespace console_application
{
    public class ContactAgentResponsePresenter
    {
        public ViewModel Handle(ContactAgentResponseMessage responseMessage)
        {
            switch(responseMessage.Status)
            {
                case ResponseStatus.Succes:
                    return new ViewModel(Texts.ThankYou);
                case ResponseStatus.ValidationFailed:
                    return new ViewModel(Texts.ValidationError);
            }
            return null;
        }
    }
}