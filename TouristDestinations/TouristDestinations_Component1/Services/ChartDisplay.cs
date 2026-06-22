using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;
using TouristDestinations_Component1.ViewModels;

namespace TouristDestinations_Component1.Services
{
    public class ChartDisplay : IObserver
    {
        private IVisitRepository repository;
        private ChartViewModel chartViewModel;

        public ChartDisplay(IVisitRepository repository, ChartViewModel chartViewModel)
        {
            this.repository = repository;
            this.chartViewModel = chartViewModel;
        }

        public void Update(DestinationVisit visit)
        {
            chartViewModel.UpdateChart(repository.GetAll());
        }
    }
}