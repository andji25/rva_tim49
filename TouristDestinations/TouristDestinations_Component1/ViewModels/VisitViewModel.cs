using System;
using System.Collections.ObjectModel;
using System.Linq;
using TouristDestinations_Component1.Commands;
using TouristDestinations_Component1.Helpers;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;
using TouristDestinations_Component1.Services;

namespace TouristDestinations_Component1.ViewModels
{
    public class VisitViewModel : BindableBase
    {
        private IVisitRepository repository;
        private IDestinationRepository destinationRepository;

        private CommandManager commandManager;
        private VisitTracker visitTracker;

        public ObservableCollection<VisitDisplayModel> Visits { get; set; }
        public ObservableCollection<TouristDestination> Destinations { get; set; }

        private VisitDisplayModel selectedVisit;
        public VisitDisplayModel SelectedVisit
        {
            get => selectedVisit;
            set
            {
                SetProperty(ref selectedVisit, value);
                if (value != null)
                {
                    SelectedDestination = Destinations.FirstOrDefault(d => d.Id == value.DestinationId);
                    InputDateOfVisit = value.DateOfVisit;
                    InputNumberOfVisitors = value.NumberOfVisitors;
                    InputDurationOfVisit = value.DurationOfVisit;
                    InputRevenue = value.Revenue;
                }
            }
        }

        private TouristDestination selectedDestination;
        public TouristDestination SelectedDestination
        {
            get => selectedDestination;
            set => SetProperty(ref selectedDestination, value);
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                Search();
            }
        }

        private int inputNumberOfVisitors;
        public int InputNumberOfVisitors
        {
            get => inputNumberOfVisitors;
            set => SetProperty(ref inputNumberOfVisitors, value);
        }

        private int inputDurationOfVisit;
        public int InputDurationOfVisit
        {
            get => inputDurationOfVisit;
            set => SetProperty(ref inputDurationOfVisit, value);
        }

        private double inputRevenue;
        public double InputRevenue
        {
            get => inputRevenue;
            set => SetProperty(ref inputRevenue, value);
        }

        private DateTime inputDateOfVisit;
        public DateTime InputDateOfVisit
        {
            get => inputDateOfVisit;
            set => SetProperty(ref inputDateOfVisit, value);
        }

        private string statusMessage;
        public string StatusMessage
        {
            get => statusMessage;
            set => SetProperty(ref statusMessage, value);
        }

        public MyICommand AddCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand UndoCommand { get; set; }
        public MyICommand RedoCommand { get; set; }

        public VisitViewModel(IVisitRepository repository, IDestinationRepository destinationRepository, CommandManager commandManager, VisitTracker visitTracker)
        {
            this.repository = repository;
            this.destinationRepository = destinationRepository;
            this.commandManager = commandManager;
            this.visitTracker = visitTracker;

            Destinations = new ObservableCollection<TouristDestination>(destinationRepository.GetAll());
            Visits = CreateVisitDisplay();

            InputDateOfVisit = DateTime.Today;

            AddCommand = new MyICommand(OnAdd);
            EditCommand = new MyICommand(OnEdit);
            DeleteCommand = new MyICommand(OnDelete);
            UndoCommand = new MyICommand(OnUndo);
            RedoCommand = new MyICommand(OnRedo);
        }


        private void Search()
        {
            var all = repository.GetAll();
            var filtered = all.Where(v =>
            {
                var destination = Destinations.FirstOrDefault(d => d.Id == v.DestinationId);
                string destName = destination?.Name ?? "";
                return string.IsNullOrEmpty(SearchText) ||
                       destName.ToLower().Contains(SearchText.ToLower()) ||
                       v.StateType.ToString().ToLower().Contains(SearchText.ToLower()) ||
                       v.NumberOfVisitors.ToString().Contains(SearchText) ||
                       v.DurationOfVisit.ToString().Contains(SearchText) ||
                       v.Revenue.ToString().Contains(SearchText) ||
                       v.DateOfVisit.ToString("dd.MM.yyyy").Contains(SearchText);
            }).ToList();

            var result = new ObservableCollection<VisitDisplayModel>();
            foreach (var visit in filtered)
            {
                var destination = Destinations.FirstOrDefault(d => d.Id == visit.DestinationId);
                string name = destination?.Name ?? visit.DestinationId.ToString();
                result.Add(new VisitDisplayModel(visit, name));
            }

            Visits = result;
            OnPropertyChanged(nameof(Visits));
        }

