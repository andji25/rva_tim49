using System.Collections.Generic;
using System.ServiceModel;
using TouristDestinations_Component2.Models;

namespace TouristDestinations_Component2.WCF
{
    public class VisitServiceClient
    {
        private ChannelFactory<IVisitService> channelFactory;
        private IVisitService channel;

        public VisitServiceClient()
        {
            channelFactory = new ChannelFactory<IVisitService>(
                new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/VisitService"));
            channel = channelFactory.CreateChannel();
        }

        public List<DestinationVisit> GetVisits()
        {
            return channel.GetVisits();
        }

        public List<TouristDestination> GetDestinations()
        {
            return channel.GetDestinations();
        }

        public void Close()
        {
            channelFactory.Close();
        }
    }
}