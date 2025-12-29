namespace app.RepositoryClasses;

using System.Text.Json;
public class DataSaverComponent<T>
{
    
    private readonly string _filepath;
    private IEnumerable<T>? _cachedData = null;
    public IEnumerable<T> Data { 
        get {
            _cachedData ??= ReadJson();
            return _cachedData ?? [];
        }
    }
    public DataSaverComponent(string dbDirectory): this(dbDirectory, $"{typeof(T).Name}s") {}
    public DataSaverComponent(string dbDirectory, string filename)
    {
        _filepath = Path.Join(dbDirectory, $"{filename}.json");
        if (!File.Exists(_filepath))
        {
            var file = new FileInfo(_filepath);
            file.Directory!.Create();
            using FileStream jsonFile = File.Create(_filepath);
            JsonSerializer.Serialize<IEnumerable<T>>(jsonFile, []);
        }
    }

    private IEnumerable<T>? ReadJson()
    {
        using FileStream jsonFile = File.Open(_filepath, FileMode.Open, FileAccess.Read);
        var res = JsonSerializer.Deserialize<IEnumerable<T>>(jsonFile);
        // I don't know when Deserialize returns null
        if (res is null) Console.WriteLine($"unable to read data from {_filepath}");
        return res;
    }

    public void Save(IEnumerable<T> records)
    {
        using FileStream jsonFile = File.Create(_filepath);
        JsonSerializer.Serialize(jsonFile, records);
    }

}