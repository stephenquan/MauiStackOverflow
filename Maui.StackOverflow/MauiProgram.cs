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
			.RegisterSample<AnimPopupPage>(nameof(AnimPopupPage), "Demonstrates an animating popup")
			.RegisterSample<BorderLabelPage>(nameof(BorderLabelPage), "Demonstrates a border with a label")
			.RegisterSample<BottomSheetPage>(nameof(BottomSheetPage), "BottomSheet page")
			.RegisterSample<EditorPage>(nameof(EditorPage), "Editor page")
			.RegisterSample<EntryMaskPage>(nameof(EntryMaskPage), "Demonstrates EntryMask behavior")
			.RegisterSample<FragmentPage>(nameof(FragmentPage), "Demonstrates a fragment manager implementation based on ControlView.")
			.RegisterSample<ImageClipPage>(nameof(ImageClipPage), "Demonstrates an image aspect clip")
			.RegisterSample<ImageMapPage>(nameof(ImageMapPage), "Demonstrates an image map")
			.RegisterSample<LocalizedPage>(nameof(LocalizedPage), "Localized page")
			.RegisterSample<NumericEntryPage>(nameof(NumericEntryPage), "Demonstrates NumericEntry control")
			.RegisterSample<ParserPage>(nameof(ParserPage), "Demonstrates C# Parsing expression grammar")
			.RegisterSample<RgbColorPage>(nameof(RgbColorPage), "Demonstrates RgbColor markup extension")
			.RegisterSample<SVGPage>(nameof(SVGPage), "Demonstrates SVG usages")
			.RegisterSample<ThemePage>(nameof(ThemePage), "Demonstrates some AppThemeBinding usages")
			.RegisterSample<UserThemePage>(nameof(UserThemePage), "Demonstrates a custom UserTheme view model")
			.RegisterSample<VolumeSliderPage>(nameof(VolumeSliderPage), "Demonstrates a custom slider")
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
