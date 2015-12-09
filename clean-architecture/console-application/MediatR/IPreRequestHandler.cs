namespace console_application.MediatR
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}