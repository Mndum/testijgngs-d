using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NewEmail : EmailBase, ISerializedPart<string, EmailPrefabManager.EmailPrefab>, IKey<string>, IWithPrefab<EmailPrefabManager.EmailPrefab>
{
	public static char Prefix = 'N';

	public string emailId;

	public List<string> specialValues;

	private List<object> _preParsedSpecialValues;

	public const string SenderPostFix = "_Sender";

	public const string TopicPostFix = "_Topic";

	public const string TextPostFix = "_Text";

	public List<object> PreParsedSpecialValues => _preParsedSpecialValues ?? (_preParsedSpecialValues = LOS.PreparseSerializedStringArgumentsForLocalization(specialValues));

	public override string Sender => GetString(Prefab.SenderTextOverride, GetSenderKey());

	public override string Topic => GetString(Prefab.TopicTextOverride, GetTopicKey());

	public override string Text => GetString(Prefab.MainTextOverride, GetTextKey());

	public string Key => emailId;

	public EmailPrefabManager.EmailPrefab Prefab { get; private set; }

	public bool IsSpam => Prefab.IsSpam;

	public NewEmail(string emailId, List<string> specialValues = null)
	{
		this.emailId = emailId;
		this.specialValues = specialValues ?? new List<string>();
	}

	private string GetString(EmailPrefabManager.EmailPrefab.TextOverride textOverride, string key)
	{
		if (textOverride != null)
		{
			return textOverride(key, this);
		}
		return LOC.Emails.GS(key, PreParsedSpecialValues);
	}

	public string GetSenderKey()
	{
		return emailId + "_Sender";
	}

	public string GetTopicKey()
	{
		return emailId + "_Topic";
	}

	public string GetTextKey()
	{
		return emailId + "_Text";
	}

	protected override void OnEmailRead()
	{
		Prefab.OnReadEvent?.Start(this);
	}

	public override string Serialize()
	{
		return Prefix + JsonUtility.ToJson(this);
	}

	public NewEmail Clone()
	{
		return EmailPrefabManager.GetEmail(emailId, specialValues);
	}

	public void InitPrefab(EmailPrefabManager.EmailPrefab prefab)
	{
		Prefab = prefab;
	}
}
