using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public sealed class ItchIoSingleton : SingletonMonoBehaviour<ItchIoSingleton>
{
	[Serializable]
	public class ItchIoPageInfo
	{
		public string Username;

		public string GameId;

		public string GetItchGameLink()
		{
			return "https://" + Username + ".itch.io/" + GameId;
		}
	}

	[Serializable]
	public class ItchIoResponse
	{
		public string[] errors;

		public string latest;

		public bool IsValid()
		{
			if (!string.IsNullOrWhiteSpace(latest))
			{
				if (errors != null)
				{
					return errors.Length == 0;
				}
				return true;
			}
			return false;
		}
	}

	private ItchIoResponse _responseFromItchIoVersionRequest = new ItchIoResponse();

	private DateTime _lastSuccessfulResponseTime = DateTime.MinValue;

	private ManagedCoroutine _requestCoroutine = new ManagedCoroutine();

	[SerializeField]
	private SerializableDateTime _earlyAccessEndTimeUtc;

	[SerializeField]
	private bool isEarlyAccessBuild;

	[field: Header("Runtime")]
	[field: SerializeField]
	public ItchIoPageInfo MainPageInfo { get; private set; }

	[field: SerializeField]
	public ItchIoPageInfo TestingPageInfo { get; private set; }

	public DateTime EarlyAccessEndTime => _earlyAccessEndTimeUtc.Value;

	public event Action OnItchIoVersionResponse;

	public string GetCurrentChannel()
	{
		return GetChannel(Application.platform);
	}

	public string GetChannel(RuntimePlatform platform)
	{
		switch (platform)
		{
		default:
			return "win-64";
		case RuntimePlatform.LinuxPlayer:
		case RuntimePlatform.LinuxEditor:
			return "linux-64";
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
			return "mac-64";
		case RuntimePlatform.WebGLPlayer:
			return "html";
		case RuntimePlatform.Android:
			return "android";
		}
	}

	public string GetItchGameLink()
	{
		return MainPageInfo.GetItchGameLink();
	}

	public bool TryGetVersionFromItchIo(out string value)
	{
		if (_responseFromItchIoVersionRequest.IsValid())
		{
			value = _responseFromItchIoVersionRequest.latest;
			return true;
		}
		value = null;
		return false;
	}

	private IEnumerator UpdateLatestVersion(string channelName)
	{
		string url = "https://itch.io/api/1/x/wharf/latest?target=" + MainPageInfo.Username + "/" + MainPageInfo.GameId + "&channel_name=" + channelName;
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();
		_responseFromItchIoVersionRequest = new ItchIoResponse();
		if (www.result == UnityWebRequest.Result.Success)
		{
			_lastSuccessfulResponseTime = DateTime.Now;
			JsonUtility.FromJsonOverwrite(www.downloadHandler.text, _responseFromItchIoVersionRequest);
		}
		else
		{
			Debug.LogWarning("Failed to get version number from itch.io!!!\n" + url);
		}
		OnItchIoVersionResponse?.Invoke();
	}

	public void MakeSureGotLatestVersionFromItchIo()
	{
		if (!_requestCoroutine.Running && !((DateTime.Now - _lastSuccessfulResponseTime).TotalMinutes < 60.0) && Application.platform != RuntimePlatform.WebGLPlayer)
		{
			_requestCoroutine.Start(this, UpdateLatestVersion(GetCurrentChannel()));
		}
	}

	public void Start()
	{
		MakeSureGotLatestVersionFromItchIo();
	}

	public bool IsInPaidEarlyAccessPeriod()
	{
		if (DateTime.UtcNow < _earlyAccessEndTimeUtc)
		{
			return isEarlyAccessBuild;
		}
		return false;
	}
}
