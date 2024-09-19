using CommunityToolkit.Maui.Views;

namespace Maui.StackOverflow;

public partial class AnimPopup : Popup
{
	public AnimPopup()
	{
		InitializeComponent();
	}

	async void OnAnimateEntry(object sender, EventArgs e)
	{
		await content.TranslateTo(0, 0, 250, Easing.CubicIn);
	}

	async void OnAnimateExit(object sender, EventArgs e)
	{
		await content.TranslateTo(0, 200, 250, Easing.CubicOut);
		await this.CloseAsync();
	}
}
