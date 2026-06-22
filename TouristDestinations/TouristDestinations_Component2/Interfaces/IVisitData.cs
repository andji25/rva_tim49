using System.Collections.Generic;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.Interfaces
{
    public interface IVisitData
    {
        Dictionary<string, List<DestinationVisit>> GetData();
    }
}
