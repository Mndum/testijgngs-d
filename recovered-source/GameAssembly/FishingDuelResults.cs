public class FishingDuelResults
{
	public int PlayerScore { get; private set; }

	public int EnemyScore { get; private set; }

	public void AddPlayerScore(int score)
	{
		PlayerScore += score;
	}

	public void AddEnemyScore(int score)
	{
		EnemyScore += score;
	}

	public bool IsPlayerWinning()
	{
		return PlayerScore > EnemyScore;
	}

	public float GetCurrentRatio()
	{
		int num = PlayerScore + EnemyScore;
		if (num <= 0)
		{
			return 0.5f;
		}
		return (float)PlayerScore / (float)num;
	}
}
