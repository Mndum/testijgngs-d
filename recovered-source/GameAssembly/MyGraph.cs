using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyGraph : MonoBehaviour
{
	public class GraphStyleContainer
	{
		public Func<float, string> xDataLabelFunc;

		public Func<float, string> yDataLabelFunc;

		public float yoffset;

		public int xlabelCount;

		public string xTitle;

		public string yTitle;

		public GraphStyleContainer(Func<float, string> xDataLabelFunc = null, Func<float, string> yDataLabelFunc = null, float yoffset = 10f, int xlabelCount = 5, string xTitle = null, string yTitle = null)
		{
			this.xDataLabelFunc = xDataLabelFunc;
			this.yDataLabelFunc = yDataLabelFunc;
			this.yoffset = yoffset;
			this.xlabelCount = xlabelCount;
			this.xTitle = xTitle;
			this.yTitle = yTitle;
		}
	}

	private RectTransform container;

	public float yLabelSize = 20f;

	public float xLabelSize = 20f;

	public float dashSize = 2f;

	public Color backgroundColor = new Color(20f, 20f, 90f);

	public Color lineColor = Color.white;

	public Color lineLossColor = Color.red;

	public Color lineGainColor = Color.green;

	public Color pointColor = Color.red;

	public Sprite DashXTexture;

	public Sprite dashYTexture;

	private void Reset()
	{
		if (container != null)
		{
			UnityEngine.Object.Destroy(container.gameObject);
		}
		GameObject gameObject = new GameObject("PlotArea");
		container = gameObject.AddComponent<RectTransform>();
		gameObject.transform.SetParent(transform, worldPositionStays: false);
		container.anchorMin = new Vector2(0f, 0f);
		container.anchorMax = new Vector2(1f, 1f);
		container.offsetMin = new Vector2(yLabelSize, xLabelSize);
		container.offsetMax = new Vector2(0f, 0f);
		GameObject gameObject2 = new GameObject("background", typeof(Image));
		gameObject2.transform.SetParent(container, worldPositionStays: false);
		gameObject2.GetComponent<Image>().color = backgroundColor;
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(0f, 0f);
		component.anchorMin = new Vector2(0f, 0f);
		component.anchorMax = new Vector2(1f, 1f);
	}

	private Color LineColor(float prev, float current)
	{
		if (prev == current)
		{
			return lineColor;
		}
		if (prev > current)
		{
			return lineLossColor;
		}
		return lineGainColor;
	}

	public void SetData(IEnumerable<int> data, GraphStyleContainer gsc = null)
	{
		SetData(data.Select((Func<int, float>)((int t) => t)).ToList(), gsc ?? new GraphStyleContainer());
	}

	public void SetData(IEnumerable<float> data, GraphStyleContainer gsc = null)
	{
		SetData(data.ToList(), gsc ?? new GraphStyleContainer());
	}

	public void SetData(List<float> data, GraphStyleContainer gsc = null)
	{
		gsc = gsc ?? new GraphStyleContainer();
		Reset();
		Func<float, string> func = gsc.xDataLabelFunc;
		Func<float, string> func2 = gsc.yDataLabelFunc;
		if (func == null)
		{
			func = (float t) => t.ToString("0.##");
		}
		if (func2 == null)
		{
			func2 = (float t) => t.ToString("0.##");
		}
		Rect rect = container.rect;
		float x = rect.size.x;
		float y = rect.size.y;
		float num = data.Min();
		float num2 = num - gsc.yoffset;
		if (num2 < 0f && num >= 0f)
		{
			num2 = 0f;
		}
		float num3 = data.Max() + gsc.yoffset;
		float num4 = x / (float)(data.Count - 1);
		float num5 = num3 - num2;
		int num6 = 5;
		for (int num7 = 0; num7 < num6; num7++)
		{
			float num8 = (float)num7 * 1f / (float)(num6 - 1);
			float arg = num8 * num5 + num2;
			CreateYLabel(num8 * y, func2(arg));
		}
		for (int num9 = 0; num9 < gsc.xlabelCount; num9++)
		{
			float num10 = (float)num9 * 1f / (float)(gsc.xlabelCount - 1);
			float arg2 = (float)data.Count * num10;
			CreateXLabel(num10 * x, func(arg2));
		}
		Vector2? vector = null;
		for (int num11 = 0; num11 < data.Count; num11++)
		{
			float num12 = (float)num11 * num4;
			float y2 = (data[num11] - num2) / num5 * y;
			if (gsc.xlabelCount < 0)
			{
				CreateXLabel(num12, func(num11));
			}
			Vector2 vector2 = new Vector2(num12, y2);
			if (vector.HasValue)
			{
				CreateDotConnection(vector.Value, vector2, LineColor(data[num11 - 1], data[num11]));
			}
			vector = vector2;
		}
		if (gsc.xTitle != null)
		{
			GameObject gameObject = new GameObject("Title", typeof(RectTransform));
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.SetParent(container, worldPositionStays: false);
			TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
			textMeshProUGUI.alignment = TextAlignmentOptions.Center;
			textMeshProUGUI.textWrappingMode = TextWrappingModes.NoWrap;
			textMeshProUGUI.overflowMode = TextOverflowModes.Overflow;
			textMeshProUGUI.fontSize = 16f;
			component.anchoredPosition = new Vector2(x / 2f, 0f - xLabelSize - 15f);
			Vector2 anchorMin = (component.anchorMax = new Vector2(0f, 0f));
			component.anchorMin = anchorMin;
			component.sizeDelta = new Vector2(0f, 0f);
			textMeshProUGUI.text = gsc.xTitle;
		}
		if (gsc.yTitle != null)
		{
			GameObject gameObject2 = new GameObject("yLabel", typeof(RectTransform));
			RectTransform component2 = gameObject2.GetComponent<RectTransform>();
			component2.SetParent(container, worldPositionStays: false);
			TextMeshProUGUI textMeshProUGUI2 = gameObject2.AddComponent<TextMeshProUGUI>();
			textMeshProUGUI2.alignment = TextAlignmentOptions.Midline;
			textMeshProUGUI2.textWrappingMode = TextWrappingModes.Normal;
			textMeshProUGUI2.overflowMode = TextOverflowModes.Overflow;
			textMeshProUGUI2.fontSize = 16f;
			component2.anchoredPosition = new Vector2(0f - yLabelSize - 15f, y / 2f);
			Vector2 anchorMin = (component2.anchorMax = new Vector2(0f, 0f));
			component2.anchorMin = anchorMin;
			component2.sizeDelta = new Vector2(0f, 0f);
			textMeshProUGUI2.text = gsc.yTitle;
		}
	}

	private void CreateXLabel(float xPos, string label)
	{
		GameObject gameObject = new GameObject("xLabel", typeof(RectTransform));
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.SetParent(container, worldPositionStays: false);
		TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
		textMeshProUGUI.alignment = TextAlignmentOptions.Center;
		textMeshProUGUI.textWrappingMode = TextWrappingModes.NoWrap;
		textMeshProUGUI.overflowMode = TextOverflowModes.Overflow;
		textMeshProUGUI.fontSize = 16f;
		component.anchoredPosition = new Vector2(xPos, (0f - xLabelSize) / 2f - 10f);
		Vector2 anchorMin = (component.anchorMax = new Vector2(0f, 0f));
		component.anchorMin = anchorMin;
		component.sizeDelta = new Vector2(0f, 0f);
		textMeshProUGUI.text = label;
		GameObject gameObject2 = new GameObject("xLabelDashes", typeof(Image));
		RectTransform component2 = gameObject2.GetComponent<RectTransform>();
		component2.SetParent(container, worldPositionStays: false);
		anchorMin = (component2.offsetMax = new Vector2(0f, 0f));
		component2.offsetMin = anchorMin;
		component2.anchorMin = new Vector2(0f, 0f);
		component2.anchorMax = new Vector2(0f, 1f);
		component2.anchoredPosition = new Vector2(xPos, 0f);
		component2.sizeDelta = new Vector2(dashSize, component2.sizeDelta.y);
		Image component3 = gameObject2.GetComponent<Image>();
		component3.sprite = dashYTexture;
		component3.type = Image.Type.Tiled;
	}

	private void CreateYLabel(float yPos, string label)
	{
		GameObject gameObject = new GameObject("yLabel", typeof(RectTransform));
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.SetParent(container, worldPositionStays: false);
		TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
		textMeshProUGUI.alignment = TextAlignmentOptions.MidlineRight;
		textMeshProUGUI.textWrappingMode = TextWrappingModes.NoWrap;
		textMeshProUGUI.overflowMode = TextOverflowModes.Overflow;
		textMeshProUGUI.fontSize = 16f;
		component.anchoredPosition = new Vector2(-10f, yPos);
		Vector2 anchorMin = (component.anchorMax = new Vector2(0f, 0f));
		component.anchorMin = anchorMin;
		component.sizeDelta = new Vector2(0f, 0f);
		textMeshProUGUI.text = label;
		GameObject gameObject2 = new GameObject("xLabelDashes", typeof(Image));
		RectTransform component2 = gameObject2.GetComponent<RectTransform>();
		component2.SetParent(container, worldPositionStays: false);
		anchorMin = (component2.offsetMax = new Vector2(0f, 0f));
		component2.offsetMin = anchorMin;
		component2.anchorMin = new Vector2(0f, 0f);
		component2.anchorMax = new Vector2(1f, 0f);
		component2.anchoredPosition = new Vector2(0f, yPos);
		component2.sizeDelta = new Vector2(component2.sizeDelta.x, dashSize);
		Image component3 = gameObject2.GetComponent<Image>();
		component3.sprite = DashXTexture;
		component3.type = Image.Type.Tiled;
	}

	private GameObject CreatePoint(Vector2 pos)
	{
		GameObject gameObject = new GameObject("point");
		gameObject.transform.SetParent(container, worldPositionStays: false);
		gameObject.AddComponent<Image>().color = pointColor;
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.anchoredPosition = pos;
		Vector2 anchorMin = (component.anchorMax = new Vector2(0f, 0f));
		component.anchorMin = anchorMin;
		component.sizeDelta = new Vector2(10f, 10f);
		return gameObject;
	}

	private void CreateDotConnection(Vector2 prevPos, Vector2 nextPos, Color color)
	{
		GameObject gameObject = new GameObject("pointConnection");
		gameObject.transform.SetParent(container, worldPositionStays: false);
		gameObject.AddComponent<Image>().color = color;
		RectTransform component = gameObject.GetComponent<RectTransform>();
		Vector2 vector = nextPos - prevPos;
		float x = Vector2.Distance(prevPos, nextPos);
		component.anchoredPosition = prevPos + vector * 0.5f;
		Vector2 anchorMin = (component.anchorMax = new Vector2(0f, 0f));
		component.anchorMin = anchorMin;
		component.sizeDelta = new Vector2(x, 2f);
		component.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(vector.y, vector.x) * 57.29578f);
	}
}
