using Newtonsoft.Json;

namespace Lambda.Configuration {
    abstract public class Configuration : IConfiguration {
        public virtual async Task<bool> Save(string filePath, JsonSerializerSettings? jss = null) {
            try {
                string json = JsonConvert.SerializeObject(this, jss);
                await File.WriteAllTextAsync(filePath, json);

                return true;
            } catch {
                return false;
            }
        }

        public virtual async Task<T> Get<T>(string filePath, JsonSerializerSettings? jss = null) where T : new() {
            try {
                string generalJson = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<T>(generalJson, jss) ?? new();
            } catch {
                return new T();
            }
        }
    }
}
