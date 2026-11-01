using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

public static class EncodingHelper
{
	public static readonly string EscapeChar = "~";

	public static string Encode(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return string.Empty;
		}
		return Encode(Encoding.UTF8.GetBytes(input));
	}

	public static string Encode(byte[] bytes)
	{
		return ToBase64String(Compress(bytes));
	}

	public static bool TryDecode(string input, [NotNullWhen(true)] out string? result)
	{
		result = null;
		if (!TryDecodeBytes(input, out byte[] result2))
		{
			return false;
		}
		try
		{
			result = Encoding.UTF8.GetString(result2);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static bool TryDecodeBytes(string input, [NotNullWhen(true)] out byte[]? result)
	{
		result = null;
		if (string.IsNullOrWhiteSpace(input))
		{
			return false;
		}
		try
		{
			byte[] compressedData = FromBase64String(input);
			result = Decompress(compressedData);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static string ToBase64String(byte[] bytes)
	{
		return Convert.ToBase64String(bytes);
	}

	public static byte[] FromBase64String(string text)
	{
		return Convert.FromBase64String(text);
	}

	public static string GetHashCodeString(string input)
	{
		return ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input)));
	}

	private static byte[] Compress(byte[] data)
	{
		using MemoryStream memoryStream = new MemoryStream();
		using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
		{
			deflateStream.Write(data, 0, data.Length);
		}
		return memoryStream.ToArray();
	}

	private static byte[] Decompress(byte[] compressedData)
	{
		using MemoryStream stream = new MemoryStream(compressedData);
		using MemoryStream memoryStream = new MemoryStream();
		using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
		{
			deflateStream.CopyTo(memoryStream);
		}
		return memoryStream.ToArray();
	}
}