        private bool Validate()
        {
            return SelectedDestination != null &&
                   InputNumberOfVisitors > 0 &&
                   InputDurationOfVisit > 0 &&
                   InputRevenue > 0 &&
                   InputDateOfVisit != default;
        }

        private void OnAdd()
        {
            if (!Validate()) return;
            var visit = new DestinationVisit(SelectedDestination.Id, InputDateOfVisit, InputNumberOfVisitors, InputDurationOfVisit, InputRevenue);
            commandManager.ExecuteCommand(new AddVisitCommand(visit, repository));
            visitTracker.AddVisit(visit);
            Visits = CreateVisitDisplay();
            OnPropertyChanged(nameof(Visits));
            ClearInputs();
        }
        private void OnEdit()
        {
            if (SelectedVisit == null || !Validate()) return;
            var oldVisit = SelectedVisit.Visit;
            var newVisit = new DestinationVisit(oldVisit.DestinationId, InputDateOfVisit, InputNumberOfVisitors, InputDurationOfVisit, InputRevenue);
            commandManager.ExecuteCommand(new EditVisitCommand(newVisit, oldVisit, repository));
            Visits = CreateVisitDisplay();
            OnPropertyChanged(nameof(Visits));
            ClearInputs();
        }

        private void OnDelete()
        {
            if (SelectedVisit == null) return;
            commandManager.ExecuteCommand(new DeleteVisitCommand(SelectedVisit.Visit, repository));
            Visits = CreateVisitDisplay();
            OnPropertyChanged(nameof(Visits));
            ClearInputs();
        }

        private void OnUndo()
        {
            commandManager.Undo();
            Visits = CreateVisitDisplay();
            OnPropertyChanged(nameof(Visits));
        }

        private void OnRedo()
        {
            commandManager.Redo();
            Visits = CreateVisitDisplay();
            OnPropertyChanged(nameof(Visits));
        }

        private void ClearInputs()
        {
            InputNumberOfVisitors = 0;
            InputDurationOfVisit = 0;
            InputRevenue = 0;
            InputDateOfVisit = DateTime.Today;
            SelectedDestination = null;
        }

        public void RefreshDestinations()
        {
            Destinations = new ObservableCollection<TouristDestination>(destinationRepository.GetAll());
            OnPropertyChanged(nameof(Destinations));
            Visits = CreateVisitDisplay();
            OnPropertyChanged(nameof(Visits));
        }

        public void RefreshVisits()
        {
            var currentSelected = SelectedVisit;
            Visits = CreateVisitDisplay();
            OnPropertyChanged(nameof(Visits));
            if (currentSelected != null)
            {
                SelectedVisit = Visits.FirstOrDefault(v =>
                    v.DestinationId == currentSelected.DestinationId &&
                    v.DateOfVisit == currentSelected.DateOfVisit);
            }
        }

        public string GetDestinationName(Guid destinationId)
        {
            var destination = Destinations.FirstOrDefault(d => d.Id == destinationId);
            return destination?.Name ?? destinationId.ToString();
        }

        private ObservableCollection<VisitDisplayModel> CreateVisitDisplay()
        {
            var result = new ObservableCollection<VisitDisplayModel>();
            foreach (var visit in repository.GetAll())
            {
                var destination = Destinations.FirstOrDefault(d => d.Id == visit.DestinationId);
                string name = destination?.Name ?? visit.DestinationId.ToString();
                result.Add(new VisitDisplayModel(visit, name));
            }
            return result;
        }
    }
}
