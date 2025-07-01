using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data
{
    [Serializable]
    public class Data
    {
        public List<ItemData> List { get; set; }
        [JsonConstructor]
        public Data(List<ItemData> list) => 
            List = list;

    }
}