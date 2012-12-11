namespace AnecdoteByExampleTwo.Domain
{
    public class EmailSent<T> : Event where T : Email {}
}