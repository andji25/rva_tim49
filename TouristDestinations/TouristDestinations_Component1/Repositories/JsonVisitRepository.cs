using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Repositories
{
    public class JsonVisitRepository : VisitRepository
    {
        private string path;

        public JsonVisitRepository(string path)
        {
            this.path = path;
            Load();
        }
        public override void Load()
        {
            if (!File.Exists(path))
                return;

            string json = File.ReadAllText(path);
            visits = JsonConvert.DeserializeObject<List<DestinationVisit>>(json);
        }

        public override void Save()
        {
            string json = JsonConvert.SerializeObject(visits, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}
