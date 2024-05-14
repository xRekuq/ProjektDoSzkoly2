using SzafkiSzkolne.Views;

namespace SzafkiSzkolne
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("//AddNewLockerView", typeof(AddNewLockerView));
            Routing.RegisterRoute("//ManageAllLockers", typeof(ManageAllLockers));
        }
    }
}
