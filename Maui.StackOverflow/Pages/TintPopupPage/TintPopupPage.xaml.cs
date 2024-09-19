using CommunityToolkit.Maui.Views;

namespace Maui.StackOverflow;

public partial class TintPopupPage : ContentPage
{
	public TintPopupPage()
	{
		InitializeComponent();
	}

	async void Button_Clicked(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		var p = new TintPopup();
		p.Anchor = btn;
		await this.ShowPopupAsync(p);

	}
}