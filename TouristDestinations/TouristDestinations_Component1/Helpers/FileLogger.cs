using System;
using System.IO;
using TouristDestinations_Component1.Interfaces;

namespace TouristDestinations_Component1.Helpers
{
	public class FileLogger : ILogger
	{
		private string path;

        public FileLogger(string path)
        {
            this.path = path;
        }

        public void Log(string message)
        {
            string logEntry = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";
            File.AppendAllText(path, logEntry + Environment.NewLine);
        }
    }
}
