internal class Console
{
    private readonly Queue<string> buffer;

    internal Console(int bufferSize)
    {
        buffer = new Queue<string>(bufferSize);
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

    internal string[] Read()
    {
        return [.. buffer];
    }
}