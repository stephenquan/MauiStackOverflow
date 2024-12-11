using CommunityToolkit.Mvvm.ComponentModel;

namespace Maui.StackOverflow;

public class UserTheme
{

	public Color TextColor { get; set; } = Colors.Black;
	public Color BackgroundColor { get; set; } = Colors.White;
}

public partial class UserThemes : ObservableObject
{
	public Dictionary<string, UserTheme> Dict { get; } = new Dictionary<string, UserTheme>()
	{
		{ "Light", new UserTheme() {
			BackgroundColor = Colors.White,
			TextColor = Colors.Black
		} },
		{ "Dark", new UserTheme() {
			BackgroundColor = Colors.Black,
			TextColor = Colors.White
		} },
		{ "Blue", new UserTheme() {
			BackgroundColor = Colors.Blue,
			TextColor = Colors.Yellow
		} },
		{ "Yellow", new UserTheme() {
			BackgroundColor = Colors.Yellow,
			TextColor = Colors.Blue
		} }
	};

	public List<string> ThemeNames => Dict.Keys.ToList();

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(CurrentTheme))]
	string currentThemeName = "Light";

	public UserTheme CurrentTheme => Dict[CurrentThemeName];
}