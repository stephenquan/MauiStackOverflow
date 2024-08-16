namespace Maui.StackOverflow;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(Maui.StackOverflow.Answers.RgbColor.RgbColorPage), typeof(Maui.StackOverflow.Answers.RgbColor.RgbColorPage));
	}
}
