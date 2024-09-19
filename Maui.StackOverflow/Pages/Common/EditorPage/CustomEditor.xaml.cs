using System.Windows.Input;

namespace Maui.StackOverflow;

public partial class CustomEditor : ContentView
{
	public static readonly BindableProperty FocusedCommandProperty = BindableProperty.Create(nameof(FocusedCommand), typeof(ICommand), typeof(CustomEditor), null);
	public ICommand FocusedCommand
	{
		get => (ICommand)GetValue(FocusedCommandProperty);
		set => SetValue(FocusedCommandProperty, value);
	}

	public static readonly BindableProperty UnfocusedCommandProperty = BindableProperty.Create(nameof(UnfocusedCommand), typeof(ICommand), typeof(CustomEditor), null);
	public ICommand UnfocusedCommand
	{
		get => (ICommand)GetValue(UnfocusedCommandProperty);
		set => SetValue(UnfocusedCommandProperty, value);
	}

	public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomEditor), null);
	public object CommandParameter
	{
		get => GetValue(CommandParameterProperty);
		set => SetValue(CommandParameterProperty, value);
	}

	public CustomEditor()
	{
		InitializeComponent();
	}
}