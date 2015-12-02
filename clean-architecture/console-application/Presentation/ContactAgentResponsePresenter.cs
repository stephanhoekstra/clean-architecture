using example.Usecases;

namespace console_application.Presentation
{
    public class ContactAgentResponsePresenter
    {
        public ContactAgentResponseViewModel Handle(ContactAgentResponseMessage responseMessage)
        {
            switch(responseMessage.Status)
            {
                case ResponseStatus.Succes:
                    return new ContactAgentResponseViewModel(Texts.ThankYou);
                case ResponseStatus.ValidationFailed:
                    return new ContactAgentResponseViewModel(Texts.ValidationError);
            }
            return null;
        }
    }
}