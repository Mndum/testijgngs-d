using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public static class IK7pufE04T73yICDIFmaC2PTNnlfOoOnD_k9WRjcbZeiZqtpD
{
	public static y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz hZFUdNYRkR970jDGhZVoK9PY1HoYc1z4YNE8(RSA rsaPublic, string serial, out aLeid8Q7PeAa2S1xHnmyLmGVhK7JK8JpU4eT0FUJlAWhl? data)
	{
		data = null;
		string[] array = serial.Split(EncodingHelper.EscapeChar);
		if (array.Length != 4)
		{
			return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.fVcqq_Rbin7M2qV4FjY_tKvPzFz7GlhbTsMmIN_;
		}
		string text = array[0];
		string text2 = array[1];
		string text3 = array[2];
		string obj = array[3];
		string hashCodeString = EncodingHelper.GetHashCodeString(text + text2 + text3);
		if (obj != hashCodeString)
		{
			return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.zIh31JMKlQZzPXEDneAtNVz4dtuhL2g8GI;
		}
		if (!EncodingHelper.TryDecodeBytes(text2, out byte[] result) || !EncodingHelper.TryDecodeBytes(text, out byte[] result2))
		{
			return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.JrmwWEP9KRcDZrGosww1Zsf1ewVccrG2waTyZi_cqa;
		}
		bool flag;
		try
		{
			flag = rsaPublic.VerifyData(result, result2, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
		}
		catch
		{
			flag = false;
		}
		if (!flag)
		{
			return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.iW58h9CpA7u0MZTAqtzqnq5NUK3gQsQvLI76LpWu0;
		}
		try
		{
			data = JsonConvert.DeserializeObject<aLeid8Q7PeAa2S1xHnmyLmGVhK7JK8JpU4eT0FUJlAWhl>(Encoding.UTF8.GetString(result));
			if (data == null)
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.UybECjckFmqAgDwQ9EbQ86RmiuhpEY0OjtatXg;
			}
		}
		catch
		{
			return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.UybECjckFmqAgDwQ9EbQ86RmiuhpEY0OjtatXg;
		}
		return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz._6ot3xX5bsH6WtzD4NAAwT4HuV6AhYJbQraltlQAVKVN9hY;
	}

	public static y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz o8ThmWr7WNcDZR4beC9Vzyi2wX1LHIEvyqyxF8EuQV(NSe7Yow69QKoNWAV4AQ2HQ9xJjIc7lT6ZDlV i, RSA r, string s)
	{
		try
		{
			y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz2 = hZFUdNYRkR970jDGhZVoK9PY1HoYc1z4YNE8(r, s, out aLeid8Q7PeAa2S1xHnmyLmGVhK7JK8JpU4eT0FUJlAWhl data);
			if (y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz2 != y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz._6ot3xX5bsH6WtzD4NAAwT4HuV6AhYJbQraltlQAVKVN9hY)
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz2;
			}
			if (data == null)
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.UybECjckFmqAgDwQ9EbQ86RmiuhpEY0OjtatXg;
			}
			if (!Guid.TryParseExact(data.x, DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("ATRYUw==", 4), 21), out var _))
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.E_jGIvxGLUCzrtomAQwqW2AgMI59tqIJ8SucZ2TtEa8p58_;
			}
			if (data.h != i.yrcyxM_6vOD4ZJuzrTLh2G4jjmfEsc())
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.lL3JHrM5iho00X7DzU7KB6IEGRy8UE7PdYbfBXv6kohnhPiB;
			}
			if (data.i != i.gnj5Ta8ngTkqQsTiF7Sdk6Lm87ZmhN4Xx9())
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.IeNx5nMHApDX3PiL0fiZVFzrLKhe2amSCR;
			}
			if (data.v != i.Z0633wsfHTPODyT49YLJ_uS7IQ6wUrodbGzu1())
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.UcId9XXxVSJHwDQsv08QTh0ZFiI1w5yvbmbA8S;
			}
			if (data.p != (int)i.l_o0Q6YNttGfVdMZ0rZc9LgV8p7zUdunZK())
			{
				return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.qgXe6ne2bt9lR90p7qbBo7hMabJD3hmU7Pu;
			}
			return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz._6ot3xX5bsH6WtzD4NAAwT4HuV6AhYJbQraltlQAVKVN9hY;
		}
		catch (Exception)
		{
			return y4ZCgaDykuqRMT6dggovPhNGn9MN9eIOVqVHaKsUzAslusUz.GPq4DYVVxbu4i_FGDeF2xbtpvgldJvftpouyxfe70;
		}
	}
}
