using System.Collections.ObjectModel;
using System.Globalization;

namespace Maui.StackOverflow;

public class AppThemeBindingExtension : BindableObject, IMarkupExtension<BindingBase>, IMultiValueConverter
{
	public AppTheme Theme => Application.Current!.RequestedTheme;

	public static readonly BindableProperty LightProperty = BindableProperty.Create(nameof(Light), typeof(string), typeof(AppThemeBindingExtension));
	public string Light
	{
		get { return (string)GetValue(LightProperty); }
		set { SetValue(LightProperty, value); }
	}

	public static readonly BindableProperty DarkProperty = BindableProperty.Create(nameof(Dark), typeof(string), typeof(AppThemeBindingExtension));
	public string Dark
	{
		get { return (string)GetValue(DarkProperty); }
		set { SetValue(DarkProperty, value); }
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
				new Binding(nameof(Theme), BindingMode.OneWay, null, null, null, this),
				new Binding(nameof(Light), BindingMode.OneWay, null, null, null, this),
				new Binding(nameof(Dark), BindingMode.OneWay, null, null, null, this)
			}
		};
	}

	public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values is null || values.Length != 3)
		{
			return null;
		}

		if (values[0] is AppTheme theme)
		{
			return theme != AppTheme.Dark ? values[1] : values[2];
		}
		return null;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}