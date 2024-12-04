using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Maui.StackOverflow;

public partial class DateCultureModel : ObservableObject
{
	public DateTime StartDate { get; set; } = DateTime.Now;

	public CultureInfo Culture
	{
		get => CultureInfo.CurrentUICulture;
		set
		{
			CultureInfo.CurrentUICulture = value;
			CultureInfo.CurrentCulture = value;
			OnPropertyChanged();
		}
	}

	public CultureInfo this[string cultureName] => new CultureInfo(cultureName);

	[RelayCommand]
	public void SetCulture(CultureInfo culture) => Culture = culture;
}
