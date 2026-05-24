using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ToDoApp
{
    public static class MauiProgram
    {

        //dziala tak jak index.php w php
        //jest to punkt wejscia do aplikacji
        //konfiguruje co apliakcja ma zaladowac i uruchamia ja
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()

                // dodaje dodatkowe komponenty z biblioteki communitytoolkit

                .UseMauiCommunityToolkit() 
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
