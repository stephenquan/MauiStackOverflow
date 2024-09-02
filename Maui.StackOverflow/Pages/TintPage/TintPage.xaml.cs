namespace Maui.StackOverflow;

public partial class TintPage : ContentPage
{
	bool opened = false;
	public bool Opened
	{
		get => opened;
		set
		{
			opened = value;
			OnPropertyChanged(nameof(Opened));
		}
	}

	public TintPage()
	{
		InitializeComponent();
	}
}