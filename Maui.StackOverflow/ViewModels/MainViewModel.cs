using System.Collections.ObjectModel;

namespace Maui.StackOverflow;

public partial class MainViewModel
{
	public ObservableCollection<string> Samples { get; } = new ObservableCollection<string>
	{
		"RgbColor",
		"AppThemeImage"
	};

	public MainViewModel()
	{
	}
}
