using System.Data;
using System.Diagnostics;

namespace Maui.StackOverflow;

public partial class ParserPage : ContentPage
{
	public ParserPage()
	{
		InitializeComponent();
	}

	void OnClicked(object sender, EventArgs e)
	{
		try
		{
			Parser parser = new Parser();

			double fourtySeven = parser.Execute("5 + 6 * 7"); // 47
			result.Text += $"fourtySeven: {fourtySeven}" + "\n";

			double eleven = parser.Execute("5 + 6"); // 11
			result.Text += $"eleven: {eleven}" + "\n";

			parser.Execute("1 + 2 + 3 + "); // throws an exception
		}
		catch (Exception ex)
		{
			result.Text += $"Exception: {ex.Message}" + "\n";
		}
	}
}