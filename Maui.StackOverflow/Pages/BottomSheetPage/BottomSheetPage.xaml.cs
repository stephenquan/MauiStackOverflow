using Maui.StackOverflow.Resources.Strings;

namespace Maui.StackOverflow;

public partial class BottomSheetPage : ContentPage
{
	int count = 0;

	public BottomSheetPage()
	{
		InitializeComponent();
	}

	void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
		{
			CounterBtn.Text = string.Format("Clicked {0} time", count);
		}
		else
		{
			CounterBtn.Text = string.Format("Clicked {0} times", count);
		}

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}