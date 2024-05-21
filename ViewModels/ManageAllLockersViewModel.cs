using System.Collections.ObjectModel;
using System.Windows.Input;
using SzafkiSzkolne.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using SzafkiSzkolne.Views;
using System.ComponentModel;
using Microsoft.Maui.Controls;

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

        public ObservableCollection<Locker> Lockers { get; set; }

        public ICommand NavigateToAddLockerCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        public ManageAllLockersViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            Lockers = new ObservableCollection<Locker>();

            NavigateToAddLockerCommand = new AsyncRelayCommand(NavigateToAddLocker);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            UpdateCommand = new AsyncRelayCommand(LoadLockers);
            LoadLockers(); // Załaduj szafki przy inicjalizacji ViewModel
        }

        private async Task NavigateToAddLocker()
        {
            await Shell.Current.GoToAsync("//AddNewLockerView");
        }

        private async Task DeleteAsync()
        {
            if (Locker != null)
            {
                await _dbService.DeleteLocker(Locker);
                Lockers.Remove(Locker);
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
