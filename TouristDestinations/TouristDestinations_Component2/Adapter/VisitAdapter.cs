using System;
using System.Collections.Generic;
using System.Linq;
using TouristDestinations_Component2.Interfaces;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.Adapter
{
    public class VisitAdapter : IVisitData
    {
        private List<DestinationVisit> visits;
        private List<TouristDestination> destinations;
        private DateTime from;
        private DateTime to;

        public VisitAdapter(List<DestinationVisit> visits, List<TouristDestination> destinations, DateTime from, DateTime to)
        {
            this.visits = visits;
            this.destinations = destinations;
            this.from = from;
            this.to = to;
        }

        public Dictionary<string, List<DestinationVisit>> GetData()
        {
            Dictionary<string, List<DestinationVisit>> result = new Dictionary<string, List<DestinationVisit>>();

            foreach (DestinationVisit visit in visits)
            {
                var dest = destinations.FirstOrDefault(d => d.Id == visit.DestinationId);
                string destName = dest?.Name ?? visit.DestinationId.ToString();
                string key = $"{destName}-{from:yyyy-MM-dd}-{to:yyyy-MM-dd}";
                if (!result.ContainsKey(key))
                    result[key] = new List<DestinationVisit>();
                result[key].Add(visit);
            }

            return result;
        }
    }
}