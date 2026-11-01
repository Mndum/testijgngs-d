using System.Threading;
using Cysharp.Threading.Tasks;

public abstract class AppCommand
{
	public struct CommandFlagInfo(string flag, string description)
	{
		public string Flag = flag;

		public string Description = description;
	}

	public const char UniversalLeaveChar = 'l';

	public abstract string CommandName { get; }

	public abstract string CommandDescription { get; }

	public virtual string CommandDescriptionDetailed => null;

	public virtual bool IsListed => true;

	public virtual bool IsAllowed => true;

	public abstract UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken);

	public bool TryGetArgumentAtIndex(StringUtils.ParsedCommandData inputSplit, int index, out string a)
	{
		if (inputSplit.Arguments.Length > index)
		{
			a = inputSplit.Arguments[index];
			return true;
		}
		a = null;
		return false;
	}

	public bool TryGetArguments(StringUtils.ParsedCommandData inputSplit, out string a1, out string a2)
	{
		a2 = null;
		if (TryGetArgumentAtIndex(inputSplit, 0, out a1))
		{
			return TryGetArgumentAtIndex(inputSplit, 0, out a2);
		}
		return false;
	}

	public bool TryGetFirstArgument(StringUtils.ParsedCommandData inputSplit, out string a, bool useRawInput = false)
	{
		if (useRawInput && inputSplit.Arguments.Length >= 2 && !inputSplit.HasAnyFlags && !string.IsNullOrWhiteSpace(inputSplit.ArgumentsRaw))
		{
			a = inputSplit.ArgumentsRaw;
			return true;
		}
		if (inputSplit.Arguments.Length != 0)
		{
			a = inputSplit.Arguments[0];
			return true;
		}
		a = null;
		return false;
	}

	public void ThrowIfResultWithDescription(Result result)
	{
		if (result.IsFail && result.HasDescription)
		{
			throw new CommandsPrefabManager.AppCommandParsingException(result.GetDescription());
		}
	}

	public Result TryGetArgumentAtIndexAsNumber(StringUtils.ParsedCommandData inputSplit, int index, out int number, int min = -2147483648, int max = 2147483647)
	{
		number = 0;
		if (!TryGetArgumentAtIndex(inputSplit, index, out var a))
		{
			return Result.Fail;
		}
		if (!int.TryParse(a, out number))
		{
			return Result.FailWithDescription(LOC.UI.CommandNotAWholeNumber(a));
		}
		if (number < min || number >= max)
		{
			return Result.FailWithDescription(LOC.UI.CommandNotWithinRange(number, min, max - 1));
		}
		return Result.Success;
	}

	public void GetArgument(StringUtils.ParsedCommandData input, out string argument, bool useRawInput = false)
	{
		if (!TryGetFirstArgument(input, out argument, useRawInput))
		{
			throw new CommandsPrefabManager.AppCommandParsingException(LOC.UI.ProvidedArgumentsCountError(input.Arguments.Length) + "\n" + LOC.UI.CommandArgumentsExpectationInfo(CommandName, 1, CommandName + " arg1"));
		}
	}

	public void GetArguments(StringUtils.ParsedCommandData input, out string a1, out string a2)
	{
		if (!TryGetArguments(input, out a1, out a2))
		{
			throw new CommandsPrefabManager.AppCommandParsingException(LOC.UI.ProvidedArgumentsCountError(input.Arguments.Length) + "\n" + LOC.UI.CommandArgumentsExpectationInfo(CommandName, 2, CommandName + " arg1 arg2"));
		}
	}
}
