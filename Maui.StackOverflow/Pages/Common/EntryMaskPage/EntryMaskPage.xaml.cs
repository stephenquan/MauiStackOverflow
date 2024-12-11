using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Maui.StackOverflow;

public partial class EntryMaskPage : ContentPage
{
	public string Mask { get; set; } = @"^\d*$";

	public static readonly BindableProperty RawCursorPositionProperty = BindableProperty.Create(nameof(RawCursorPosition), typeof(int), typeof(EntryMaskPage), 0);
	public int RawCursorPosition
	{
		get => (int)GetValue(RawCursorPositionProperty);
		set => SetValue(RawCursorPositionProperty, value);
	}

	public static readonly BindableProperty RawSelectionLengthProperty = BindableProperty.Create(nameof(RawSelectionLength), typeof(int), typeof(EntryMaskPage), 0);
	public int RawSelectionLength
	{
		get => (int)GetValue(RawSelectionLengthProperty);
		set => SetValue(RawSelectionLengthProperty, value);
	}

	public static readonly BindableProperty RawTextProperty = BindableProperty.Create(nameof(RawText), typeof(string), typeof(EntryMaskPage), string.Empty);
	public string RawText
	{
		get => (string)GetValue(RawTextProperty);
		set => SetValue(RawTextProperty, value);
	}

	public static readonly BindableProperty ValidTextProperty = BindableProperty.Create(nameof(ValidText), typeof(string), typeof(EntryMaskPage), string.Empty);
	public string ValidText
	{
		get => (string)GetValue(ValidTextProperty);
		set => SetValue(ValidTextProperty, value);
	}

	public static readonly BindableProperty ValidCursorPositionProperty = BindableProperty.Create(nameof(ValidCursorPosition), typeof(int), typeof(EntryMaskPage), 0);
	public int ValidCursorPosition
	{
		get => (int)GetValue(ValidCursorPositionProperty);
		set => SetValue(ValidCursorPositionProperty, value);
	}

	//public int ValidCursorPosition => RawCursorPosition < ValidText.Length ? RawCursorPosition : ValidText.Length;
	public int ValidSelectionLength =>
		RawSelectionLength <= 0
		? 0
		: ValidCursorPosition + RawSelectionLength < ValidText.Length
		? RawSelectionLength
		: ValidText.Length - ValidCursorPosition;

	long FixTicks { get; set; } = 0;

	public FormattedString FormattedText
	{
		get
		{
			var sq = DateTime.Now.Ticks;
			FormattedString formattedString = new FormattedString();
			string section = "unknown";
			//Debug.WriteLine($"ValidText: {ValidText}, ValidCursorPosition: {ValidCursorPosition}, ValidSelectionLength: {ValidSelectionLength}");
			try
			{
				Span span;

				int cursorPosition = ValidCursorPosition;
				int selectionLength = ValidSelectionLength;
				if (cursorPosition < 0)
				{
					cursorPosition = 0;
				}
				if (cursorPosition > ValidText.Length)
				{
					cursorPosition = ValidText.Length;
				}

				if (cursorPosition >= 0)
				{
					section = "left";
					string left = ValidText.Substring(0, cursorPosition);
					if (selectionLength <= 0)
					{
						span = new Span { Text = left + "\u20d2" };
					}
					else
					{
						span = new Span { Text = left };
					}
					formattedString.Spans.Add(span);
				}

				if (selectionLength > 0)
				{
					section = "selection";
					span = new Span { Text = ValidText.Substring(cursorPosition, selectionLength), BackgroundColor = Colors.Blue, TextColor = Colors.White };
					formattedString.Spans.Add(span);
				}

				if (cursorPosition + selectionLength < ValidText.Length)
				{
					section = "right";
					string right = ValidText.Substring(cursorPosition + selectionLength);
					span = new Span { Text = right };
					formattedString.Spans.Add(span);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[{section}] Exception: {ex.Message}");
			}



			//formattedString.Spans.Add(new Span { Text = "Red bold, ", TextColor = Colors.Red, FontAttributes = FontAttributes.Bold, BackgroundColor = Colors.Blue });

			//Span span = new Span { Text = "default, " };
			//span.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => await DisplayAlert("Tapped", "This is a tapped Span.", "OK")) });
			//formattedString.Spans.Add(span);
			//formattedString.Spans.Add(new Span { Text = "italic small.", FontAttributes = FontAttributes.Italic, FontSize = 14 });


			return formattedString;
		}
	}
	public EntryMaskPage()
	{
		InitializeComponent();

		PropertyChanged += OnPropertyChanged;
	}

	async void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
			case nameof(RawText):
				//Debug.WriteLine($"RawText: {RawText}");
				if (Regex.IsMatch(RawText, Mask))
				{
					ValidText = RawText;
					OnPropertyChanged(nameof(FormattedText));
				}
				else
				{
					FixTicks = DateTime.Now.Ticks;
					await Task.Delay(1);
					RawText = ValidText;
				}
				break;

			case nameof(RawCursorPosition):
				await Task.Delay(10);
				//Debug.WriteLine($"RawCursorPosition: {RawCursorPosition} {DateTime.Now.Ticks - FixTicks}");
				if (DateTime.Now.Ticks - FixTicks < 15 * 10000)
				{
					return;
				}

				ValidCursorPosition = RawCursorPosition;
				OnPropertyChanged(nameof(FormattedText));
				break;

			case nameof(RawSelectionLength):
				await Task.Delay(10);
				if (DateTime.Now.Ticks - FixTicks < 15 * 10000)
				{
					return;
				}
				OnPropertyChanged(nameof(FormattedText));
				break;

				//case nameof(RawCursorPosition):
				//	Debug.WriteLine($"RawCursorPosition: {RawCursorPosition}");
				//	if (RawText == ValidText)
				//	{
				//		RawCursorPosition = ValidCursorPosition;
				//	}
		}
	}
}