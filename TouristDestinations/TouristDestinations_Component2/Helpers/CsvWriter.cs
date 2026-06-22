using System.IO;
using TouristDestinations_Component2.Interfaces;

namespace TouristDestinations_Component2.Helpers
{
    public class CsvWriter : IWriter
    {
        string path;

        public CsvWriter(string path)
        {
            this.path = path;
        }

        public void Write(string result)
        {
            File.WriteAllText(path, result);
        }
    }
}
