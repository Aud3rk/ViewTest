using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Data
{
    
    public class DataSaver
    {
        public void SaveData(List<ItemData> list)
        {
            Data tasks = new Data(list);
            string json =  JsonSerializer.Serialize(tasks);
            File.WriteAllText("Data.json", json);
        }

        public Data LoadData()
        {
            string json = File.ReadAllText("Data.json");
            Data data = JsonSerializer.Deserialize<Data>(json);
            return data;
        }
    }
}