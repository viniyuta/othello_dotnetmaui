using Microsoft.Extensions.Logging;
using DotNet.Meteor.HotReload.Plugin;

namespace OthelloUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        MauiAppBuilder mauiAppBuilder = builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Bangers-Regular.ttf", "Bangers");
			});
#if DEBUG
		builder.Logging.AddDebug();
		builder.EnableHotReload();
		
#endif
		return builder.Build();
	}
}