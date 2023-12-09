using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.Configuration {
    public interface IConfiguration {
        public Task<bool> Save(string filePath, JsonSerializerSettings? jss = null);
        public Task<T> Get<T>(string filePath, JsonSerializerSettings? jss = null) where T : new();
    }
}
