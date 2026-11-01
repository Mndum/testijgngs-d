using System;
using System.Text;

public class DUPADUPADUPADUPADUPA
{
	public const string x = "-/GuXCenAMjpYA+KJM-vR6-0[Z*Yf;km83Z7.yWL)by!PN@MidxGDfJ-1])HE?rcg{HTmqtmn4]AcLE%FZ.B4n@-7NeemvkAg0[D";

	public static string A(string s, int o)
	{
		if (s.IndexOf('@') >= 0)
		{
			return string.Empty;
		}
		byte[] array = Convert.FromBase64String(s);
		int length = "-/GuXCenAMjpYA+KJM-vR6-0[Z*Yf;km83Z7.yWL)by!PN@MidxGDfJ-1])HE?rcg{HTmqtmn4]AcLE%FZ.B4n@-7NeemvkAg0[D".Length;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] ^= (byte)"-/GuXCenAMjpYA+KJM-vR6-0[Z*Yf;km83Z7.yWL)by!PN@MidxGDfJ-1])HE?rcg{HTmqtmn4]AcLE%FZ.B4n@-7NeemvkAg0[D"[(i + o) % length];
		}
		return Encoding.UTF8.GetString(array);
	}

	public static string B(string s, int o)
	{
		if (s.IndexOf('@') >= 0)
		{
			return string.Empty;
		}
		byte[] array = Convert.FromBase64String(s);
		Array.Reverse(array);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)((array[i] - o - i) & 0xFF);
		}
		return Encoding.UTF8.GetString(array);
	}
}
