using System.Collections.ObjectModel;
using System.Globalization;

namespace Maui.StackOverflow;

[ContentProperty(nameof(R))]
public class RgbColorExtension : BindableObject, IMarkupExtension<BindingBase>, IMultiValueConverter
{
	public static readonly BindableProperty RProperty = BindableProperty.Create(nameof(R), typeof(float), typeof(RgbColorExtension));
	public float R
	{
		get { return (float)GetValue(RProperty); }
		set { SetValue(RProperty, value); }
	}

	public static readonly BindableProperty GProperty = BindableProperty.Create(nameof(G), typeof(float), typeof(RgbColorExtension));
	public float G
	{
		get { return (float)GetValue(GProperty); }
		set { SetValue(GProperty, value); }
	}

	public static readonly BindableProperty BProperty = BindableProperty.Create(nameof(B), typeof(float), typeof(RgbColorExtension));
	public float B
	{
		get { return (float)GetValue(BProperty); }
		set { SetValue(BProperty, value); }
	}

	public static readonly BindableProperty AProperty = BindableProperty.Create(nameof(A), typeof(float), typeof(RgbColorExtension));
	public float A
	{
		get { return (float)GetValue(AProperty); }
		set { SetValue(AProperty, value); }
	}

	public object ProvideValue(IServiceProvider serviceProvider)
	{
		return (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
	}

	BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
	{
		return new MultiBinding()
		{
			Converter = this,
			Mode = BindingMode.OneWay,
			Bindings = new Collection<BindingBase>
			{
				new Binding(nameof(R), BindingMode.OneWay, null, null, null, this),
				new Binding(nameof(G), BindingMode.OneWay, null, null, null, this),
				new Binding(nameof(B), BindingMode.OneWay, null, null, null, this),
				new Binding(nameof(A), BindingMode.OneWay, null, null, null, this)
			}
		};
	}

	public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values is null || values.Length != 4 || values[0] is not float || values[1] is not float || values[2] is not float || values[3] is not float)
		{
			return null;
		}

		float R = (float)values[0];
		float G = (float)values[1];
		float B = (float)values[2];
		float A = (float)values[3];
		return Color.FromRgba(R / 255f, G / 255f, B / 255f, A / 255f);
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
