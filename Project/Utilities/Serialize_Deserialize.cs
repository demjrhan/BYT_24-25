using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Project.Entities;
using Project.Models;

namespace Project.Utilities
{
    public static class SerializeDeserialize
    {
        private static readonly string DirectoryPath = Path.Combine("db", "products");
        private static readonly string FileName = "database.json";

        private static readonly JsonSerializerOptions SerializeOptions = new() { WriteIndented = true };
        private static readonly JsonSerializerOptions Options = new() { 
            Converters = { new CustomConverter() }, 
            PropertyNameCaseInsensitive = true };

        private static readonly List<string> sections = ["People", "Products", "Other"];
        private static readonly List<Type> typesToSkip = [
            typeof(Cart), typeof(Root), typeof(CustomConverter)];

        private class Root
        {
            public Dictionary<string, List<object>> People { get; set; } = [];
            public Dictionary<string, List<object>> Products { get; set; } = [];
            public Dictionary<string, List<object>> Other { get; set; } = [];
        }

        private class CustomConverter : JsonConverter<Root>
        {
            public override Root? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                Root root  = new();
                var typeMap = GetTypeMap();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName &&
                        reader.GetString() != null)
                    {
                        var section = reader.GetString();
                        reader.Read();
                        if (sections.Any(s => s.Equals(section)) && reader.TokenType == JsonTokenType.StartObject)
                        {
                            switch (section)
                            {
                                case "People":
                                    DeserializeGroup(ref reader, typeMap, root.People, options);
                                    break;
                                case "Products":
                                    ResetInstances<Product>(typeof(Product));
                                    DeserializeGroup(ref reader, typeMap, root.Products, options);
                                    break;
                                case "Other":
                                    DeserializeGroup(ref reader, typeMap, root.Other, options);
                                    break;
                            }
                        }
                    }
                }
                return root;
            }

            public override void Write(Utf8JsonWriter writer, Root value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
            private static void DeserializeGroup(ref Utf8JsonReader reader, Dictionary<string, Type> typeMap, Dictionary<string, List<object>> group, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    reader.Read();

                    while (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string? className = reader.GetString();
                        reader.Read();

                        if (className != null && typeMap.TryGetValue(className, out Type? type))
                        {
                            MethodInfo? resetInstances = typeof(SerializeDeserialize)
                                .GetMethod("ResetInstances")
                                ?.MakeGenericMethod(type);

                            resetInstances?.Invoke(null, [type]);

                            var listType = typeof(List<>).MakeGenericType(type);
                            JsonSerializer.Deserialize(ref reader, listType, options);
                        }
                        else
                        {
                            reader.Skip();
                        }
                        reader.Read();
                    }
                }
            }

            private static Dictionary<string, Type> GetTypeMap()
            {
                return Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && !t.Name.Contains('<'))
                    .ToDictionary(t => t.Name, t => t);
            }
        }

        public static void SerializeToFile(string? testFileName = null)
        {
            var root = new Root();

            ParseInstances<Person>(root.People);
            ParseInstances<Product>(root.Products);
            ParseInstances(root.Other);

            try
            {
                string filePath = Path.Combine(DirectoryPath, FileName);
                if (testFileName != null)
                    filePath = testFileName;
                Directory.CreateDirectory(DirectoryPath);
                string jsonString = JsonSerializer.Serialize(root, SerializeOptions);

                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"Data successfully serialized to {FileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization error: {ex.Message}");
            }
        }
        public static void DeserializeFromFile(string? testFileName = null)
        {
            try
            {
                string filePath = Path.Combine(DirectoryPath, FileName);
                if (testFileName != null)
                    filePath = testFileName;
                string jsonString = File.ReadAllText(filePath);

                Inventory.ResetInventory();

                Root? root = JsonSerializer.Deserialize<Root>(jsonString, Options);
                Console.WriteLine($"Data successfully deserialized from {FileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
            }
        }

        private static void ParseInstances<T>(Dictionary<string, List<object>> group)
        {
            var subclasses = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T)));


            foreach (var subclass in subclasses)
            {
                Console.WriteLine(subclass);
                var instancesProperty = subclass.GetField("Instances", BindingFlags.Static | BindingFlags.NonPublic);
                if (instancesProperty != null)
                {
                    var instances = (IEnumerable<object>?)instancesProperty.GetValue(null);
                    if (instances != null)
                    {
                        Console.WriteLine(instances);
                        Console.WriteLine(instances.ToList());
                        group[subclass.Name] = instances.ToList();
                        Console.WriteLine(group[subclass.Name]);
                    }
                }
            }
        }

        private static void ParseInstances(Dictionary<string, List<object>> group)
        {
            var classes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                            !t.IsSubclassOf(typeof(Person)) &&
                            !t.IsSubclassOf(typeof(Product)) &&
                            !t.IsSealed);

            foreach (var cls in classes)
            {
                if (cls == typeof(Root) || cls == typeof(Program) || cls == typeof(Controller.Controller))
                    continue;

                var instancesProperty = cls.GetField("Instances", BindingFlags.Static | BindingFlags.NonPublic);
                if (instancesProperty == null) continue;

                var instances = (IEnumerable<object>?)instancesProperty.GetValue(null);
                if (instances != null)
                    group[cls.Name] = instances.ToList();
            }
        }

        public static void ResetInstances<T>(Type type)
        {
            FieldInfo? lastId = type.GetField("_lastId", BindingFlags.Static | BindingFlags.NonPublic);
            lastId?.SetValue(null, 0);

            FieldInfo? instancesField = type.GetField("Instances", BindingFlags.Static | BindingFlags.NonPublic);
            instancesField?.SetValue(null, new List<T>());
        }
    }
}
