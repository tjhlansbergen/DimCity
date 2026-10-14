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

    internal static (DimCity, string) Load()
    {
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FileName);

        try
        {
            var yaml = File.ReadAllText(filePath);
            return (new Deserializer().Deserialize<DimCity>(yaml), $"Loaded saved city from {filePath}");
        }
        catch (Exception ex)
        {
            return (new DimCity(), $"Error loading saved city: {ex.Message}");
        }
    }
}