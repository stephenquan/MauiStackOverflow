using System.Collections.ObjectModel;
using System.Globalization;

namespace Maui.StackOverflow;

public enum ThemeKey
{
	TextColor,
	BackgroundColor
}

[ContentProperty(nameof(Key))]
public class UserThemeExtension : BindableObject, IMarkupExtension<BindingBase>, IMultiValueConverter
{
	public UserThemes UserThemes { get; }

	public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(ThemeKey), typeof(UserThemeExtension), ThemeKey.TextColor);
	public ThemeKey Key
	{
		get { return (ThemeKey)GetValue(KeyProperty); }
		set { SetValue(KeyProperty, value); }
	}

	public UserThemeExtension()
	{
		UserThemes = IPlatformApplication.Current!.Services.GetService<UserThemes>()!;
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
				new Binding(nameof(UserThemes.CurrentTheme), BindingMode.OneWay, null, null, null, UserThemes),
				new Binding(nameof(Key), BindingMode.OneWay, null, null, null, this)
			}
		};
	}

	public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values is null || values.Length < 2)
		{
			return null;
		}

		if (values[0] is UserTheme theme && values[1] is ThemeKey key)
		{
			return key switch
			{
				ThemeKey.TextColor => theme.TextColor,
				ThemeKey.BackgroundColor => theme.BackgroundColor,
				_ => null
			};
		}

		return null;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}