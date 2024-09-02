using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.StackOverflow;
public class EntryWithBackspace : Entry
{
	public event EventHandler? BackspacePressed;

	public static readonly BindableProperty UserTextProperty = BindableProperty.Create(nameof(UserText), typeof(string), typeof(EntryWithBackspace), string.Empty);
	public string UserText
	{
		get => (string)GetValue(UserTextProperty);
		set => SetValue(UserTextProperty, value);
	}

	//string invisChar = "X";
	string invisChar = " ";

	public EntryWithBackspace()
	{
		this.PropertyChanged += async (s, e) =>
		{
			switch (e.PropertyName)
			{
				case nameof(Text):
					if ((Text ?? "").Length < (invisChar + (UserText ?? "")).Length)
					{
						BackspacePressed?.Invoke(this, EventArgs.Empty);
						Debug.WriteLine("Backspace");
					}
					if (Text is string text && text.Length >= 1 && text.Substring(0,1) == invisChar)
					{
						UserText = text.Substring(1);
					}
					else
					{
						UserText = Text ?? "";
						await Task.Delay(1);
						Text = invisChar + UserText;
					}
					break;

				case nameof(UserText):
					Text = invisChar + UserText;
					break;
			}
		};

		OnPropertyChanged(nameof(UserText));
	}
}
