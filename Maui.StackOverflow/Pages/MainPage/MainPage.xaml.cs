using CommunityToolkit.Mvvm.Input;

namespace Maui.StackOverflow;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    public async void OnNavigate(object sender, EventArgs e)
    {
		Button btn = (Button)sender;
		btn.IsEnabled = false;
		await Task.Delay(50);
        await Shell.Current.GoToAsync(btn.CommandParameter.ToString());
		btn.IsEnabled = true;
    }
}
