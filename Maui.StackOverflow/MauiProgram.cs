using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Maui.StackOverflow.Resources.Strings;

namespace Maui.StackOverflow;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.RegisterSample<BottomSheetPage>(nameof(BottomSheetPage), "BottomSheet page")
			.RegisterSample<CheckMarkPage>(nameof(CheckMarkPage), "CheckMark page")
			.RegisterSample<EditorPage>(nameof(EditorPage), "Editor page")
			.RegisterSample<LocalizedPage>(nameof(LocalizedPage), "Localized page")
			.RegisterSample<NumericEntryPage>(nameof(NumericEntryPage), "Demonstrates NumericEntry control")
			.RegisterSample<RgbColorPage>(nameof(RgbColorPage), "Demonstrates RgbColor markup extension")
			.RegisterSample<ParserPage>(nameof(ParserPage), "Demonstrates C# Parsing expression grammar")
			.RegisterSample<RgbColorPage>(nameof(RgbColorPage), "RgbColor page")
			.RegisterSample<SVGPage>(nameof(SVGPage), "Demonstrates SVG usages")
			.RegisterSample<ThemePage>(nameof(ThemePage), "Demonstrates some AppThemeBinding usages")
			.RegisterSample<UserBoolPage>(nameof(UserBoolPage), "Demonstrates a user boolean")
			.RegisterSample<UserThemePage>(nameof(UserThemePage), "Demonstrates a custom UserTheme view model")
			.RegisterSample<VolumeSliderPage>(nameof(VolumeSliderPage), "Demonstrates a custom slider")
			.RegisterSample<WebPage>(nameof(WebPage), "Demonstrates running adhoc async code in a WebView")
			.AddLocalization<AppStrings>()
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
