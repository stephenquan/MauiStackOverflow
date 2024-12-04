namespace Maui.StackOverflow;

public partial class VisualStatesView : ContentView
{
	public static readonly BindableProperty IsNormalProperty = BindableProperty.Create(nameof(IsNormal), typeof(bool), typeof(VisualStatesView), false);
	public bool IsNormal
	{
		get => (bool)GetValue(IsNormalProperty);
		set => SetValue(IsNormalProperty, value);
	}

	public static readonly BindableProperty IsPointerOverProperty = BindableProperty.Create(nameof(IsPointerOver), typeof(bool), typeof(VisualStatesView), false);
	public bool IsPointerOver
	{
		get => (bool)GetValue(IsPointerOverProperty);
		set => SetValue(IsPointerOverProperty, value);
	}

	public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(VisualStatesView), false);
	public bool IsSelected
	{
		get => (bool)GetValue(IsSelectedProperty);
		set => SetValue(IsSelectedProperty, value);
	}

	public VisualStatesView()
	{
		InitializeComponent();
	}
}