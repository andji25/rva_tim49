using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TouristDestinations_Component2.Adapter;
using TouristDestinations_Component2.Helpers;
using TouristDestinations_Component2.Interfaces;
using TouristDestinations_Component2.Models;
using TouristDestinations_Component2.Services;
using TouristDestinations_Component2.Strategies;
using TouristDestinations_Component2.WCF;

namespace TouristDestinations_Component2.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private StatisticsService statisticsService;
        private List<DestinationVisit> allVisits;

        public ObservableCollection<TouristDestination> Destinations { get; set; }

        private TouristDestination selectedDestination;
        public TouristDestination SelectedDestination
        {
            get => selectedDestination;
            set => SetProperty(ref selectedDestination, value);
        }

        private DateTime fromDate;
        public DateTime FromDate
        {
            get => fromDate;
            set => SetProperty(ref fromDate, value);
        }

        private DateTime toDate;
        public DateTime ToDate
        {
            get => toDate;
            set => SetProperty(ref toDate, value);
        }

        private string selectedStrategy;
        public string SelectedStrategy
        {
            get => selectedStrategy;
            set => SetProperty(ref selectedStrategy, value);
        }

        private string result;
        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        public ObservableCollection<string> Strategies { get; set; }

        public MyICommand CalculateCommand { get; set; }
        public MyICommand ExportCommand { get; set; }
        public MyICommand LoadFromServiceCommand { get; set; }
        public MainViewModel()
        {
            allVisits = new List<DestinationVisit>();
            Destinations = new ObservableCollection<TouristDestination>();

            FromDate = DateTime.Today.AddMonths(-1);
            ToDate = DateTime.Today;

            Strategies = new ObservableCollection<string>
            {
                "Lowest Visitors",
                "Total Revenue",
                "OffSeason Count"
            };

            IWriter writer = new CsvWriter("results.csv");
            statisticsService = new StatisticsService(new VisitAdapter(allVisits), writer);

            CalculateCommand = new MyICommand(OnCalculate);
            ExportCommand = new MyICommand(OnExport);
            LoadFromServiceCommand = new MyICommand(LoadFromService);
        }

        public void LoadData(List<DestinationVisit> visits, List<TouristDestination> destinations)
        {
            allVisits = visits;
            Destinations = new ObservableCollection<TouristDestination>(destinations);
            statisticsService = new StatisticsService(new VisitAdapter(allVisits), new CsvWriter("results.csv"));
            OnPropertyChanged(nameof(Destinations));
        }

        private List<DestinationVisit> FilterVisits()
        {
            return allVisits.Where(v =>
                (SelectedDestination == null || v.DestinationId == SelectedDestination.Id) &&
                v.DateOfVisit >= FromDate &&
                v.DateOfVisit <= ToDate
            ).ToList();
        }

        private void OnCalculate()
        {
            if (string.IsNullOrEmpty(SelectedStrategy)) return;

            var filtered = FilterVisits();
            var adapter = new VisitAdapter(filtered);
            statisticsService = new StatisticsService(adapter, new CsvWriter("results.csv"));

            switch (SelectedStrategy)
            {
                case "Lowest Visitors":
                    statisticsService.SetStrategy(new LowestVisitorsStrategy());
                    break;
                case "Total Revenue":
                    statisticsService.SetStrategy(new TotalRevenueStrategy());
                    break;
                case "OffSeason Count":
                    statisticsService.SetStrategy(new OffSeasonCountStrategy());
                    break;
            }

            Result = statisticsService.Calculate();
        }

        private void OnExport()
        {
            if (string.IsNullOrEmpty(Result)) return;
            statisticsService.WriteResults(Result);
        }

        public void LoadFromService()
        {
            VisitServiceClient client = new VisitServiceClient();
            var visits = client.GetVisits();
            var destinations = client.GetDestinations();
            LoadData(visits, destinations);
            client.Close();
        }
    }
}