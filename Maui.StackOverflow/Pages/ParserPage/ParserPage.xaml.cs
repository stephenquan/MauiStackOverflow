using System.Diagnostics;

namespace Maui.StackOverflow;

public partial class ParserPage : ContentPage
{
	public Parser Parser { get; } = new Parser();

	public static readonly BindableProperty ExpressionProperty = BindableProperty.Create(nameof(Expression), typeof(string), typeof(ParserPage), "1+2",
		propertyChanged: (b, o, n) => ((ParserPage)b).OnResultChanged());
	public string Expression
	{
		get => (string)GetValue(ExpressionProperty);
		set => SetValue(ExpressionProperty, value);
	}

	public object Result
	{
		get
		{
			try
			{
				return Parser.Execute(Expression);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Exception: {ex.Message}");
				return ex.Message;
			}
		}
	}

	public void OnResultChanged() => OnPropertyChanged(nameof(Result));

	public List<string> Samples { get; } = new List<string> { "1+2", "1+2*3", "1+2*3/4", "1+2*3/4-5" };

	public ParserPage()
	{
		InitializeComponent();
	}

	async void OnSample(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		btn.IsEnabled = false;
		await Task.Delay(50);
		Expression = btn.Text;
		btn.IsEnabled = true;
	}
}