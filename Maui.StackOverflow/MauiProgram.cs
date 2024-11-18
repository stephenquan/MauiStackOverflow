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
			.RegisterSample<BackspacePage>(nameof(BackspacePage), "Demonstrates a border with a label")
			.RegisterSample<BorderLabelPage>(nameof(BorderLabelPage), "Demonstrates a border with a label")
			.RegisterSample<BottomSheetPage>(nameof(BottomSheetPage), "BottomSheet page")
			.RegisterSample<CheckMarkPage>(nameof(CheckMarkPage), "CheckMark page")
			.RegisterSample<DockPage>(nameof(DockPage), "Dock Layout page")
			.RegisterSample<EditorPage>(nameof(EditorPage), "Editor page")
			.RegisterSample<EntryMaskPage>(nameof(EntryMaskPage), "Demonstrates EntryMask behavior")
			.RegisterSample<ExpanderPage>(nameof(ExpanderPage), "Expander page")
			.RegisterSample<FileSaverPage>(nameof(FileSaverPage), "FileSaver page")
			.RegisterSample<FlexPage>(nameof(FlexPage), "FlexLayout page")
			.RegisterSample<ImageClipPage>(nameof(ImageClipPage), "Demonstrates an image aspect clip")
			.RegisterSample<ImageMapPage>(nameof(ImageMapPage), "Demonstrates an image map")
			.RegisterSample<LocalizedPage>(nameof(LocalizedPage), "Localized page")
			.RegisterSample<NumericEntryPage>(nameof(NumericEntryPage), "Demonstrates NumericEntry control")
			.RegisterSample<ParserPage>(nameof(ParserPage), "Demonstrates C# Parsing expression grammar")
			.RegisterSample<RadioStylePage>(nameof(RadioStylePage), "Demonstrates radio button style")
			.RegisterSample<RgbColorPage>(nameof(RgbColorPage), "Demonstrates RgbColor markup extension")
			.RegisterSample<RgbColorPage>(nameof(RgbColorPage), "RgbColor page")
			.RegisterSample<SVGPage>(nameof(SVGPage), "Demonstrates SVG usages")
			.RegisterSample<ThemePage>(nameof(ThemePage), "Demonstrates some AppThemeBinding usages")
			.RegisterSample<TintPage>(nameof(TintPage), "Demonstrates some AppThemeBinding usages")
			.RegisterSample<TintPopupPage>(nameof(TintPopupPage), "Demonstrates some Popup with tinted background")
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
