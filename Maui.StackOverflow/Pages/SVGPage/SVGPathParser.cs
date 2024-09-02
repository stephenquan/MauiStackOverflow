using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Maui.StackOverflow;

public partial class SVGPathParser
{
	public string Path { get; private set; } = string.Empty;
	public int PathIndex { get; private set; } = 0;
	public Match? LastMatch { get; private set; } = null;

	[GeneratedRegex(@"^\s*([MmLlHhVvZzCcSsAa])\s*")]
	public static partial Regex CommandRegex();

	[GeneratedRegex(@"^\s*([-+]?\d*\.?\d+)\s*")]
	public static partial Regex NumberRegex();

	public Dictionary<string, int> CommandArgs = new()
	{
		{ "M", 2 },
		{ "L", 2 },
		{ "Z", 0 },
		{ "C", 6 },
		{ "S", 4 },
		{ "A", 7 },
		{ "H", 1 },
		{ "V", 1 },
	};

	public string Command { get; private set; } = string.Empty;
	public int ArgCount { get; private set; } = 0;
	public double[] Numbers { get; } = new double[10];

	public bool Parse(string path)
	{
		this.Path = path;
		this.PathIndex = 0;

		while (ParseCommand())
		{
			continue;
		}

		Debug.WriteLine($"Remainder: {this.Path.Substring(this.PathIndex)}");

		return false;
	}

	public bool ParseCommand()
	{
		if (!ParsePattern(CommandRegex()))
		{
			return false;
		}

		Command = LastMatch!.Groups[1].Value;
		string commandUpper = Command.ToUpper();
		if (!CommandArgs.ContainsKey(commandUpper))
		{
			return false;
		}

		ArgCount = CommandArgs[commandUpper];
		if (ArgCount == 0)
		{
			Debug.WriteLine($"{Command} ");
			return true;
		}

		while (ParsePattern(NumberRegex()))
		{
			string number = LastMatch!.Groups[1].Value;
			Numbers[0] = double.Parse(number);

			for (int i = 1; i < ArgCount; i++)
			{
				if (!ParsePattern(NumberRegex()))
				{
					Debug.WriteLine($"Error: Expected {ArgCount} arguments for command {Command}");
					return false;
				}

				number = LastMatch!.Groups[1].Value;
				Numbers[i] = double.Parse(number);
			}

			Debug.Write($"{Command} ");
			for (int i = 0; i < ArgCount; i++)
			{
				Debug.Write($"{Numbers[i]} ");
			}
			Debug.WriteLine("");
		}

		return true;
	}

	public bool ParsePattern(Regex pattern)
	{
		LastMatch = pattern.Match(this.Path.Substring(this.PathIndex));
		if (!LastMatch.Success)
		{
			return false;
		}

		this.PathIndex += LastMatch.Length;

		return true;
	}
}
