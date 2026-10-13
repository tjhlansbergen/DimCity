using YamlDotNet.Serialization;

internal static class Persistance
{
    const string FileName = ".dimcity.yaml";

    internal static string Store(DimCity city)
    {
        var yaml = new Serializer().Serialize(city);
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FileName);

        File.WriteAllText(filePath, yaml);
        return filePath;
    }

    internal static DimCity Load()
    {
        // Implementation for loading the DimCity object by name
        return new DimCity(); // Placeholder return
    }
}