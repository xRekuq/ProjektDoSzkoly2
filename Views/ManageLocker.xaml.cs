using SzafkiSzkolne.ViewModels;

namespace SzafkiSzkolne.Views;

public partial class ManageLocker : ContentPage
{
	public ManageLocker(ManageLockerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}