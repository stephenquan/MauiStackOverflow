using System.Globalization;

namespace Maui.StackOverflow;

public partial class LocalizedPage : ContentPage
{
	public LocalizedPage()
	{
		InitializeComponent();
	}

	void OnLanguageClicked(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		LocalizationManager.Current!.CurrentUICulture = new CultureInfo(btn.Text.ToString());
	}
}
