using UnityEngine;

public class SoapScript : MonoBehaviour
{
	[SerializeField]
	private SoapData soapData;

	private const float bubbleRotationAmplitude = 2f;

	private SpriteRenderer _spriteRenderer;

	private SoapParticleData _currentParticleData;

	private SoapParticleType _soapParticleType;

	private Live2DController.Part _connectedPart;

	private Live2DRaycaster.HitTriangleData _hitTriangleData;

	private float _soapLife;

	private int _seed;

	private float _startRotationZ;

	private Vector3 _startScale;

	private float _rotationSpeed;

	private float _rotationSpeedOffset;

	private float _bubbleLifetime;

	public const float AllSoapStrength = 155f;

	public const float LiquidSoapStrength = 80f;

	public const float SpreadSoapLife = 75f;

	public const float MinSpreadableSoapLife = 1f;

	private static Color bubbleColor = new Color32(247, 242, 247, 255);

	public bool IsKilled { get; private set; }

	public SoapItemData SoapItemData { get; private set; }

	public SoapParticleType SoapParticleType => _soapParticleType;

	public Live2DController.Part ConnectedPart => _connectedPart;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		if (_spriteRenderer == null)
		{
			Debug.LogError("SoapScript requires a SpriteRenderer component!");
		}
	}

	public void TransformToLifted()
	{
		_soapParticleType = SoapParticleType.Lifted;
		UpdateParticleState();
		UpdateGraphics();
	}

	public void UpdateParticleState()
	{
		if (_soapParticleType == SoapParticleType.Liquid && _soapLife <= 75f)
		{
			_soapParticleType = SoapParticleType.Spread;
		}
		_currentParticleData = soapData.GetParticleData(_seed, _soapParticleType);
	}

	private void SetLocalZ(float localZ)
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.z = localZ;
		transform.localPosition = localPosition;
	}

	private void SetRotationZ(float rotationZ)
	{
		_startRotationZ = rotationZ;
		transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ);
	}

	private void SwitchConnectedPart(Live2DController.Part connectedPart, Live2DRaycaster.HitTriangleData hitTriangleData)
	{
		_connectedPart = connectedPart;
		_hitTriangleData = hitTriangleData;
		transform.SetParent(connectedPart.CubismDrawable.transform, worldPositionStays: true);
	}

	public void InitSoap(Live2DController.Part connectedPart, Live2DRaycaster.HitTriangleData hitTriangleData, SoapParticleType soapParticleType, float targetLife, SoapItemData soapItemData)
	{
		SoapItemData = soapItemData;
		SwitchConnectedPart(connectedPart, hitTriangleData);
		_seed = Random.Range(0, 1000000);
		_soapParticleType = soapParticleType;
		_soapLife = targetLife;
		UpdateParticleState();
		if (soapParticleType == SoapParticleType.Bubble)
		{
			transform.SetGlobalScale(Random.Range(0.5f, 1.05f).ToVector3());
			SetRotationZ(0f);
		}
		else
		{
			transform.SetGlobalScale(Random.Range(0.9f, 1.05f).ToVector3());
			SetRotationZ(Random.Range(0, 360));
		}
		_startScale = transform.localScale;
		_rotationSpeedOffset = Random.Range(0f, 3f);
		_rotationSpeed = Random.Range(2f, 2.2f);
		_bubbleLifetime = Random.Range(1f, 4f);
		UpdateGraphics();
	}

	public void MoveSoap(Live2DController.Part connectedPart, Live2DRaycaster.HitTriangleData hitTriangleData, Vector2 movement)
	{
		SwitchConnectedPart(connectedPart, hitTriangleData);
		if (_soapParticleType == SoapParticleType.Lifted)
		{
			_soapParticleType = SoapParticleType.Spread;
			UpdateParticleState();
		}
		if (_soapParticleType == SoapParticleType.Bubble)
		{
			_bubbleLifetime = Mathf.Max(_bubbleLifetime - movement.sqrMagnitude, 0.5f);
		}
		if (_currentParticleData.HasDirection())
		{
			float rotationZ = _startRotationZ * 0.5f + Quaternion.FromToRotation(_currentParticleData.MovementDirection, movement).eulerAngles.z * 0.5f;
			SetRotationZ(rotationZ);
		}
		UpdateGraphics();
	}

	public float GetSoapLife()
	{
		return _soapLife;
	}

	public void ExtractSoapLife(ref float goal)
	{
		float num = Mathf.Min(Mathf.Max(_soapLife - 1f, 0f), goal);
		goal -= num;
		_soapLife -= num;
		if (num > 0f)
		{
			UpdateParticleState();
			UpdateGraphics();
		}
	}

	public void UpdateManual()
	{
		Vector3 position = _hitTriangleData.GetPosition(_connectedPart.Mesh.vertices);
		transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
		switch (_soapParticleType)
		{
		case SoapParticleType.Spread:
		{
			float num3 = Mathf.Sin(_rotationSpeed * Time.time + _rotationSpeedOffset) * 2f;
			transform.localRotation = Quaternion.Euler(0f, 0f, _startRotationZ + num3);
			break;
		}
		case SoapParticleType.Bubble:
		{
			if (IsKilled)
			{
				break;
			}
			float num = Mathf.Sin(_rotationSpeed * Time.time);
			float num2 = Mathf.Cos(_rotationSpeed * Time.time);
			transform.localScale = new Vector3(_startScale.x * (0.9f + num * 0.1f), _startScale.y * (0.9f + num2 * 0.1f), _startScale.z);
			if (_soapLife <= 0f)
			{
				R.Sound.Shower_Bubble_All.PlayRandomSlightlyRandomVolumeAndPitch();
				transform.localScale = Vector3.zero;
				IsKilled = true;
			}
			else if (_soapLife <= 0.03f)
			{
				float value = 1f - _soapLife / 0.03f;
				value = EasingFunction.EaseInElastic(0f, 1f, value);
				transform.localScale = Vector3.LerpUnclamped(transform.localScale, Vector3.zero, value);
				if ((double)value > 0.5)
				{
					_spriteRenderer.color = _spriteRenderer.color.SetAlpha(value);
				}
				_soapLife -= Time.deltaTime * 0.03f / 0.05f;
			}
			else
			{
				_soapLife -= Time.deltaTime / _bubbleLifetime;
				if (_soapLife < 0.03f)
				{
					_soapLife = 0.03f;
				}
			}
			break;
		}
		case SoapParticleType.Liquid:
		case SoapParticleType.Lifted:
			break;
		}
	}

	private void UpdateGraphics()
	{
		_spriteRenderer.sprite = _currentParticleData.Sprite;
		switch (_soapParticleType)
		{
		case SoapParticleType.Liquid:
		{
			float t2 = 1f - (_soapLife - 75f) / 80f;
			_spriteRenderer.color = Color.Lerp(SoapItemData.SoapColor, bubbleColor, t2);
			transform.localScale = _startScale * Mathf.Lerp(1f, 0.75f, t2);
			SetLocalZ(Mathf.Lerp(-2E-05f, -4E-05f, t2));
			break;
		}
		case SoapParticleType.Spread:
		{
			float t = 1f - _soapLife / 75f;
			_spriteRenderer.color = bubbleColor;
			transform.localScale = _startScale * Mathf.Lerp(1f, 0.75f, t);
			SetLocalZ(Mathf.Lerp(-5E-05f, -6E-05f, t));
			break;
		}
		case SoapParticleType.Lifted:
			_spriteRenderer.color = bubbleColor;
			SetLocalZ(-6E-05f);
			break;
		case SoapParticleType.Bubble:
			_spriteRenderer.color = bubbleColor;
			SetLocalZ(-6E-05f);
			break;
		}
	}
}
