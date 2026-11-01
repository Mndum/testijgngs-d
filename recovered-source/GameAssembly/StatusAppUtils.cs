using System;
using System.Text;

public class StatusAppUtils
{
	public static string Base64Encode(string input)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
	}
}
