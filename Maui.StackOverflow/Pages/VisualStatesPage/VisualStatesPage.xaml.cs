using System.Collections.ObjectModel;

namespace Maui.StackOverflow;

public partial class VisualStatesPage : ContentPage
{
	public ObservableCollection<string> TenFruits { get; } = new ()
	{
		"Apple",
		"Banana",
		"Cherry",
		"Date",
		"Elderberry",
		"Fig",
		"Grape",
		"Honeydew",
		"Ice Cream Bean",
		"Jackfruit"
	};

	public static readonly BindableProperty SelectedFruitProperty = BindableProperty.Create(nameof(SelectedFruit), typeof(string), typeof(VisualStatesPage), default(string));
	public string SelectedFruit
	{
		get => (string)GetValue(SelectedFruitProperty);
		set => SetValue(SelectedFruitProperty, value);
	}

	public VisualStatesPage()
	{
		InitializeComponent();
	}
}