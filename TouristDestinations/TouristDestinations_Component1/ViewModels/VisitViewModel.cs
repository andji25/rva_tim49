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

        public ObservableCollection<DestinationVisit> Visits { get; set; }
        public ObservableCollection<TouristDestination> Destinations { get; set; }

        private DestinationVisit selectedVisit;
        public DestinationVisit SelectedVisit
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

            Visits = new ObservableCollection<DestinationVisit>(repository.GetAll());
            Destinations = new ObservableCollection<TouristDestination>(destinationRepository.GetAll());

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
                string.IsNullOrEmpty(SearchText) ||
                v.DestinationId.ToString().Contains(SearchText) ||
                v.StateType.ToString().ToLower().Contains(SearchText.ToLower())
            ).ToList();

            Visits = new ObservableCollection<DestinationVisit>(filtered);
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
            Visits.Add(visit);
            ClearInputs();
        }
        private void OnEdit()
        {
            if (SelectedVisit == null || !Validate()) return;
            var oldVisit = SelectedVisit;
            var newVisit = new DestinationVisit(oldVisit.DestinationId, InputDateOfVisit, InputNumberOfVisitors, InputDurationOfVisit, InputRevenue);
            commandManager.ExecuteCommand(new EditVisitCommand(newVisit, oldVisit, repository));

            int index = Visits.IndexOf(oldVisit);
            if (index >= 0)
                Visits[index] = newVisit;

            ClearInputs();
        }

        private void OnDelete()
        {
            if (SelectedVisit == null) return;
            commandManager.ExecuteCommand(new DeleteVisitCommand(SelectedVisit, repository));
            Visits.Remove(SelectedVisit);
            ClearInputs();
        }

        private void OnUndo()
        {
            commandManager.Undo();
            Visits = new ObservableCollection<DestinationVisit>(repository.GetAll());
            OnPropertyChanged(nameof(Visits));
        }

        private void OnRedo()
        {
            commandManager.Redo();
            Visits = new ObservableCollection<DestinationVisit>(repository.GetAll());
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
    }
}
