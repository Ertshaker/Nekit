using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Nekit
{
    public class ConfigIO
    {
        private string PATH;
        public ConfigIO(string path)
        {
            PATH = path;
        }
        public BindingList<Config> LoadData()
        {
            if (!File.Exists(PATH))
            {
                File.CreateText(PATH);
                return new BindingList<Config>() { new Config()};
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                var json = JsonConvert.DeserializeObject<BindingList<Config>>(fileText);
                if (json != null && json.Count != 0)
                    return json;
                else
                    return new BindingList<Config>() { new Config() };
            }
        }

        public void SaveData(object ConfigList)
        {
            using (var writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(ConfigList);
                writer.Write(output);
            }
        }
    }
}