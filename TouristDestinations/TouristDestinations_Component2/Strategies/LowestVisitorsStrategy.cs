using System.Collections.Generic;
using System.Linq;
using TouristDestinations_Component2.Interfaces;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.Strategies
{
    public class LowestVisitorsStrategy : IStatisticsStrategy
    {
        public string Calculate(Dictionary<string, List<DestinationVisit>> data)
        {
            string result = "Lowest Visitors and Average Duration per Destination:\n";
            foreach (var entry in data)
            {
                int lowest = entry.Value.Min(v => v.NumberOfVisitors);
                double avgDuration = entry.Value.Average(v => v.DurationOfVisit);
                result += $"{entry.Key}: Lowest visitors: {lowest}, Average duration: {avgDuration:F1} days\n";
            }
            return result;
        }
    }
}