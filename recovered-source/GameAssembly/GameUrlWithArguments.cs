using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class GameUrlWithArguments : GameUrl
{
	public static readonly char EscapeCharacter = '%';

	public static readonly char ArgumentsStart = '?';

	public static readonly char ArgumentsSplit = '&';

	public static readonly string ArgumentsSplitEscaped = EscapeCharacter.ToString() + ArgumentsSplit;

	public static readonly char ArgumentKeyValueSplit = '=';

	public static readonly string ArgumentKeyValueSplitEscaped = EscapeCharacter.ToString() + ArgumentKeyValueSplit;

	public static readonly Regex ArgumentsSplitRegex = new Regex("(?<!" + EscapeCharacter + ")" + ArgumentsSplit);

	public static readonly Regex ArgumentsKeyValueSplitRegex = new Regex("(?<!" + EscapeCharacter + ")" + ArgumentKeyValueSplit);

	public Dictionary<string, string> Arguments { get; set; }

	public string AssembledWithArguments
	{
		get
		{
			if (Arguments.Count == 0)
			{
				return Assembled;
			}
			string assembled = Assembled;
			char argumentsStart = ArgumentsStart;
			string text = argumentsStart.ToString();
			argumentsStart = ArgumentsSplit;
			return assembled + text + string.Join(argumentsStart.ToString(), Arguments.Select((KeyValuePair<string, string> x) =>
			{
				string text2 = EscapeKeyValueSplit(x.Key);
				char argumentKeyValueSplit = ArgumentKeyValueSplit;
				return EscapeArgumentsSplit(text2 + argumentKeyValueSplit + EscapeKeyValueSplit(x.Value));
			}));
		}
	}

	public static string EscapeArgumentsSplit(string str)
	{
		char argumentsSplit = ArgumentsSplit;
		return str.Replace(argumentsSplit.ToString(), ArgumentsSplitEscaped);
	}

	public static string UnescapeArgumentsSplit(string str)
	{
		string argumentsSplitEscaped = ArgumentsSplitEscaped;
		char argumentsSplit = ArgumentsSplit;
		return str.Replace(argumentsSplitEscaped, argumentsSplit.ToString());
	}

	public static string EscapeKeyValueSplit(string str)
	{
		char argumentKeyValueSplit = ArgumentKeyValueSplit;
		return str.Replace(argumentKeyValueSplit.ToString(), ArgumentKeyValueSplitEscaped);
	}

	public static string UnescapeKeyValueSplit(string str)
	{
		string argumentKeyValueSplitEscaped = ArgumentKeyValueSplitEscaped;
		char argumentKeyValueSplit = ArgumentKeyValueSplit;
		return str.Replace(argumentKeyValueSplitEscaped, argumentKeyValueSplit.ToString());
	}

	public GameUrlWithArguments(string str)
	{
		Arguments = new Dictionary<string, string>();
		int num = str.IndexOf(ArgumentsStart);
		if (num == -1)
		{
			FromString(str);
			return;
		}
		FromString(str.Substring(0, num));
		if (num + 1 < str.Length)
		{
			ParseArguments(str.Substring(num + 1));
		}
	}

	public GameUrlWithArguments(string strWithoutArguments, Dictionary<string, string> arguments)
	{
		Arguments = arguments;
		FromString(strWithoutArguments);
	}

	private void ParseArguments(string arguments)
	{
		string[] array = ArgumentsSplitRegex.Split(arguments);
		foreach (string str in array)
		{
			string[] array2 = ArgumentsKeyValueSplitRegex.Split(UnescapeArgumentsSplit(str));
			if (array2.Length == 2)
			{
				string key = UnescapeKeyValueSplit(array2[0]);
				string value = UnescapeKeyValueSplit(array2[1]);
				Arguments[key] = value;
			}
		}
	}
}
