namespace AnecdoteByExampleTwo.Application
{
    public class EmailSent<T> : Event where T : Email {}
}