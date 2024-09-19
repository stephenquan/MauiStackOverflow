using CommunityToolkit.Maui.Views;

namespace Maui.StackOverflow;

public partial class AnimPopupPage : ContentPage
{
	public AnimPopupPage()
	{
		InitializeComponent();
	}

	async void Button_Clicked(object sender, EventArgs e)
	{
		var p = new AnimPopup();
		await this.ShowPopupAsync(p);
	}
}