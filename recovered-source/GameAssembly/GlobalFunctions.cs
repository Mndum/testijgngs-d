using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using F23.StringSimilarity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalFunctions
{
	public enum VariableType
	{
		HasSetter,
		HasGetter,
		HasSetterAndGetter
	}

	private static readonly Levenshtein Levenshtein = new Levenshtein();

	public static bool IsValidEmail(string email)
	{
		string text = email.Trim();
		if (text.EndsWith("."))
		{
			return false;
		}
		try
		{
			return new MailAddress(email).Address == text;
		}
		catch
		{
			return false;
		}
	}

	public static IEnumerable<(TLeft, TRight)> LeftOuterJoin<TLeft, TRight>(IEnumerable<TLeft> leftItems, Func<TLeft, object> leftIdSelector, IEnumerable<TRight> rightItems, Func<TRight, object> rightIdSelector)
	{
		return from left in leftItems
			join right in rightItems on leftIdSelector(left) equals rightIdSelector(right) into temp
			from right in temp.DefaultIfEmpty()
			select (left: left, right: right);
	}

	public static IEnumerable<(TLeft, TRight)> RightOuterJoin<TLeft, TRight>(IEnumerable<TLeft> leftItems, Func<TLeft, object> leftIdSelector, IEnumerable<TRight> rightItems, Func<TRight, object> rightIdSelector)
	{
		return from right in rightItems
			join left in leftItems on rightIdSelector(right) equals leftIdSelector(left) into temp
			from left in temp.DefaultIfEmpty()
			select (left: left, right: right);
	}

	public static bool IsEqualTo(this Color32 aCol, Color32 aRef)
	{
		if (aCol.r == aRef.r && aCol.g == aRef.g && aCol.b == aRef.b)
		{
			return aCol.a == aRef.a;
		}
		return false;
	}

	public static Color ToColor(this float color)
	{
		return new Color(color, color, color);
	}

	public static Vector3 ToVector3(this float value)
	{
		return new Vector3(value, value, value);
	}

	public static Color32 ToColor(this byte color)
	{
		return new Color32(color, color, color, 255);
	}

	public static Color32 ToColor(this int color)
	{
		return ((byte)color).ToColor();
	}

	[Pure]
	public static Color SetAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	[Pure]
	public static Color32 SetAlpha32(this Color32 color, byte alpha)
	{
		return new Color32(color.r, color.g, color.b, alpha);
	}

	public static Color ChangeValueAndSaturation(this Color32 color, float newSaturationPercent = 0.8f, float newValuePercent = 0.8f)
	{
		Color.RGBToHSV(color, out var H, out var S, out var V);
		return Color.HSVToRGB(H, S * newSaturationPercent, V * newValuePercent);
	}

	public static int MoveTowards(int current, int target, int maxChange, int minMaxChange)
	{
		return MoveTowards(current, target, Mathf.Max(maxChange, minMaxChange));
	}

	public static int MoveTowards(int current, int target, int maxChange)
	{
		int num = target - current;
		if (num > 0)
		{
			if (maxChange <= num)
			{
				return current + num;
			}
			return current + maxChange;
		}
		if (num < 0)
		{
			if (-maxChange <= num)
			{
				return current + num;
			}
			return current - maxChange;
		}
		return current;
	}

	public static float MoveTowards(float current, float target, float maxDelta)
	{
		if (Mathf.Abs(target - current) <= maxDelta)
		{
			return target;
		}
		return current + Mathf.Sign(target - current) * maxDelta;
	}

	public static float Lerp3(float min, float average, float max, float t)
	{
		if (!(t < 0.5f))
		{
			return Mathf.Lerp(average, max, t * 2f - 1f);
		}
		return Mathf.Lerp(min, average, t * 2f);
	}

	public static float SmoothLerpEaseOutDeltaT(float current, float target, float sharpness)
	{
		float t = 1f - Mathf.Pow(1f - sharpness, Time.deltaTime * 60f);
		return Mathf.Lerp(current, target, t);
	}

	public static float CalculateInputFromCenterOffset(float min, float max, float center, float position)
	{
		if (position <= center)
		{
			float num = center - min;
			if (num == 0f)
			{
				return 0f;
			}
			return (position - center) / num;
		}
		float num2 = max - center;
		if (num2 == 0f)
		{
			return 0f;
		}
		return (position - center) / num2;
	}

	public static float CalculateOffsetFromCenter(float min, float max, float center, float input)
	{
		if ((double)input < 0.0)
		{
			float num = center - min;
			return input * num + center;
		}
		float num2 = max - center;
		return input * num2 + center;
	}

	public static bool ImplementsInterface(Type type, Type implementedInterface)
	{
		return type.GetInterfaces().Contains(implementedInterface);
	}

	public static bool IsOrImplementsInteraceOrCanBeAssignedTo(Type type, Type target)
	{
		if (type == target)
		{
			return true;
		}
		if (target.IsInterface && ImplementsInterface(type, target))
		{
			return true;
		}
		if (type.IsSubclassOf(target))
		{
			return true;
		}
		return false;
	}

	private static bool CheckIfPropertyHas(PropertyInfo property, VariableType variableType = VariableType.HasSetterAndGetter)
	{
		switch (variableType)
		{
		case VariableType.HasSetter:
			if (property.GetSetMethod(nonPublic: true) != null)
			{
				return true;
			}
			break;
		case VariableType.HasGetter:
			if (property.GetGetMethod(nonPublic: true) != null)
			{
				return true;
			}
			break;
		default:
			if (property.GetSetMethod(nonPublic: true) != null && property.GetGetMethod(nonPublic: true) != null)
			{
				return true;
			}
			break;
		}
		return false;
	}

	public static List<string> GetAllPropertyAndMemberNames(Type baseType, VariableType variableType = VariableType.HasSetterAndGetter)
	{
		IEnumerable<string> first = from p in baseType.GetProperties()
			where CheckIfPropertyHas(p, variableType)
			select $"{p.Name} ({p.PropertyType})";
		IEnumerable<string> second = from p in baseType.GetFields()
			select $"{p.Name} ({p.FieldType})";
		return first.Concat(second).ToList();
	}

	public static bool HasProperty<S>(string name, Type propertyType, VariableType variableType = VariableType.HasSetterAndGetter)
	{
		return HasProperty(name, typeof(S), propertyType, variableType);
	}

	public static bool HasProperty(string name, Type baseType, Type propertyType, VariableType variableType = VariableType.HasSetterAndGetter)
	{
		if (name == null)
		{
			return false;
		}
		PropertyInfo property = baseType.GetProperty(name);
		if (property != null && IsOrImplementsInteraceOrCanBeAssignedTo(property.PropertyType, propertyType) && CheckIfPropertyHas(property, variableType))
		{
			return true;
		}
		return false;
	}

	public static bool HasPropertyAnyType(string name, Type baseType, VariableType variableType = VariableType.HasSetterAndGetter)
	{
		if (name == null)
		{
			return false;
		}
		PropertyInfo property = baseType.GetProperty(name);
		if (property != null && CheckIfPropertyHas(property, variableType))
		{
			return true;
		}
		return false;
	}

	public static bool HasField<S>(string name, Type propertyType)
	{
		return HasField(name, typeof(S), propertyType);
	}

	public static bool HasField(string name, Type baseType, Type type)
	{
		if (name == null)
		{
			return false;
		}
		FieldInfo field = baseType.GetField(name);
		if (field != null && IsOrImplementsInteraceOrCanBeAssignedTo(field.FieldType, type))
		{
			return true;
		}
		return false;
	}

	public static bool HasFieldAnyType(string name, Type baseType)
	{
		if (name == null)
		{
			return false;
		}
		if (baseType.GetField(name) != null)
		{
			return true;
		}
		return false;
	}

	public static bool HasPropertyOrField<S>(string name, Type baseType, VariableType variableType = VariableType.HasSetterAndGetter)
	{
		if (HasProperty<S>(name, baseType, variableType))
		{
			return true;
		}
		return HasField<S>(name, baseType);
	}

	public static bool HasPropertyOrFieldAnyType(string name, Type baseType, VariableType variableType = VariableType.HasSetterAndGetter)
	{
		if (HasPropertyAnyType(name, baseType, variableType))
		{
			return true;
		}
		return HasFieldAnyType(name, baseType);
	}

	public static MemberInfo GetPropertyOrField(string name, Type baseType, Type targetType)
	{
		PropertyInfo property = baseType.GetProperty(name);
		if (property != null && IsOrImplementsInteraceOrCanBeAssignedTo(property.PropertyType, targetType))
		{
			return property;
		}
		FieldInfo field = baseType.GetField(name);
		if (field != null && IsOrImplementsInteraceOrCanBeAssignedTo(field.FieldType, targetType))
		{
			return field;
		}
		return null;
	}

	public static int GetListenerNumber(this UnityEventBase unityEvent)
	{
		object value = typeof(UnityEventBase).GetField("m_Calls", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic).GetValue(unityEvent);
		return (int)value.GetType().GetProperty("Count").GetValue(value);
	}

	public static string TimePeriod(int minutes)
	{
		int num = minutes % 60;
		int num2 = minutes / 60;
		int num3 = num2 / 24;
		num2 %= 24;
		StringBuilderCache.AcquireManual(out var stringBuilder);
		if (num3 != 0)
		{
			stringBuilder.Append(LOC.UI.UI_PluralDays(num3));
		}
		if (num2 != 0)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(LOC.UI.UI_PluralHours(num2));
		}
		if (num != 0 || (num3 == 0 && num2 == 0))
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(LOC.UI.UI_PluralMinutes(num));
		}
		return StringBuilderCache.ReleaseWithToString(stringBuilder);
	}

	public static string TimePeriodHours(int minutes)
	{
		int num = minutes / 60;
		int num2 = num / 24;
		num %= 24;
		StringBuilderCache.AcquireManual(out var stringBuilder);
		if (num2 != 0)
		{
			stringBuilder.Append(LOC.UI.UI_PluralDays(num2));
		}
		if (num != 0)
		{
			if (num2 > 0)
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(LOC.UI.UI_PluralHours(num));
		}
		else if (num2 == 0)
		{
			stringBuilder.Append(LOC.UI.UI_LessThanAnHour);
		}
		return StringBuilderCache.ReleaseWithToString(stringBuilder);
	}

	public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator<T> enumerator)
	{
		while (enumerator.MoveNext())
		{
			yield return enumerator.Current;
		}
	}

	public static Canvas GetRootCanvas(this Transform rectTransform)
	{
		Canvas componentInParent = rectTransform.GetComponentInParent<Canvas>();
		if ((bool)componentInParent)
		{
			if (!componentInParent.isRootCanvas)
			{
				return componentInParent.rootCanvas;
			}
			return componentInParent;
		}
		return null;
	}

	public static byte[] Hash(byte[] bytes)
	{
		return SHA256.Create().ComputeHash(bytes);
	}

	public static string Hash(string str)
	{
		return BitConverter.ToString(Hash(Encoding.UTF8.GetBytes(str)));
	}

	public static IEnumerable<int> AllIndexesOf(this string str, string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			throw new ArgumentException("the string to find may not be empty", "value");
		}
		int index = 0;
		while (true)
		{
			index = str.IndexOf(value, index, StringComparison.Ordinal);
			if (index != -1)
			{
				yield return index;
				index += value.Length;
				continue;
			}
			break;
		}
	}

	public static double LevenshteinDistance(string s, string t)
	{
		return Levenshtein.Distance(s, t);
	}

	public static string DescribeChange(string before, string after)
	{
		if (before == after)
		{
			return "Strings equal!";
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			int num = Mathf.Min(before.Length, after.Length);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (before[i] != after[i])
				{
					num2++;
				}
			}
			if (num2 > 10)
			{
				stringBuilder.AppendLine("More than 10 different letters!");
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					if (before[j] != after[j])
					{
						stringBuilder.AppendLine($"Letter {j}: {before[j]}({(int)before[j]})=>{before[j]}({(int)before[j]})");
					}
				}
				for (int k = num; k < before.Length; k++)
				{
					stringBuilder.AppendLine($"Letter {k}: {before[k]}({(int)before[k]})=>Nothing");
				}
				for (int l = num; l < after.Length; l++)
				{
					stringBuilder.AppendLine($"Letter {l}: Nothing=>{after[l]}({(int)after[l]})");
				}
			}
			return stringBuilder.ToString();
		}
	}

	public static int Mod(int x, int m)
	{
		int num = x % m;
		if (num >= 0)
		{
			return num;
		}
		return num + m;
	}

	public static IEnumerable<string> DirectorySearchRelative(string dir, bool recursive = true)
	{
		if (Directory.Exists(dir))
		{
			return from f in Directory.EnumerateFiles(dir, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
				select "/" + Path.GetRelativePath(dir, f).Replace(Path.DirectorySeparatorChar, '/');
		}
		return Array.Empty<string>();
	}

	public static IEnumerable<string> DirectorySearchFull(string dir, bool recursive = true)
	{
		if (Directory.Exists(dir))
		{
			return Directory.EnumerateFiles(dir, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}
		return Array.Empty<string>();
	}

	public static IEnumerable<Type> GetParentTypes(this Type type)
	{
		if (!(type == null))
		{
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				yield return interfaces[i];
			}
			Type currentBaseType = type.BaseType;
			while (currentBaseType != null)
			{
				yield return currentBaseType;
				currentBaseType = currentBaseType.BaseType;
			}
		}
	}

	public static IEnumerable<Type> GetParentTypesWithCurrent(this Type type)
	{
		if (!(type == null))
		{
			yield return type;
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				yield return interfaces[i];
			}
			Type currentBaseType = type.BaseType;
			while (currentBaseType != null)
			{
				yield return currentBaseType;
				currentBaseType = currentBaseType.BaseType;
			}
		}
	}

	public static string ReplaceInvalidChars(string filename)
	{
		return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
	}

	public static string FormatBytes(long bytes)
	{
		string[] array = new string[5] { "B", "KB", "MB", "GB", "TB" };
		double num = bytes;
		int num2 = 0;
		while (num2 < array.Length && bytes >= 1024)
		{
			num = (double)bytes / 1024.0;
			num2++;
			bytes /= 1024;
		}
		return $"{num:0.##} {array[num2]}";
	}

	public static T[] ConcatArrays<T>(params T[][] p)
	{
		int num = 0;
		T[] array = new T[p.Sum((T[] a) => a.Length)];
		foreach (T[] array2 in p)
		{
			Array.Copy(array2, 0, array, num, array2.Length);
			num += array2.Length;
		}
		return array;
	}

	public static void DisableGameObjects(params Component[] components)
	{
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(value: false);
		}
	}

	public static void EnableGameObjects(params Component[] components)
	{
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(value: false);
		}
	}

	public static void SmartDestroyIfNotNull(this UnityEngine.Object obj)
	{
		if (!(obj == null))
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	public static void SmartDestroyMainGameObjectIfNotNull(this MonoBehaviour obj)
	{
		if (!(obj == null))
		{
			UnityEngine.Object.Destroy(obj.gameObject);
		}
	}

	public static void ButtonClickingInARow_Click(ref int clickCount, ref float lastClickTime, float maxTimeBetweenClicks = 3f)
	{
		float unscaledTime = Time.unscaledTime;
		if (unscaledTime - lastClickTime > maxTimeBetweenClicks)
		{
			clickCount = 1;
		}
		else
		{
			clickCount++;
		}
		lastClickTime = unscaledTime;
	}

	public static bool ButtonClickingInARow_TimeoutDone(float lastClickTime, float maxTimeBetweenClicks = 3f)
	{
		return Time.unscaledTime - lastClickTime < maxTimeBetweenClicks;
	}

	public static void AppendComparisonToEquipped(StringBuilder stringBuilder, float currentlyEquippedValue, float otherItemValue)
	{
		if (currentlyEquippedValue == 0f)
		{
			if (otherItemValue == 0f)
			{
				stringBuilder.Append(" (");
				stringBuilder.Append(LOC.UI.ComparsionToEquippedItem_NoChange);
				stringBuilder.Append(")");
			}
			return;
		}
		stringBuilder.Append(" (");
		float num = (otherItemValue - currentlyEquippedValue) / Math.Abs(currentlyEquippedValue);
		if (num > 0f)
		{
			stringBuilder.AppendColorTagStart(MyColor32Presets.Green);
			stringBuilder.Append(LOC.UI.ComparsionToEquippedItem_IncreasedByPercent(Mathf.Abs(num)));
			stringBuilder.AppendColorTagEnd();
		}
		else if (num < 0f)
		{
			stringBuilder.AppendColorTagStart(MyColor32Presets.Red);
			stringBuilder.Append(LOC.UI.ComparsionToEquippedItem_DecreasedByPercent(Mathf.Abs(num)));
			stringBuilder.AppendColorTagEnd();
		}
		else
		{
			stringBuilder.Append(LOC.UI.ComparsionToEquippedItem_NoChange);
		}
		stringBuilder.Append(")");
	}

	public static DirectoryInfo EnsureDirectory(string path, bool clear = false, bool makeHidden = false)
	{
		DirectoryInfo directoryInfo;
		if (Directory.Exists(path))
		{
			if (clear)
			{
				Directory.Delete(path, recursive: true);
				directoryInfo = Directory.CreateDirectory(path);
			}
			else
			{
				directoryInfo = new DirectoryInfo(path);
			}
		}
		else
		{
			directoryInfo = Directory.CreateDirectory(path);
		}
		if (makeHidden && (directoryInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
		{
			directoryInfo.Attributes |= FileAttributes.Hidden;
		}
		return directoryInfo;
	}
}
