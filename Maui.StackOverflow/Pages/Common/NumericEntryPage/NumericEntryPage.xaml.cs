using System.Text.RegularExpressions;

namespace Maui.StackOverflow;

public partial class NumericEntryPage : ContentPage
{
	string numericText = "0";
	public string NumericText
	{
		get => numericText;
		set
		{
			if (numericText == value)
			{
				return;
			}

			if (Regex.Matches(value, @"^\-?\d*$").Count > 0)
			{
				numericText = value;
			}

			OnPropertyChanged(nameof(NumericText));
		}
	}

	public NumericEntryPage()
	{
		InitializeComponent();

		BindingContext = this;
	}
}