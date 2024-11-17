namespace Maui.StackOverflow;

/// <summary>
/// Provides the RgbColor XAML markup extension for creating colors from R, G, B, A bindable properties.
/// </summary>
/// <code>
/// <BoxView Color="{local:RgbColor R={Binding ...}, G={Binding ...}, B={Binding ...}, A={Binding ...}}" />
/// </code>
[ContentProperty(nameof(R))]
public class RgbColorExtension : BindableObject, IMarkupExtension<BindingBase>
{
	public static readonly BindableProperty RProperty = BindableProperty.Create(nameof(R), typeof(float), typeof(RgbColorExtension),
		propertyChanged: (b, o, n) => ((RgbColorExtension)b).OnColorChanged());
	public float R
	{
		get { return (float)GetValue(RProperty); }
		set { SetValue(RProperty, value); }
	}

	public static readonly BindableProperty GProperty = BindableProperty.Create(nameof(G), typeof(float), typeof(RgbColorExtension),
		propertyChanged: (b, o, n) => ((RgbColorExtension)b).OnColorChanged());
	public float G
	{
		get { return (float)GetValue(GProperty); }
		set { SetValue(GProperty, value); }
	}

	public static readonly BindableProperty BProperty = BindableProperty.Create(nameof(B), typeof(float), typeof(RgbColorExtension),
		propertyChanged: (b, o, n) => ((RgbColorExtension)b).OnColorChanged());
	public float B
	{
		get { return (float)GetValue(BProperty); }
		set { SetValue(BProperty, value); }
	}

	public static readonly BindableProperty AProperty = BindableProperty.Create(nameof(A), typeof(float), typeof(RgbColorExtension),
		propertyChanged: (b, o, n) => ((RgbColorExtension)b).OnColorChanged());
	public float A
	{
		get { return (float)GetValue(AProperty); }
		set { SetValue(AProperty, value); }
	}

	public Color Color => Color.FromRgba(R / 255f, G / 255f, B / 255f, A / 255f);

	public void OnColorChanged() => OnPropertyChanged(nameof(Color));

	public object ProvideValue(IServiceProvider serviceProvider)
		=> (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);

	BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
		=> new Binding(nameof(Color), source: this);
}
