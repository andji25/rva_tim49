using System.Collections.Generic;
using System.Linq;
using TouristDestinations_Component2.Interfaces;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.Strategies
{
    public class TotalRevenueStrategy : IStatisticsStrategy
    {
        public string Calculate(Dictionary<string, List<DestinationVisit>> data)
        {
            string result = "Total Revenue per Destination:\n";
            foreach (var entry in data)
            {
                double total = entry.Value.Sum(v => v.Revenue);
                result += $"{entry.Key}: {total:C}\n";
            }
            return result;
        }
    }
}