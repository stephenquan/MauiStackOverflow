using System.Windows.Input;

namespace Maui.StackOverflow;

class CustomEditorInner : Editor
{
	public static readonly BindableProperty FocusedCommandProperty = BindableProperty.Create(nameof(FocusedCommand), typeof(ICommand), typeof(CustomEditorInner), null);
	public ICommand FocusedCommand
	{
		get => (ICommand)GetValue(FocusedCommandProperty);
		set => SetValue(FocusedCommandProperty, value);
	}

	public static readonly BindableProperty UnfocusedCommandProperty = BindableProperty.Create(nameof(UnfocusedCommand), typeof(ICommand), typeof(CustomEditorInner), null);
	public ICommand UnfocusedCommand
	{
		get => (ICommand)GetValue(UnfocusedCommandProperty);
		set => SetValue(UnfocusedCommandProperty, value);
	}

	public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomEditorInner), null);
	public object CommandParameter
	{
		get => GetValue(CommandParameterProperty);
		set => SetValue(CommandParameterProperty, value);
	}

	public CustomEditorInner()
	{
		this.Focused += (s,e) => FocusedCommand?.Execute(CommandParameter);
		this.Unfocused += (s,e) => UnfocusedCommand?.Execute(CommandParameter);
	}
}
