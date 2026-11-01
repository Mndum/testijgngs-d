using System.Text;
using UnityEngine;

public class GameStatRandomPercentage : IGameStat
{
	public readonly float DefaultValue;

	public readonly float DefaultMultiply;

	public float Value;

	public float Multiply;

	public GameStatRandomPercentage(float defaultValue, float defaultMultiply = 1f)
	{
		DefaultValue = defaultValue;
		DefaultMultiply = defaultMultiply;
		ResetStat();
	}

	public void ResetStat()
	{
		Value = DefaultValue;
		Multiply = DefaultMultiply;
	}

	public float Calculate(float add = 0f, float multi = 1f)
	{
		return (Value + add) * Multiply * multi;
	}

	public bool Check(float add = 0f, float multi = 1f)
	{
		return Random.value < Calculate(add, multi);
	}

	public override string ToString()
	{
		bool flag = !Mathf.Approximately(Multiply, 1f);
		bool flag2 = !Mathf.Approximately(Value, 0f);
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			if (flag)
			{
				stringBuilder.Append("(");
			}
			if (flag2)
			{
				stringBuilder.AppendFormat($"X+{Value:P}");
			}
			else
			{
				stringBuilder.Append("X");
			}
			if (flag)
			{
				stringBuilder.AppendFormat(")*({0:P})", Multiply);
			}
			bool flag3 = Mathf.Approximately(DefaultMultiply, Multiply);
			bool flag4 = Mathf.Approximately(DefaultValue, Value);
			if (flag4 & flag3)
			{
				stringBuilder.Append(" ALL DEFAULT");
			}
			else if (flag3)
			{
				stringBuilder.Append(" M DEFAULT");
			}
			else if (flag4)
			{
				stringBuilder.Append(" V DEFAULT");
			}
			return stringBuilder.ToString();
		}
	}
}
