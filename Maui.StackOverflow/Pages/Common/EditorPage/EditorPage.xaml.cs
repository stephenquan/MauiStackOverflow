using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;

namespace Maui.StackOverflow;

public partial class EditorPage : ContentPage
{
	[RelayCommand]
	void MyFocused(int magic)
	{
		Debug.WriteLine($"OnFocused magic:{magic}");
	}

	[RelayCommand]
	void MyUnfocused(int magic)
	{
		Debug.WriteLine($"OnUnfocused magic:{magic}");
	}

	public int Magic { get; set; } = 42;

	public EditorPage()
	{
		InitializeComponent();

		BindingContext = this;
	}
}