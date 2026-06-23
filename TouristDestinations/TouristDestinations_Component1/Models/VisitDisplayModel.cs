using System;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Models
{
    public class VisitDisplayModel
    {
        public DestinationVisit Visit { get; set; }
        public string DestinationName { get; set; }

        public Guid DestinationId => Visit.DestinationId;
        public DateTime DateOfVisit => Visit.DateOfVisit;
        public int NumberOfVisitors => Visit.NumberOfVisitors;
        public int DurationOfVisit => Visit.DurationOfVisit;
        public double Revenue => Visit.Revenue;
        public VisitStateType StateType => Visit.StateType;

        public VisitDisplayModel(DestinationVisit visit, string destinationName)
        {
            Visit = visit;
            DestinationName = destinationName;
        }
    }
}