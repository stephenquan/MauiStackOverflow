using System.Text.RegularExpressions;

namespace Maui.StackOverflow;

public class Parser
{
	public string Expression { get; internal set; } = string.Empty;
	public int ExpressionIndex { get; internal set; } = 0;
	public Match? LastMatch { get; internal set; } = null;
	public Stack<double> Results { get; } = new Stack<double>();
	public Dictionary<string, Func<double, double, double>> Operators { get; } = new Dictionary<string, Func<double, double, double>>()
	{
		{ "+", (a, b) => a + b },
		{ "-", (a, b) => a - b },
		{ "*", (a, b) => a * b },
		{ "/", (a, b) => a / b }
	};

	public double Execute(string expr)
	{
		Expression = expr;
		ExpressionIndex = 0;
		Results.Clear();
		if (!ParseExpression() || ExpressionIndex < Expression.Length)
		{
			throw new Exception("Invalid expression");
		}
		return Results.Pop();
	}
	bool ParseExpression() => ParseSum();
	bool ParseSum() => ParseBinaryOperator(@"^\s*(\+|\-)\s*", ParseProduct);
	bool ParseProduct() => ParseBinaryOperator(@"^\s*(\*|\/)\s*", ParsePrimary);
	bool ParseBinaryOperator(string pattern, Func<bool> parseNextPriority)
	{
		if (!parseNextPriority())
		{
			return false;
		}
		while (ParsePattern(pattern))
		{
			string @operator = LastMatch!.Groups[1].Value;
			if (!parseNextPriority())
			{
				return false;
			}
			var b = Results.Pop();
			var a = Results.Pop();
			Results.Push(Operators[@operator](a, b));
		}
		return true;
	}
	bool ParsePrimary()
	{
		if (ParsePattern(@"^\s*(\d+(\.\d+)?)\s*"))
		{
			Results.Push(double.Parse(LastMatch!.Groups[1].Value));
			return true;
		}
		if (ParsePattern(@"^\s*\(\s*"))
		{
			if (!ParseExpression())
			{
				return false;
			}
			if (!ParsePattern(@"^\s*\)\s*"))
			{
				return false;
			}
			return true;
		}
		return false;
	}
	bool ParsePattern(string pattern)
	{
		var regex = new Regex(pattern);
		LastMatch = regex.Match(Expression.Substring(ExpressionIndex));
		if (!LastMatch!.Success)
		{
			return false;
		}
		ExpressionIndex += LastMatch.Groups[0].Length;
		return true;
	}
}