using System;
using System.Collections.Generic;
using System.Linq;

public static class CommandsPrefabManager
{
	public class AppCommandParsingException : Exception
	{
		public AppCommandParsingException(string message)
			: base(message)
		{
		}
	}

	private static List<AppCommand> _commands = new List<AppCommand>
	{
		new HelpCommand(),
		new InteractCommand(),
		new SummaryCommand(),
		new CowsayCommand(),
		new ClearScreenCommand(),
		new SnakeCommand(),
		new ViewValueCommand(),
		new DateCommand(),
		new CubeCommand(),
		new StyleCommand(),
		new EightBallCommand(),
		new FileCommand(),
		new VideoCommand(),
		new EquipmentCommand(),
		new ExitCommand()
	};

	public static IEnumerable<AppCommand> GetListedCommands()
	{
		return _commands.Where((AppCommand x) => x.IsListed);
	}

	public static bool TryGetCommand(string name, out AppCommand found)
	{
		foreach (AppCommand command in _commands)
		{
			if (string.Equals(name, command.CommandName, StringComparison.InvariantCultureIgnoreCase))
			{
				found = command;
				return true;
			}
		}
		found = null;
		return false;
	}
}
