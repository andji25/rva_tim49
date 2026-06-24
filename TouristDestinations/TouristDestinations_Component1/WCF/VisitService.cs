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
            var data = visitRepository.GetAll();

            System.Windows.MessageBox.Show(
                $"SERVER GetVisits -> {data.Count}");

            return data;
        }

        public List<TouristDestination> GetDestinations()
        {
            var data = destinationRepository.GetAll();

            System.Windows.MessageBox.Show(
                $"SERVER GetDestinations -> {data.Count}");

            return data;
        }
    }
}