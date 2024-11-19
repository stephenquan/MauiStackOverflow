using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;

namespace Maui.StackOverflow;

public static class LocalizationManagerExtensions
{
	public static MauiAppBuilder AddLocalization(this MauiAppBuilder builder, Type stringType)
		=> LocalizationManager.AddLocalization(builder, stringType);
	public static MauiAppBuilder AddLocalization<T>(this MauiAppBuilder builder)
		=> LocalizationManager.AddLocalization(builder, typeof(T));


	public static void SetLocalize(this BindableObject bindable, BindableProperty property, string key, params object[] args)
		=> LocalizationManager.Current!.SetLocalize(bindable, property, key, args);
}

[ContentProperty(nameof(Key))]
public class LocalizeExtension : BindableObject, IMarkupExtension<BindingBase>
{
	public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(string), typeof(LocalizeExtension), null,
		propertyChanged: (b,o,n) => ((LocalizeExtension) b).OnTextChanged());
	public string Key
	{
		get => (string)GetValue(KeyProperty);
		set => SetValue(KeyProperty, value);
	}

	public static readonly BindableProperty X0Property = BindableProperty.Create(nameof(X0), typeof(object), typeof(LocalizeExtension), null,
		propertyChanged: (b, o, n) => ((LocalizeExtension)b).OnTextChanged());
	public object X0
	{
		get => GetValue(X0Property);
		set => SetValue(X0Property, value);
	}

	public string? Text
		=> LocalizationManager.Current?.GetLocalizedString(Key, X0);

	public void OnTextChanged()
	{
		OnPropertyChanged(nameof(Text));
	}

	public object ProvideValue(IServiceProvider serviceProvider)
	=> (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);

	BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
		=> new Binding(nameof(Text), source: this);

	public LocalizeExtension()
	{
		LocalizationManager.Current!.PropertyChanged += (s, e) => OnTextChanged();
	}
}

public class LocalizeConverter : IMultiValueConverter
{
	public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		=> (values.Length >= 2) && (values[0] is string key)
			? LocalizationManager.Current?.GetLocalizedString(key, values.Skip(2).ToArray())
			: null;
	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		=> throw new NotImplementedException();
}

public partial class LocalizationManager : ObservableObject
{
	static LocalizationManager? current = null;
	public static LocalizationManager? Current => current ??= IPlatformApplication.Current!.Services.GetService<LocalizationManager>();

	static Dictionary<Type, IStringLocalizer?> localizers = new();
	static bool initialized = false;

	public static MauiAppBuilder AddLocalization(MauiAppBuilder builder, Type stringType)
	{
		if (!initialized)
		{
			builder.Services.AddSingleton<LocalizationManager>();
			builder.Services.AddLocalization();
			initialized = true;
		}

		if (!localizers.ContainsKey(stringType))
		{
			localizers[stringType] = null;
		}
		return builder;
	}

	string installedUICulturedName = CultureInfo.InstalledUICulture.Name;
	public CultureInfo InstalledUICulture => CultureInfo.InstalledUICulture;

	string currentUICultureName = CultureInfo.CurrentUICulture.Name;

	public CultureInfo CurrentUICulture
	{
		get => CultureInfo.CurrentUICulture;
		set => SetCulture(value);
	}

	[RelayCommand]
	public void SetCulture(CultureInfo culture)
	{
		CultureInfo.CurrentUICulture = culture;
		Poll();
	}

	public string this[string key] => GetLocalizedString(key);

	IDispatcherTimer timer;

	public LocalizationManager()
	{
		timer = Application.Current!.Dispatcher.CreateTimer();
		timer.Interval = TimeSpan.FromSeconds(1);
		timer.Tick += (s, e) => Poll();
		timer.Start();
	}

	public void Poll()
	{
		CultureInfo.InstalledUICulture.ClearCachedData();
		if (CultureInfo.InstalledUICulture.Name != installedUICulturedName)
		{
			installedUICulturedName = CultureInfo.InstalledUICulture.Name;
			OnPropertyChanged(nameof(InstalledUICulture));
			//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InstalledUICulture)));
			CultureInfo.CurrentUICulture = CultureInfo.InstalledUICulture;
		}

		CultureInfo.CurrentUICulture.ClearCachedData();
		if (CultureInfo.CurrentUICulture.Name != currentUICultureName)
		{
			currentUICultureName = CultureInfo.CurrentUICulture.Name;
			OnPropertyChanged(nameof(CurrentUICulture));
			//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUICulture)));
			OnPropertyChanged("Item");
			//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
		}
	}

	public string GetLocalizedString(string key, params object[] args)
	{
		foreach (Type stringType in localizers.Keys)
		{
			IStringLocalizer? localizer = localizers[stringType];
			if (localizer is null)
			{
				Type stringLocalizerType = typeof(IStringLocalizer<>).MakeGenericType(new Type[] { stringType });
				localizer = (IStringLocalizer?)IPlatformApplication.Current!.Services.GetService(stringLocalizerType);
				if (localizer is null)
				{
					localizers.Remove(stringType);
					continue;
				}
				localizers[stringType] = localizer;
			}

			string str = localizer.GetString(key).Value;
			if (str is not null)
			{
				if (args.Length > 0)
				{
					str = string.Format(str, args);
				}
				return str;
			}
		}

		return key;
	}

	public static BindingBase MakeBinding(object key, params object[] args)
	{
		Collection<BindingBase> bindings = new()
		{
			(key is BindingBase keyBinding) ? keyBinding : new Binding(".", source: key),
			new Binding(nameof(LocalizationManager.CurrentUICulture), source: LocalizationManager.Current)
		};

		foreach (object arg in args)
		{
			if (arg is BindingBase argBinding)
			{
				bindings.Add(argBinding);
			}
			else
			{
				bindings.Add(new Binding(".", source: arg));
			}
		}

		return new MultiBinding()
		{
			Bindings = bindings,
			Converter = new LocalizeConverter()
		};
	}

	public void SetLocalize(BindableObject bindableObject, BindableProperty property, string key, params object[] args)
		=> bindableObject.SetBinding(property, MakeBinding(key, args));

	public BindingBase GetLocalizedStringBinding(string key)
	{
		return new Binding($"[{key}]", BindingMode.OneWay, null, null, null, this);
	}
}
