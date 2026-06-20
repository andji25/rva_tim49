using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Repositories
{
    public class XmlDestinationRepository : DestinationRepository
    {
        private string path;

        public XmlDestinationRepository(string path)
        {
            this.path = path;
            Load();
        }
        public override void Load()
        {
            if (!File.Exists(path))
                return;

            XmlSerializer serializer = new XmlSerializer(typeof(List<TouristDestination>));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                destinations = (List<TouristDestination>)serializer.Deserialize(stream);
            }
        }

        public override void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TouristDestination>));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, destinations);
            }
        }
    }
}
