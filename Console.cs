internal class Console
{
    private readonly Queue<string> buffer;

    internal Console()
    {
        buffer = new Queue<string>(100);
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

    internal string[] Read(int count)
    {
        return [.. buffer.Skip(buffer.Count - count).Take(count)];
    }
}