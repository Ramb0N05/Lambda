using Newtonsoft.Json;

namespace Lambda.Configuration {

    public interface IConfiguration {

        public Task<bool> Save(string filePath, JsonSerializerSettings? jss = null);

        public Task<T> Get<T>(string filePath, JsonSerializerSettings? jss = null) where T : new();
    }
}
