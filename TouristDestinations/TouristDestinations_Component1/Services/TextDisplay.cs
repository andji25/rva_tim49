using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;
using TouristDestinations_Component1.ViewModels;

namespace TouristDestinations_Component1.Services
{
    public class TextDisplay : IObserver
    {
        private VisitViewModel visitViewModel;

        public TextDisplay(VisitViewModel visitViewModel)
        {
            this.visitViewModel = visitViewModel;
        }

        public void Update(DestinationVisit visit)
        {
            visitViewModel.StatusMessage = $"Latest visit: {visit.DestinationId}, State: {visit.StateType}";
        }
    }
}