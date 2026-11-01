using System;
using EditorAttributes;
using UnityEngine;

public sealed class ParticleSystemGroup : MonoBehaviour
{
	[SerializeField]
	[EditorAttributes.ReadOnly]
	private ParticleSystem[] _allSystems;

	private void Reset()
	{
		CacheSystems();
	}

	private void OnValidate()
	{
		CacheSystems();
	}

	private void Awake()
	{
		if (_allSystems == null || _allSystems.Length == 0)
		{
			CacheSystems();
		}
	}

	private void CacheSystems()
	{
		_allSystems = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
	}

	public void ApplyToAll(Action<ParticleSystem> action)
	{
		ParticleSystem[] allSystems = _allSystems;
		foreach (ParticleSystem obj in allSystems)
		{
			action(obj);
		}
	}

	public void DisableEmission()
	{
		ApplyToAll((ParticleSystem x) =>
		{
			x.DisableEmission();
		});
	}

	public void EmitIndexWithDirection(int index, int count, Vector3 directionNormalized, float angleRandomOffset)
	{
		if (_allSystems.Length < index + 1)
		{
			return;
		}
		ParticleSystem particleSystem = _allSystems[index];
		if (count > 0)
		{
			ParticleSystem.MainModule main = particleSystem.main;
			ParticleSystem.EmitParams emitParams = default;
			for (int i = 0; i < count; i++)
			{
				Vector3 vector = Quaternion.Euler(0f, UnityEngine.Random.Range(0f - angleRandomOffset, angleRandomOffset), 0f) * directionNormalized;
				emitParams.startLifetime = main.startLifetime.Evaluate(UnityEngine.Random.value);
				emitParams.velocity = vector * main.startSpeed.Evaluate(UnityEngine.Random.value);
				emitParams.applyShapeToPosition = true;
				particleSystem.Emit(emitParams, 1);
			}
		}
	}

	public void EmitIndex(int index, int count)
	{
		if (_allSystems.Length >= index + 1)
		{
			_allSystems[index].Emit(count);
		}
	}

	public void EmitAll(int count)
	{
		for (int i = 0; i < _allSystems.Length; i++)
		{
			_allSystems[i].Emit(count);
		}
	}

	public void EmitFirstTwo(int count1, int count2)
	{
		if (_allSystems.Length >= 1)
		{
			_allSystems[0].Emit(count1);
		}
		if (_allSystems.Length >= 2)
		{
			_allSystems[1].Emit(count2);
		}
	}

	public void EmitFirstThree(int count1, int count2, int count3)
	{
		if (_allSystems.Length >= 1)
		{
			_allSystems[0].Emit(count1);
		}
		if (_allSystems.Length >= 2)
		{
			_allSystems[1].Emit(count2);
		}
		if (_allSystems.Length >= 3)
		{
			_allSystems[2].Emit(count3);
		}
	}

	public void EmitFirstN(int[] counts)
	{
		int num = Mathf.Min(counts.Length, _allSystems.Length);
		for (int i = 0; i < num; i++)
		{
			_allSystems[i].Emit(counts[i]);
		}
	}

	public ParticleSystem GetParticleSystem(int i)
	{
		return _allSystems[i];
	}
}
