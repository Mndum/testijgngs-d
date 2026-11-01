using Coffee.UIExtensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class ParticleSystemUtilities
{
	public static void MimicBursts(this UIParticle particle)
	{
		foreach (ParticleSystem particle2 in particle.particles)
		{
			particle2.MimicBursts();
		}
	}

	public static void DisableEmission(this ParticleSystem particleSystem)
	{
		ParticleSystem.EmissionModule emission = particleSystem.emission;
		emission.enabled = false;
	}

	public static void MimicBursts(this ParticleSystem particleSystem)
	{
		ParticleSystem.EmissionModule emission = particleSystem.emission;
		int burstCount = emission.burstCount;
		for (int i = 0; i < burstCount; i++)
		{
			ParticleSystem.Burst burst = emission.GetBurst(i);
			if (IsInstant(burst))
			{
				TriggerBurstInstant(particleSystem, burst);
			}
			else
			{
				TriggerBurstAsync(particleSystem, burst).Forget();
			}
		}
	}

	private static bool IsInstant(ParticleSystem.Burst burst)
	{
		if (burst.time <= 0.001f)
		{
			return burst.cycleCount <= 1;
		}
		return false;
	}

	private static void TriggerBurstInstant(ParticleSystem particleSystem, ParticleSystem.Burst burst)
	{
		int count = (int)burst.count.Evaluate(Random.value);
		particleSystem.Emit(count);
	}

	private static async UniTaskVoid TriggerBurstAsync(ParticleSystem particleSystem, ParticleSystem.Burst burst)
	{
		await UniTask.Delay((int)(burst.time * 1000f));
		for (int i = 0; i < burst.cycleCount; i++)
		{
			if (burst.probability >= 1f || Random.value <= burst.probability)
			{
				int count = (int)burst.count.Evaluate(Random.value);
				particleSystem.Emit(count);
			}
			if (burst.repeatInterval > 0f && i < burst.cycleCount - 1)
			{
				await UniTask.Delay((int)(burst.repeatInterval * 1000f));
			}
		}
	}
}
