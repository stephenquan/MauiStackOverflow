namespace Maui.StackOverflow;

public partial class BorderLabel : ContentView
{
	public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(BorderLabel), "Title");
	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public BorderLabel()
	{
		InitializeComponent();
	}
}