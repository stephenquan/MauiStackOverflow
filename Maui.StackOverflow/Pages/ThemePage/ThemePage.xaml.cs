namespace Maui.StackOverflow;

public partial class ThemePage : ContentPage
{
	public ThemePage()
	{
		InitializeComponent();
	}

	void OnDayNightClicked(object sender, EventArgs e)
	{
		Application.Current!.UserAppTheme = Application.Current.RequestedTheme != AppTheme.Light ? AppTheme.Light : AppTheme.Dark;
	}
}