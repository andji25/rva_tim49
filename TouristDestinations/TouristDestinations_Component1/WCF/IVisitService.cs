using System.Collections.Generic;
using System.ServiceModel;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.WCF
{
    [ServiceContract]
    public interface IVisitService
    {
        [OperationContract]
        List<DestinationVisit> GetVisits();

        [OperationContract]
        List<TouristDestination> GetDestinations();
    }
}