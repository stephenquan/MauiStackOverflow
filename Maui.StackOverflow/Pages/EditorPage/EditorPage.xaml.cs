using System.Diagnostics;

namespace Maui.StackOverflow;

public partial class EditorPage : ContentPage
{
	public static readonly BindableProperty IsEditorFocusedProperty = BindableProperty.Create(nameof(IsEditorFocused), typeof(bool), typeof(EditorPage), false);
	public bool IsEditorFocused
	{
		get => (bool)GetValue(IsEditorFocusedProperty);
		set => SetValue(IsEditorFocusedProperty, value);
	}

	public EditorPage()
	{
		InitializeComponent();

		BindingContext = this;

		PropertyChanged += (s, e) =>
		{
			switch (e.PropertyName)
			{
				case nameof(IsEditorFocused):
					Debug.WriteLine($"IsEditorFocused changed to {IsEditorFocused}");
					break;
			}
		};
	}
}
