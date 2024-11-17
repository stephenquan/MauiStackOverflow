using System.Text.RegularExpressions;

namespace Maui.StackOverflow;

public class MaskEntry : Entry
{
	public string Mask { get; set; } = @"^\-?\d*$";
	public MaskEntry()
	{
		TextChanged += async (s, e) =>
		{
			if (!string.IsNullOrEmpty(Mask) && !Regex.Match(e.NewTextValue, Mask).Success)
			{
				Opacity = 0.1;
				await Task.Delay(1);
				Opacity = 1;
				Text = e.OldTextValue ?? "";
			}
		};
	}
}
