using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using SzafkiSzkolne.Models;

namespace SzafkiSzkolne.ViewModels
{
    public class EditLockerViewModel : IQueryAttributable, INotifyPropertyChanged
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
        private string _isOccupied;
        public string IsOccupied
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
        public ICommand NavigateBackCommand { get; private set; }
        public ICommand EditLockerCommand { get; private set; }
        public EditLockerViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            NavigateBackCommand = new AsyncRelayCommand(NavigateBack);
            EditLockerCommand = new AsyncRelayCommand(EditLocker);
        }

        private async Task EditLocker()
        {
            if (!string.IsNullOrWhiteSpace(_owner))
            {
                _isOccupied = "Tak";
            }
            else
            {
                _isOccupied = "Nie";
            }

            Locker.LockerNr = _lockerNr;
            Locker.RegalNr = _regalNr;
            Locker.Owner = _owner;
            Locker.Floor = _floor;
            Locker.isOccupied = _isOccupied;

            _dbService.UpdateLocker(Locker);

            Alert($"Zmiany zapisano.");
            await NavigateBack();
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
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Locker"))
            {
                Locker = query["Locker"] as Locker;
                if (Locker != null)
                {
                    LockerNr = Locker.LockerNr;
                    RegalNr = Locker.RegalNr;
                    Owner = Locker.Owner;
                    Floor = Locker.Floor;
                    IsOccupied = Locker.isOccupied;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
