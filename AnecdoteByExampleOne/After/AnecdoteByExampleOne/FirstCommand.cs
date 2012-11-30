namespace AnecdoteByExampleOne
{
    public class FirstCommand : Command
    {
        public Something Payload { get; private set; }

        public FirstCommand(Something payload)
        {
            Payload = payload;
        }
    }
}