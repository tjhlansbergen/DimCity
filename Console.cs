internal class Console
{
    private readonly Queue<string> buffer;

    internal Console()
    {
        buffer = new Queue<string>(1000);
    }

    internal void Write(string text, bool debug = false)
    {
        if (debug)
        {
            text = $"{DateTime.Now:HH:mm:ss} - {text}";
        }

        buffer.Enqueue(text);
        if (buffer.Count >= buffer.Capacity)
        {
            buffer.Dequeue();
        }
    }

    internal string[] Read(int count, int offset = 0)
    {
        var start = buffer.Count - count - offset;
        if (start < 0) start = 0;
        return [.. buffer.Skip(start).Take(count)];
    }

    internal int Count => buffer.Count;
}