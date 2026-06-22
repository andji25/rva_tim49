using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using TouristDestinations_Component1.Helpers;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;
using TouristDestinations_Component1.Repositories;
using TouristDestinations_Component1.Services;
using TouristDestinations_Component1.WCF;

namespace TouristDestinations_Component1.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private ServiceHost serviceHost;
        public DestinationViewModel DestinationViewModel { get; set; }
        public VisitViewModel VisitViewModel { get; set; }

        private IDestinationRepository destinationRepository;
        private IVisitRepository visitRepository;

        private CommandManager commandManager;
        private VisitTracker visitTracker;

        private string selectedFormat;
        public string SelectedFormat
        {
            get => selectedFormat;
            set => SetProperty(ref selectedFormat, value);
        }

        public ChartViewModel ChartViewModel { get; set; }

        public MyICommand LoadCommand { get; set; }
        public MyICommand SaveCommand { get; set; }
        public MyICommand<string> SelectFormatCommand { get; set; }

        public MainViewModel()
        {
            ILogger logger = new FileLogger("log.txt");
            commandManager = new CommandManager(logger);
            SelectedFormat = "XML";

            LoadCommand = new MyICommand(Load);
            SaveCommand = new MyICommand(Save);
            SelectFormatCommand = new MyICommand<string>(OnSelectFormat);
        }

        private void OnSelectFormat(string format)
        {
            SelectedFormat = format;
        }

        public void Save()
        {
            destinationRepository?.Save();
            visitRepository?.Save();
        }

        public void Load()
        {
            if (SelectedFormat == "XML")
            {
                destinationRepository = new XmlDestinationRepository("destinations.xml");
                visitRepository = new XmlVisitRepository("visits.xml");
            }
            else
            {
                destinationRepository = new JsonDestinationRepository("destinations.json");
                visitRepository = new JsonVisitRepository("visits.json");
            }

            visitTracker = new VisitTracker(visitRepository);
            SeedIfEmpty();

            DestinationViewModel = new DestinationViewModel(destinationRepository, commandManager);
            VisitViewModel = new VisitViewModel(visitRepository, destinationRepository, commandManager, visitTracker);
            ChartViewModel = new ChartViewModel();

            IObserver chartDisplay = new ChartDisplay(visitRepository, ChartViewModel);
            IObserver textDisplay = new TextDisplay(VisitViewModel);
            visitTracker.Register(chartDisplay);
            visitTracker.Register(textDisplay);
            
            OnPropertyChanged(nameof(DestinationViewModel));
            OnPropertyChanged(nameof(VisitViewModel));
            OnPropertyChanged(nameof(ChartViewModel));

            StartWcfService();
        }

        private void SeedIfEmpty()
        {
            if (destinationRepository.GetAll().Count == 0)
            {
                destinationRepository.Add(new TouristDestination("Zlatibor", "Serbia", "Mountain"));
                destinationRepository.Add(new TouristDestination("Dubrovnik", "Croatia", "Coastal"));
                destinationRepository.Add(new TouristDestination("Santorini", "Greece", "Island"));
            }

            if (visitRepository.GetAll().Count == 0)
            {
                var destinations = destinationRepository.GetAll();
                visitRepository.Add(new DestinationVisit(destinations[0].Id, System.DateTime.Now, 150, 3, 2500.0));
                visitRepository.Add(new DestinationVisit(destinations[1].Id, System.DateTime.Now.AddDays(-10), 45, 2, 800.0));
                visitRepository.Add(new DestinationVisit(destinations[2].Id, System.DateTime.Now.AddDays(-20), 80, 5, 1500.0));
            }
        }

        private void StartWcfService()
        {
            serviceHost = new ServiceHost(new VisitService(visitRepository, destinationRepository),
                                          new Uri("http://localhost:8080/VisitService"));
            serviceHost.AddServiceEndpoint(typeof(IVisitService),
                                           new BasicHttpBinding(),
                                           "http://localhost:8080/VisitService");
            serviceHost.Open();
        }

        public void StopWcfService()
        {
            serviceHost?.Close();
        }
    }
}
