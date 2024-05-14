using Microsoft.Extensions.Logging;
using SzafkiSzkolne.Models;
using SzafkiSzkolne.ViewModels;
using SzafkiSzkolne.Views;

namespace SzafkiSzkolne
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services
                .AddSingleton<LocalDbService>()
                .AddScoped<ManageAllLockers>()
                .AddTransient<ManageAllLockersViewModel>()
                .AddScoped<AddNewLockerView>()
                .AddTransient<AddNewLockerViewModel>()
                .AddScoped<ManageLocker>()
                .AddTransient<ManageLockerViewModel>();

            return builder.Build();
        }
    }
}
