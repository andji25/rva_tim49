using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Repositories
{
    public class JsonDestinationRepository : DestinationRepository
    {
        private string path;

        public JsonDestinationRepository(string path)
        {
            this.path = path;
            Load();
        }
        public override void Load()
        {
            if (!File.Exists(path))
                return;

            string json = File.ReadAllText(path);
            destinations = JsonConvert.DeserializeObject<List<TouristDestination>>(json);
        }

        public override void Save()
        {
            string json = JsonConvert.SerializeObject(destinations, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}
