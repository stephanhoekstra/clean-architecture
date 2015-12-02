using System.Text;
using example.Usecases;

namespace console_application.Presentation
{
    /// <summary>
    /// The purpose of this class is to take the output of the system 
    /// (everything within the 'business rules' circles of the architecture), 
    /// and translate it to a viewModel that has everything for the end-user to 'look at', 
    /// to see that their request was processed. 
    /// </summary>
    public class ContactAgentResponsePresenter
    {
        public ContactAgentResponseViewModel Handle(ContactAgentResponseMessage responseMessage)
        {
            switch(responseMessage.ValidationResult.IsValid)
            {
                case true:
                    return new ContactAgentResponseViewModel(Texts.ThankYou);
                case false:
                    var sb = new StringBuilder();
                    sb.AppendLine(Texts.ValidationError);

                    foreach (var error in responseMessage.ValidationResult.Errors)
                    {
                        sb.AppendLine(error.ErrorMessage);
                    }
                    return new ContactAgentResponseViewModel(sb.ToString());
            }
            return null;
        }
    }
}