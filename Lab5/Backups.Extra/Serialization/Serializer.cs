using Newtonsoft.Json;

namespace Backups.Extra.Serialization;

public class Serializer
{
    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented,
        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        NullValueHandling = NullValueHandling.Ignore,
    };

    public Serializer(string jsonPath)
    {
        JsonPath = jsonPath;
    }

    public string JsonPath { get; }

    public string GetSerializedObject(BackupTaskExtra taskExtra) => JsonConvert.SerializeObject(taskExtra, _settings);

    public void OpenWriteJson(string jsonData)
    {
        File.WriteAllText(JsonPath, jsonData);
    }

    public string OpenReadJson()
    {
        return File.ReadAllText(JsonPath);
    }

    public void Serialize(BackupTaskExtra taskExtra)
    {
        string serializedObject = GetSerializedObject(taskExtra);
        OpenWriteJson(serializedObject);
    }

    public BackupTaskExtra Deserialize()
    {
        string jsonData = OpenReadJson();
        return JsonConvert.DeserializeObject<BackupTaskExtra>(jsonData, _settings);
    }
}