namespace Maui.StackOverflow;

public partial class ImageMapPage : ContentPage
{
	public ImageMapPage()
	{
		InitializeComponent();
	}

	void Button_Clicked(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		DisplayAlert("Identify", btn.CommandParameter.ToString(), "OK");
	}
}