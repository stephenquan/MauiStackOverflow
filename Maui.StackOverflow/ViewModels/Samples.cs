namespace Maui.StackOverflow;

public class Sample
{
	public string Name { get; }
	public Type? Page { get; }

	public Sample(string name, Type page)
	{
		Name = name;
		Page = page;
	}
}

public static class Samples
{
	public static List<Sample> Items { get; } = new List<Sample>();

	public static MauiAppBuilder RegisterSample(this MauiAppBuilder builder, string name, Type page)
	{
		Items.Add(new Sample(name, page));
		builder.Services.AddTransient(page);
		return builder;
	}

	public static MauiAppBuilder RegisterSample<T>(this MauiAppBuilder builder, string name)
	{
		return builder.RegisterSample(name, typeof(T));
	}
}

