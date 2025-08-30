using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace UniversityWebApp.Services
{
    public class FileSaverService
    {
        public async static Task<bool> SaveJson<T>(Dictionary<string, T> values, string path, string fileName)
        {
            try
            {
                var fullPath = Path.Join(path, fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                    //Open file, deserialize it, add new item, serialize it, save it
                    using (var file = File.Open(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        var stream = new Memory<byte>();
                        await file.ReadAsync(stream);
                        var result = Encoding.UTF8.GetString(stream.ToArray());
                        var dataList = JsonSerializer.Deserialize<JsonArray>(result);
                        if (dataList == null)
                            dataList = new JsonArray();
                        var json = new JsonObject();
                        foreach (KeyValuePair<string, T> kvp in values)
                        {
                            json.Add(kvp.Key, JsonValue.Create(kvp.Value));
                        }
                        dataList.Add(json);
                    }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
