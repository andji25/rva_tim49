using System.Collections.ObjectModel;
using System.Linq;
using TouristDestinations_Component1.Commands;
using TouristDestinations_Component1.Helpers;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;
using TouristDestinations_Component1.Services;

namespace TouristDestinations_Component1.ViewModels
{
    public class DestinationViewModel : BindableBase
    {
        private IDestinationRepository repository;
        private CommandManager commandManager;

        public ObservableCollection<TouristDestination> Destinations { get; set; }

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

        private string inputName;
        public string InputName
        {
            get => inputName;
            set => SetProperty(ref inputName, value);
        }

        private string inputCountry;
        public string InputCountry
        {
            get => inputCountry;
            set => SetProperty(ref inputCountry, value);
        }

        private string inputType;
        public string InputType
        {
            get => inputType;
            set => SetProperty(ref inputType, value);
        }

        public MyICommand AddCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand UndoCommand { get; set; }
        public MyICommand RedoCommand { get; set; }

        public DestinationViewModel(IDestinationRepository repository, CommandManager commandManager)
        {
            this.repository = repository;
            this.commandManager = commandManager;

            Destinations = new ObservableCollection<TouristDestination>(repository.GetAll());

            AddCommand = new MyICommand(OnAdd);
            EditCommand = new MyICommand(OnEdit);
            DeleteCommand = new MyICommand(OnDelete);
            UndoCommand = new MyICommand(OnUndo);
            RedoCommand = new MyICommand(OnRedo);
        }

        public void Refresh(IDestinationRepository repository)
        {
            this.repository = repository;
            Destinations = new ObservableCollection<TouristDestination>(repository.GetAll());
            OnPropertyChanged(nameof(Destinations));
        }

        private void Search()
        {
            var all = repository.GetAll();
            var filtered = all.Where(d =>
                string.IsNullOrEmpty(SearchText) ||
                d.Name.ToLower().Contains(SearchText.ToLower()) ||
                d.Country.ToLower().Contains(SearchText.ToLower()) ||
                d.Type.ToLower().Contains(SearchText.ToLower())
            ).ToList();

            Destinations = new ObservableCollection<TouristDestination>(filtered);
            OnPropertyChanged(nameof(Destinations));
        }

        private bool Validate()
        {
            return !string.IsNullOrWhiteSpace(InputName) &&
                   !string.IsNullOrWhiteSpace(InputCountry) &&
                   !string.IsNullOrWhiteSpace(InputType);
        }

        private void OnAdd()
        {
            if (!Validate()) return;
            var destination = new TouristDestination(InputName, InputCountry, InputType);
            commandManager.ExecuteCommand(new AddDestinationCommand(destination, repository));
            Destinations.Add(destination);
            ClearInputs();
        }

        private void OnEdit()
        {
            if (SelectedDestination == null || !Validate()) return;
            var oldDestination = SelectedDestination;
            var newDestination = new TouristDestination(InputName, InputCountry, InputType);
            commandManager.ExecuteCommand(new EditDestinationCommand(newDestination, oldDestination, repository));
            Refresh(repository);
            ClearInputs();
        }

        private void OnDelete()
        {
            if (SelectedDestination == null) return;
            commandManager.ExecuteCommand(new DeleteDestinationCommand(SelectedDestination, repository));
            Destinations.Remove(SelectedDestination);
            ClearInputs();
        }

        private void OnUndo()
        {
            commandManager.Undo();
            Refresh(repository);
        }

        private void OnRedo()
        {
            commandManager.Redo();
            Refresh(repository);
        }

        private void ClearInputs()
        {
            InputName = string.Empty;
            InputCountry = string.Empty;
            InputType = string.Empty;
        }
    }
}
