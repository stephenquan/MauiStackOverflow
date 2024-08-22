namespace Maui.StackOverflow;

public class Sample
{
	public Type? Page { get; }
	public string Name { get; }
	public string Description { get; }

	public Sample(Type page, string name, string description)
	{
		Page = page;
		Name = name;
		Description = description;
	}
}

public static class Samples
{
	public static List<Sample> AllSamples { get; } = new List<Sample>();

	public static MauiAppBuilder RegisterSample(this MauiAppBuilder builder, Type page, string name, string description)
	{
		AllSamples.Add(new Sample(page, name, description));
		builder.Services.AddTransient(page);
		return builder;
	}

	public static MauiAppBuilder RegisterSample<T>(this MauiAppBuilder builder, string name, string description)
	{
		return builder.RegisterSample(typeof(T), name, description);
	}
}

