using System.Collections.Generic;
using TouristDestinations_Component2.Interfaces;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.Adapter
{
    public class VisitAdapter : IVisitData
    {
        private List<DestinationVisit> visits;

        public VisitAdapter(List<DestinationVisit> visits)
        {
            this.visits = visits;
        }

        public Dictionary<string, List<DestinationVisit>> GetData()
        {
            Dictionary<string, List<DestinationVisit>> result = new Dictionary<string, List<DestinationVisit>>();

            foreach (DestinationVisit visit in visits)
            {
                string key = visit.DestinationId.ToString();
                if (!result.ContainsKey(key))
                    result[key] = new List<DestinationVisit>();
                result[key].Add(visit);
            }

            return result;
        }
    }
}