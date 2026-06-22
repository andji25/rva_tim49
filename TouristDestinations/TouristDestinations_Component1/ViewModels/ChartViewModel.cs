using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Linq;
using TouristDestinations_Component1.Helpers;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.ViewModels
{
    public class ChartViewModel : BindableBase
    {
        private ObservableCollection<ISeries> series;
        public ObservableCollection<ISeries> Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }

        public ChartViewModel()
        {
            series = new ObservableCollection<ISeries>();
        }

        public void UpdateChart(List<DestinationVisit> visits)
        {
            int popular = visits.Count(v => v.StateType == VisitStateType.Popular);
            int stable = visits.Count(v => v.StateType == VisitStateType.Stable);
            int decline = visits.Count(v => v.StateType == VisitStateType.Decline);
            int offSeason = visits.Count(v => v.StateType == VisitStateType.OffSeason);

            Series = new ObservableCollection<ISeries>
            {
                new PieSeries<int> { Values = new[] { popular }, Name = "Popular" },
                new PieSeries<int> { Values = new[] { stable }, Name = "Stable" },
                new PieSeries<int> { Values = new[] { decline }, Name = "Decline" },
                new PieSeries<int> { Values = new[] { offSeason }, Name = "OffSeason" }
            };
        }
    }
}