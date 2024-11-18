using System.Reflection;

namespace Maui.StackOverflow;

public partial class MainPage : ContentPage
{
	public string? MauiVersion { get; set; } = "0.0.0";

	public MainPage()
	{
		var assembly = typeof(MainPage).Assembly;
		foreach (var attribute in assembly.GetCustomAttributes<AssemblyMetadataAttribute>())
		{
			switch (attribute.Key)
			{
				case "Assembly.Metadata.Attribute.MauiVersion":
					MauiVersion = attribute.Value;
					break;
			}
		}

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
