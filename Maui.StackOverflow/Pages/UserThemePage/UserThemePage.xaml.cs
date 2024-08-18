namespace Maui.StackOverflow;

public partial class UserThemePage : ContentPage
{
	public UserThemes UserThemes { get; }

	public UserThemePage(UserThemes userThemes)
	{
		UserThemes = userThemes;

		InitializeComponent();

		BindingContext = this;
	}

	void OnThemeClicked(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		UserThemes.CurrentThemeName = btn.Text;
	}
}
