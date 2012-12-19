namespace AnecdoteByExampleTwo.Application.Commands
{
    public class SendEmail
    {
        public Email Email { get; private set; }

        public SendEmail(Email email)
        {
            Email = email;
        }
    }
}
