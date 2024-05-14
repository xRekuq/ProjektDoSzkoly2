using SzafkiSzkolne.ViewModels;

namespace SzafkiSzkolne.Views;

public partial class AddNewLockerView : ContentPage
{
	public AddNewLockerView(AddNewLockerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}