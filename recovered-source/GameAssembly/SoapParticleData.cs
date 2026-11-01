using System;
using EditorAttributes;
using UnityEngine;

[Serializable]
public class SoapParticleData
{
	[AssetPreview(0f, 0f)]
	public Sprite Sprite;

	[Validate("Odds need to be a positive number!", "InvalidOdds", MessageMode.Error, true)]
	public float Odds = 1f;

	[MessageBox("No direction provided", "HasNoDirection", MessageMode.None, StringInputMode.Constant, false)]
	public Vector2 MovementDirection;

	private bool InvalidOdds()
	{
		return Odds <= 0f;
	}

	public bool HasDirection()
	{
		return MovementDirection != Vector2.zero;
	}

	public bool HasNoDirection()
	{
		return !HasDirection();
	}
}
