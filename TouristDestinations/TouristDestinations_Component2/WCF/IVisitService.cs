using System.Collections.Generic;
using System.ServiceModel;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.WCF
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