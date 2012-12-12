namespace AnecdoteByExampleTwo.Application
{
    public class Email
    {
        public string From { get; private set; }
        public string To { get; private set; }

        public Email(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}