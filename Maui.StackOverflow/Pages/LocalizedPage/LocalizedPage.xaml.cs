using System.Globalization;

namespace Maui.StackOverflow;

public partial class LocalizedPage : ContentPage
{
	public List<CultureInfo> Languages { get; } = new List<CultureInfo>
	{
		new CultureInfo("en-US"),
		new CultureInfo("fr-FR")
	};

	int count = 0;

	public LocalizedPage()
	{
		InitializeComponent();
	}

	void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
		{
			CounterBtn.SetLocalize(Button.TextProperty, "BTN_CLICKED_1_TIME", count);
		}
		else
		{
			CounterBtn.SetLocalize(Button.TextProperty, "BTN_CLICKED_N_TIMES", count);
		}

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

}