using System.Collections.ObjectModel;
using System.Windows.Input;
using SzafkiSzkolne.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using SzafkiSzkolne.Views;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;

namespace SzafkiSzkolne.ViewModels
{
    public class ManageAllLockersViewModel : INotifyPropertyChanged
    {
        LocalDbService _dbService;
        private Locker _locker;
        public Locker Locker
        {
            get => _locker;
            set
            {
                _locker = value;
                OnPropertyChanged(nameof(Locker));
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                SearchLockers(); // Automatically filter when search query changes
            }
        }

        public ObservableCollection<Locker> Lockers { get; set; }
        public ObservableCollection<Locker> AllLockers { get; set; }

        public ICommand NavigateToAddLockerCommand { get; private set; }
        public ICommand NavigateToLockerCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        public ManageAllLockersViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            Lockers = new ObservableCollection<Locker>();
            AllLockers = new ObservableCollection<Locker>();

            NavigateToAddLockerCommand = new AsyncRelayCommand(NavigateToAddLocker);
            NavigateToLockerCommand = new AsyncRelayCommand(NavigateToLocker);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            UpdateCommand = new AsyncRelayCommand(LoadLockers);
            EditCommand = new AsyncRelayCommand(NavigateToEditLocker);
            LoadLockers(); // Załaduj szafki przy inicjalizacji ViewModel
            WeakReferenceMessenger.Default.Register<UpdateLockersMessage>(this, (r, m) => LoadLockers());
        }

        private async Task  SearchLockers()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                Lockers.Clear();
                foreach (var locker in AllLockers)
                {
                    Lockers.Add(locker);
                }
            }
            else
            {
                var filteredLockers = AllLockers.Where(l => 
                    l.LockerNr.ToString().Contains(SearchQuery) ||
                    l.Owner.ToLower().Contains(SearchQuery.ToLower())
                    ).ToList();
                Lockers.Clear();
                foreach (var locker in filteredLockers)
                {
                    Lockers.Add(locker);
                }
            }
        }
        private async Task PassLockerAndNavigate(string view)
        {
            if (Locker != null)
            {
                var navigationParameters = new Dictionary<string, object>
                {
                    { "Locker", Locker }
                };

                await Shell.Current.GoToAsync($"{view}", navigationParameters);
            }
            else
            {
                Alert("Wybierz szafkę");
            }
        }
        private async Task NavigateToLocker()
        {
            await PassLockerAndNavigate("ManageLocker");
        }
        private async Task NavigateToEditLocker()
        {
            await PassLockerAndNavigate("EditLocker");
        }
        private async Task NavigateToAddLocker()
        {
            await Shell.Current.GoToAsync("AddNewLockerView");
        }

        private async Task DeleteAsync()
        {
            if (Locker != null)
            {
                await _dbService.DeleteLocker(Locker);
                Lockers.Remove(Locker);
                AllLockers.Remove(Locker);
            }
            else
            {
                Alert("Wybierz szafkę");
            }
        }

        private async Task LoadLockers()
        {
            var lockersFromDb = await _dbService.GetAllLockers();
            Lockers.Clear();
            AllLockers.Clear();
            foreach (var locker in lockersFromDb)
            {
                // Tworzenie modelu szafki na podstawie danych z bazy danych
                Locker lockerModel = new Locker
                {
                    ID = locker.ID,
                    LockerNr = locker.LockerNr,
                    RegalNr = locker.RegalNr,
                    Owner = locker.Owner,
                    Floor = locker.Floor,
                    isOccupied = locker.isOccupied
                    // Dodaj inne właściwości, takie jak ID, Owner, Floor, isOccupied, w zależności od twoich potrzeb
                };

                Lockers.Add(lockerModel);
                AllLockers.Add(lockerModel);
            }
        }

        public event EventHandler<string> AlertRequested;

        public void Alert(string message)
        {
            AlertRequested?.Invoke(this, message);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
