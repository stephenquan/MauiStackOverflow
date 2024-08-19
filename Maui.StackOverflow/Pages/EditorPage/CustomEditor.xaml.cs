namespace Maui.StackOverflow;

public partial class CustomEditor : ContentView
{
	public static readonly BindableProperty IsEditorFocusedProperty = BindableProperty.Create(nameof(IsEditorFocused), typeof(bool), typeof(CustomEditor), false);
	public bool IsEditorFocused
	{
		get => (bool)GetValue(IsFocusedProperty);
		set => SetValue(IsFocusedProperty, value);
	}

	public CustomEditor()
	{
		InitializeComponent();
	}
}