using System.Collections.Generic;
using System.ServiceModel;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class VisitService : IVisitService
    {
        private IVisitRepository visitRepository;
        private IDestinationRepository destinationRepository;

        public VisitService(IVisitRepository visitRepository, IDestinationRepository destinationRepository)
        {
            this.visitRepository = visitRepository;
            this.destinationRepository = destinationRepository;
        }

        public List<DestinationVisit> GetVisits()
        {
            return visitRepository.GetAll();
        }

        public List<TouristDestination> GetDestinations()
        {
            return destinationRepository.GetAll();
        }
    }
}