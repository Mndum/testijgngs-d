using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class AsciiRenderingUtils
{
	private static string[] _gradients = new string[7] { ".+vO$", "`.-':_,^=;><+!rc*/z?sLTv)J7(|Fi{C}fI31tlu[neoZ5Yxjya]2ESwqkP6h9d4VpOGbUAKXHm8RD#$Bg0MNWQ%&@", "10398", "junJUN", "01", "+.x.X", "xdXD" };

	public static int GradientCount => _gradients.Length;

	public static void NextGradient(ref int gradientIndex)
	{
		gradientIndex = (gradientIndex + 1) % _gradients.Length;
	}

	public static string GetGradient(ref int index)
	{
		if (index < 0)
		{
			index = 0;
		}
		if (index > _gradients.Length - 1)
		{
			index %= _gradients.Length;
		}
		return _gradients[index];
	}

	public static async UniTask<(bool, string)> GetCustomGradient(BotStatusAppPopup popup, CancellationToken cancellationToken)
	{
		BotStatusAppPopup.StringConsoleLine inputLine = popup.GetStringConsoleLine(LOC.UI.CubeCommandInputGradient(_gradients.GetRandom()));
		popup.PrintLine(inputLine);
		popup.UpdateDisplay();
		string text = await popup.GetPlayerInput(saveInMemory: true, cancellationToken);
		if (!string.IsNullOrWhiteSpace(text))
		{
			popup.RemoveLine(inputLine);
			return (true, text);
		}
		popup.RemoveLine(inputLine);
		return (false, null);
	}

	public static char GetShadeCharacter(string currentGradient, float brightness)
	{
		int value = Mathf.FloorToInt(brightness * (float)currentGradient.Length);
		value = Mathf.Clamp(value, 0, currentGradient.Length - 1);
		return currentGradient[value];
	}
}
