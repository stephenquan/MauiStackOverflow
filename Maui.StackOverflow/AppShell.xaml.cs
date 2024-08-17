namespace Maui.StackOverflow;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		foreach (var sample in Samples.Items)
		{
			Routing.RegisterRoute(sample.Name, sample.Page);
		}
	}
}
