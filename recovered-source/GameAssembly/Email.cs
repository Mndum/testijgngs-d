using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class Email : EmailBase
{
	public static char Prefix = 'O';

	public string sender;

	public string topic;

	[TextArea(10, 15)]
	public string text;

	private static Regex regex = new Regex("{.+?}");

	private static Regex cleanRegex = new Regex("[$&+,:;=?@#|'<>.^*()%!-]");

	public override string Sender => sender;

	public override string Topic => topic;

	public override string Text => text;

	public Email(string sender, string topic, string text)
	{
		this.sender = sender;
		this.topic = topic;
		this.text = text;
	}

	public Email Clone()
	{
		return new Email(string.Copy(sender), string.Copy(topic), string.Copy(text));
	}

	protected override void OnEmailRead()
	{
	}

	public override string Serialize()
	{
		return Prefix + JsonUtility.ToJson(this);
	}

	public bool TryConvertToNewEmail(List<KeyValuePair<string, Email>> oldEmails, out NewEmail newEmail)
	{
		int num = oldEmails.FindIndex((KeyValuePair<string, Email> oldEmail) => oldEmail.Value.sender == sender && oldEmail.Value.text == this.text && oldEmail.Value.topic == topic);
		if (num >= 0 && num < oldEmails.Count)
		{
			string key = oldEmails[num].Key;
			newEmail = EmailPrefabManager.GetEmail(key);
			newEmail.read = read;
			return true;
		}
		try
		{
			foreach (KeyValuePair<string, Email> oldEmail in oldEmails)
			{
				if (!oldEmail.Value.text.Contains('{') || !oldEmail.Value.text.Contains('}') || !(oldEmail.Value.sender == sender) || !(oldEmail.Value.topic == topic))
				{
					continue;
				}
				string input = cleanRegex.Replace(oldEmail.Value.text, "\\$&");
				Regex regex = new Regex(Email.regex.Replace(input, (Match t) => "(?<_" + t.Value.Trim('{', '}') + ">.+?)"));
				Match match = regex.Match(this.text);
				if (match.Success)
				{
					string[] groupNames = regex.GetGroupNames();
					Dictionary<int, string> dictionary = new Dictionary<int, string>();
					string[] array = groupNames;
					foreach (string text in array)
					{
						if (text.StartsWith("_"))
						{
							int key2 = int.Parse(text.Substring(1));
							Group obj = match.Groups[text];
							dictionary[key2] = obj.Value;
						}
					}
					int num3 = dictionary.Keys.Max();
					List<string> list = new List<string>();
					for (int num4 = 0; num4 <= num3; num4++)
					{
						if (dictionary.TryGetValue(num4, out var value))
						{
							list.Add(value);
						}
						else
						{
							list.Add("0");
						}
					}
					newEmail = EmailPrefabManager.GetEmail(oldEmail.Key, list);
					newEmail.read = read;
					return true;
				}
				Debug.LogWarning("Failed matching regex email \"" + topic + "\" from \"" + sender + "\"");
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		if (onReadEventHolder != null && !read)
		{
			newEmail = EmailPrefabManager.GetEmail(EmailPrefabManager.ErrorEmail);
			return true;
		}
		newEmail = null;
		return false;
	}
}
