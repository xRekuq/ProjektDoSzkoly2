using SzafkiSzkolne.ViewModels;

namespace SzafkiSzkolne.Views;

public partial class EditLockerView : ContentPage
{
	public EditLockerView(EditLockerViewModel viewModel)
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