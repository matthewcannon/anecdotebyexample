using AnecdoteByExampleTwo.Application.Commands;

namespace AnecdoteByExampleTwo.Application.CommandHandlers
{
    public interface IHandleSendEmail
    {
        void Handle(SendEmail sendEmail);
    }
}