internal class Console
{
    private readonly Queue<string> buffer = new(10);

    internal void Write(string text)
    {
        buffer.Enqueue(text);
        if (buffer.Count > 10)
        {
            buffer.Dequeue();
        }
    }

    internal string[] Read()
    {
        return [.. buffer];
    }
}