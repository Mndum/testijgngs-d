using UnityEngine;

[CreateAssetMenu(fileName = "SoapData", menuName = "MySO/SoapData", order = 1)]
public class SoapData : ScriptableObject
{
	[SerializeField]
	public SoapParticleData[] LiquidParticles;

	[SerializeField]
	public SoapParticleData[] SpreadParticles;

	[SerializeField]
	public SoapParticleData[] LiftedParticles;

	[SerializeField]
	public SoapParticleData[] BubblesParticles;

	private Lottery<SoapParticleData> _liquidLottery;

	private Lottery<SoapParticleData> _spreadLottery;

	private Lottery<SoapParticleData> _liftedLottery;

	private Lottery<SoapParticleData> _bubblesLottery;

	public SoapParticleData GetParticleData(int seed, SoapParticleType soapParticleType)
	{
		return soapParticleType switch
		{
			SoapParticleType.Liquid => _liquidLottery.GetRandom(seed), 
			SoapParticleType.Lifted => _liftedLottery.GetRandom(seed), 
			SoapParticleType.Bubble => _bubblesLottery.GetRandom(seed), 
			_ => _spreadLottery.GetRandom(seed), 
		};
	}

	public void OnEnable()
	{
		_liquidLottery = new Lottery<SoapParticleData>();
		_liquidLottery.AddItems(LiquidParticles, (SoapParticleData x) => x.Odds);
		_spreadLottery = new Lottery<SoapParticleData>();
		_spreadLottery.AddItems(SpreadParticles, (SoapParticleData x) => x.Odds);
		_liftedLottery = new Lottery<SoapParticleData>();
		_liftedLottery.AddItems(LiftedParticles, (SoapParticleData x) => x.Odds);
		_bubblesLottery = new Lottery<SoapParticleData>();
		_bubblesLottery.AddItems(BubblesParticles, (SoapParticleData x) => x.Odds);
	}
}
