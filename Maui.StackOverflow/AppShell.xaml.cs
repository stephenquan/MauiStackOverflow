namespace Maui.StackOverflow;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		foreach (var sample in Samples.AllSamples)
		{
			Routing.RegisterRoute(sample.Name, sample.Page);
		}
	}
}
