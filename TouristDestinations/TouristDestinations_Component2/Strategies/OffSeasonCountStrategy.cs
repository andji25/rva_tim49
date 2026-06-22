using System.Collections.Generic;
using System.Linq;
using TouristDestinations_Component2.Interfaces;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.Strategies
{
    public class OffSeasonCountStrategy : IStatisticsStrategy
    {
        public string Calculate(Dictionary<string, List<DestinationVisit>> data)
        {
            string result = "OffSeason Visits per Destination:\n";
            foreach (var entry in data)
            {
                int count = entry.Value.Count(v => v.StateType == VisitStateType.OffSeason);
                result += $"{entry.Key}: {count} offseason visits\n";
            }
            return result;
        }
    }
}