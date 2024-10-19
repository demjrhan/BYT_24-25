using System.Text.Json;
namespace Project;

public class SerializeDeserialize
{
    public static void SerializeToFile<T>(List<T> objects, string fileName)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(objects);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Data successfully serialized to {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Serialization error: {ex.Message}");
        }
    }
    public static List<T> DeserializeFromFile<T>(string fileName)
    {
        try
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deserialization error: {ex.Message}");
            return new List<T>();
        }
    }
}