using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace KatalogFilm.ViewModel.Helper
{
    public static class RWJson
    {
        public static void WriteToJson(string key, string value, string file)
        {
            JObject json = new JObject
            {
                { key, value }
            };
            string jsonString = JsonConvert.SerializeObject(json);
            File.WriteAllText(file, jsonString);
        }
        public static string ReadFromJSON(string key, string file)
        {
            string jsonString = File.ReadAllText(file);
            JObject json = JsonConvert.DeserializeObject<JObject>(jsonString);
            string result = json[key].Value<string>();
            return result;
        }
    }
}
