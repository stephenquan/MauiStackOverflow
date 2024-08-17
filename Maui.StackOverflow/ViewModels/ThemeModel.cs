using System.Collections.ObjectModel;

namespace Maui.StackOverflow;

public class ThemeItem
{
	public string LightImage { get; set; } = string.Empty;
	public string DarkImage { get; set; } = string.Empty;

	public ThemeItem(string lightImage, string darkImage)
	{
		LightImage = lightImage;
		DarkImage = darkImage;
	}
}

public class ThemeModel
{
	public ObservableCollection<ThemeItem> Items { get; } = new ObservableCollection<ThemeItem>()
	{
		new ThemeItem("icon_yellow_moon.png", "icon_blue_moon.png")
	};
}
