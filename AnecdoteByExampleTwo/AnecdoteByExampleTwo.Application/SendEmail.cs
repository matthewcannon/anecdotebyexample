namespace AnecdoteByExampleTwo.Application
{
    public class SendEmail<T> where T : Email
    {
        public T Email { get; private set; }

        public SendEmail(T email)
        {
            Email = email;
        }
    }
}
