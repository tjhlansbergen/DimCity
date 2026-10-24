using YamlDotNet.Serialization;

internal static class Persistance
{
    internal static string Path(string name)
    {
        return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".dimcity", $"{name}.yaml");
    }

    internal static string Store(DimCity city)
    {
        try
        {
            var yaml = new Serializer().Serialize(city);
            var filePath = Path(city.Name);

            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException("Could not determine directory path."));
            File.WriteAllText(filePath, yaml);
            return filePath;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error storing city: {ex.Message}");
        }

    }

    internal static (DimCity, string) Load(string name)
    {
        var filePath = Path(name);

        try
        {
            if (!File.Exists(filePath))
            {
                Store(new DimCity(name));
            }

            var yaml = File.ReadAllText(filePath);
            return (new Deserializer().Deserialize<DimCity>(yaml), $"Loaded saved city from {filePath}");
        }
        catch (Exception ex)
        {
            return (new DimCity(name), $"Error loading saved city: {ex.Message}");
        }
    }
}