using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Maui.StackOverflow;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.RegisterSample<NumericEntryPage>(nameof(NumericEntryPage))
			.RegisterSample<RgbColorPage>(nameof(RgbColorPage))
			.RegisterSample<ThemePage>(nameof(ThemePage))
			.RegisterSample<UserThemePage>(nameof(UserThemePage))
			.RegisterSample<EditorPage>(nameof(EditorPage))
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddSingleton<UserThemes>();

		return builder.Build();
	}
}
