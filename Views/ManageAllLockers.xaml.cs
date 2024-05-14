using SzafkiSzkolne.ViewModels;

namespace SzafkiSzkolne.Views;

public partial class ManageAllLockers : ContentPage
{
	public ManageAllLockers(ManageAllLockersViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        viewModel.AlertRequested += ViewModel_AlertRequested;
    }
    private async void ViewModel_AlertRequested(object sender, string e)
    {
        await DisplayAlert("Alert", e, "OK");
    }
}