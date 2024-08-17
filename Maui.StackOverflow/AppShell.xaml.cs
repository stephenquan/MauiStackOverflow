namespace Maui.StackOverflow;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RgbColorPage), typeof(RgbColorPage));
		Routing.RegisterRoute(nameof(AppThemeImagePage), typeof(AppThemeImagePage));
	}
}
