using AnecdoteByExampleTwo.Application.Commands;

namespace AnecdoteByExampleTwo.Application.CommandHandlers
{
    public class NullHandleSendEmail : IHandleSendEmail
    {
        public void Handle(SendEmail sendEmail) { }
    }
}