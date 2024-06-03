using SzafkiSzkolne.Views;

namespace SzafkiSzkolne
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Routing
            Routing.RegisterRoute("AddNewLockerView", typeof(AddNewLockerView));
            Routing.RegisterRoute("ManageAllLockers", typeof(ManageAllLockers));
            Routing.RegisterRoute("ManageLocker", typeof(ManageLocker));
            Routing.RegisterRoute("EditLocker", typeof(EditLockerView));
        }
    }
}
