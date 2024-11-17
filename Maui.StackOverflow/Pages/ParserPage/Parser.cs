using System.Text.RegularExpressions;

namespace Maui.StackOverflow;

public class Parser
{
	public string Expr { get; internal set; } = string.Empty;
	public int ExprIndex { get; internal set; } = 0;
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
		Expr = expr;
		ExprIndex = 0;
		Results.Clear();
		if (!ParseExpr() || ExprIndex < Expr.Length)
		{
			throw new Exception("Invalid expression");
		}
		return Results.Pop();
	}
	bool ParseExpr() => ParseSum();
	bool ParseSum() => ParseBinaryOp(@"^\s*(\+|\-)\s*", ParseProduct);
	bool ParseProduct() => ParseBinaryOp(@"^\s*(\*|\/)\s*", ParsePrimary);
	bool ParseBinaryOp(string pattern, Func<bool> parseNextPriority)
	{
		if (!parseNextPriority())
		{
			return false;
		}
		while (ParsePattern(pattern))
		{
			string op = LastMatch!.Groups[1].Value;
			if (!parseNextPriority())
			{
				return false;
			}
			var b = Results.Pop();
			var a = Results.Pop();
			Results.Push(Operators[op](a, b));
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
			if (!ParseExpr())
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
		LastMatch = regex.Match(Expr.Substring(ExprIndex));
		if (!LastMatch!.Success)
		{
			return false;
		}
		ExprIndex += LastMatch.Groups[0].Length;
		return true;
	}
}
