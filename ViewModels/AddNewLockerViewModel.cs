using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.Windows.Input;
using SzafkiSzkolne.Models;

namespace SzafkiSzkolne.ViewModels
{
    public class AddNewLockerViewModel : INotifyPropertyChanged
    {
        LocalDbService _dbService;

        private int _lockerNr;
        public int LockerNr
        {
            get => _lockerNr;
            set
            {
                if (_lockerNr != value)
                {
                    _lockerNr = value;
                    OnPropertyChanged(nameof(LockerNr));
                }
            }
        }

        private string _regalNr;
        public string RegalNr
        {
            get => _regalNr;
            set
            {
                if (_regalNr != value)
                {
                    _regalNr = value;
                    OnPropertyChanged(nameof(RegalNr));
                }
            }
        }

        private string _owner;
        public string Owner
        {
            get => _owner;
            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged(nameof(Owner));
                }
            }
        }
        private int _floor;
        public int Floor
        {
            get => _floor;
            set
            {
                if (_floor != value)
                {
                    _floor = value;
                    OnPropertyChanged(nameof(Floor));
                }
            }
        }
        private bool _isOccupied;
        public bool IsOccupied
        {
            get => _isOccupied;
            set
            {
                if (_isOccupied != value)
                {
                    _isOccupied = value;
                    OnPropertyChanged(nameof(IsOccupied));
                }
            }
        }

        public ICommand AddLockerCommand { get; private set; }
        public ICommand NavigateBackCommand { get; private set; }
        public AddNewLockerViewModel(LocalDbService dbService)
        {
            _dbService = dbService;

            AddLockerCommand = new AsyncRelayCommand(AddLocker);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBack);
        }

        private async Task AddLocker()
        {
            if (!string.IsNullOrWhiteSpace(_owner))
            {
                _isOccupied = true;
            }
            else
            {
                _isOccupied = false;
            }
            var locker = new Locker()
            {
                LockerNr = _lockerNr,
                RegalNr = _regalNr,
                Owner = _owner,
                Floor = _floor,
                isOccupied = _isOccupied
            };
            
            _dbService.CreateLocker(locker);
            
            Alert($"Dodano szafkę numer: {LockerNr}.");

            LockerNr = 0;
            RegalNr = string.Empty;
            Owner = string.Empty;
            Floor = 0;
        }

        private async Task NavigateBack()
        {
            WeakReferenceMessenger.Default.Send(new UpdateLockersMessage());
            await Shell.Current.GoToAsync("..");
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
