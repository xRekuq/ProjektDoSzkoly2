using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SzafkiSzkolne.Models;

namespace SzafkiSzkolne.ViewModels
{
    public class ManageLockerViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private string _qrMessage;
        public string QrMessage
        {
            get => _qrMessage;
            set
            {
                if (_qrMessage != value)
                {
                    _qrMessage = value;
                    OnPropertyChanged(nameof(QrMessage));
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
                QrMessage = $"Numer szafki to {_locker.LockerNr}";
            }
        }
        public ICommand NavigateBackCommand { get; private set; }
        public ManageLockerViewModel()
        {
            NavigateBackCommand = new AsyncRelayCommand(NavigateBack);
        }

        private async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Locker"))
            {
                Locker = query["Locker"] as Locker;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
