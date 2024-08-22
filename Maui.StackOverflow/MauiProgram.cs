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
			.RegisterSample<NumericEntryPage>(nameof(NumericEntryPage), "Demonstrates NumericEntry control")
			.RegisterSample<RgbColorPage>(nameof(RgbColorPage), "Demonstrates RgbColor markup extension")
			.RegisterSample<ParserPage>(nameof(ParserPage), "Demonstrates C# Parsing expression grammar")
			.RegisterSample<ThemePage>(nameof(ThemePage), "Demonstrates some AppThemeBinding usages")
			.RegisterSample<UserThemePage>(nameof(UserThemePage), "Demonstrates a custom UserTheme view model")
			.RegisterSample<EditorPage>(nameof(EditorPage), "Demonstrates a customized Editor in a ContentView")
			.RegisterSample<WebPage>(nameof(WebPage), "Demonstrates running adhoc async code in a WebView")
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
