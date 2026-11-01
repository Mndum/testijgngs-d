using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RC4
{
	public static string Encrypt(string key, string data)
	{
		Encoding unicode = Encoding.Unicode;
		return Convert.ToBase64String(Encrypt(unicode.GetBytes(key), unicode.GetBytes(data)));
	}

	public static string Decrypt(string key, string data)
	{
		Encoding unicode = Encoding.Unicode;
		return unicode.GetString(Encrypt(unicode.GetBytes(key), Convert.FromBase64String(data)));
	}

	public static byte[] Encrypt(byte[] key, byte[] data)
	{
		return EncryptOutput(key, data).ToArray();
	}

	public static byte[] Decrypt(byte[] key, byte[] data)
	{
		return EncryptOutput(key, data).ToArray();
	}

	private static byte[] EncryptInitalize(byte[] key)
	{
		byte[] array = (from i in Enumerable.Range(0, 256)
			select (byte)i).ToArray();
		int num = 0;
		int num2 = 0;
		for (; num < 256; num++)
		{
			num2 = (num2 + key[num % key.Length] + array[num]) & 0xFF;
			Swap(array, num, num2);
		}
		return array;
	}

	private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
	{
		byte[] s = EncryptInitalize(key);
		int i = 0;
		int j = 0;
		return data.Select((byte b) =>
		{
			i = (i + 1) & 0xFF;
			j = (j + s[i]) & 0xFF;
			Swap(s, i, j);
			return (byte)(b ^ s[(s[i] + s[j]) & 0xFF]);
		});
	}

	private static void Swap(byte[] s, int i, int j)
	{
		ref byte reference = ref s[i];
		ref byte reference2 = ref s[j];
		byte b = s[j];
		byte b2 = s[i];
		reference = b;
		reference2 = b2;
	}
}
