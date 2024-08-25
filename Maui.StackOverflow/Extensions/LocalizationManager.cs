using System.ComponentModel;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Maui.StackOverflow;

public static class LocalizationManagerExtensions
{
	public static MauiAppBuilder AddLocalization(this MauiAppBuilder builder, Type stringType)
		=> LocalizationManager.AddLocalization(builder, stringType);
	public static MauiAppBuilder AddLocalization<T>(this MauiAppBuilder builder)
		=> LocalizationManager.AddLocalization(builder, typeof(T));

	public static void SetLocalizedString(this BindableObject bindable, BindableProperty property, string key)
	{
		bindable.SetBinding(property, LocalizationManager.Current!.GetLocalizedStringBinding(key));
	}
}

public class LocalizationManager : INotifyPropertyChanged
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

	public CultureInfo InstalledUICulture => CultureInfo.InstalledUICulture;
	public CultureInfo CurrentUICulture
	{
		get => CultureInfo.CurrentUICulture;
		set
		{
			CultureInfo.CurrentUICulture = value;
			Poll();
		}
	}

	public string this[string key] => GetLocalizedString(key);

	string installedUICulturedName = CultureInfo.InstalledUICulture.Name;
	string currentUICultureName = CultureInfo.CurrentUICulture.Name;

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
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InstalledUICulture)));
			CultureInfo.CurrentUICulture = CultureInfo.InstalledUICulture;
		}

		CultureInfo.CurrentUICulture.ClearCachedData();
		if (CultureInfo.CurrentUICulture.Name != currentUICultureName)
		{
			currentUICultureName = CultureInfo.CurrentUICulture.Name;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUICulture)));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
		}
	}

	public string GetLocalizedString(string key)
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
					return key;
				}
				localizers[stringType] = localizer;
			}
			return localizer?.GetString(key)?.Value ?? key;
		}

		return key;
	}

	public BindingBase GetLocalizedStringBinding(string key)
	{
		return new Binding($"[{key}]", BindingMode.OneWay, null, null, null, this);
	}

	public event PropertyChangedEventHandler? PropertyChanged;
}
