using System;
using System.Collections;
using GameItems;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

public class PixelGlassesWebsite : Website
{
	public TextMeshProUGUI PixelGlassesText;

	private void TakeSoul(Action afterwards)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Health -= 0.5f;
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.MentalHealth -= 0.2f;
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddFlag("soultaken");
		UiOverlay.Instance.OkPopup(LOC.UI.pixelglassesdotic_SoulTaken, "...", afterwards);
	}

	private void Purchase()
	{
		Item item = new Item(ItemEnum.PixelGlasses);
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddItem(item);
		UiOverlay.Instance.OkPopup(LOC.UI.UI_Done, LOC.UI.pixelglassesdotic_Get, () =>
		{
		});
	}

	[Preserve]
	public void BuyPixelGlasses()
	{
		if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.HasItem(ItemEnum.PixelGlasses.ToGameId()))
		{
			int price = new Item(ItemEnum.PixelGlasses).GetPrice();
			_ = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.money;
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("soultaken"))
			{
				price *= 5000;
				UiOverlay.Instance.OkPopup(LOC.UI.pixelglassesdotic_What, LOC.UI.pixelglassesdotic_ThrewOut, () =>
				{
					UiOverlay.Instance.SimplePopup(LOC.UI.Shop_Buy, LOC.UI.Shop_BuyPrompt(price), () =>
					{
						if (GameVariables.Current.TryTakeMoney(price))
						{
							Purchase();
						}
						else
						{
							UiOverlay.Instance.OkPopup(LOC.UI.UI_Failed, LOC.UI.Shop_NotEnoughMoney);
						}
					}, () =>
					{
					});
				});
				return;
			}
			UiOverlay.Instance.SimplePopup(LOC.UI.Shop_Buy, LOC.UI.Shop_BuyPrompt(price), () =>
			{
				GameVariables gv = GameVariables.Current;
				if (gv.TryTakeMoney(price))
				{
					Purchase();
				}
				else
				{
					UiOverlay.Instance.SimplePopup(LOC.UI.UI_Failed, LOC.UI.Shop_NotEnoughMoney + LOC.UI.pixelglassesdotic_Discount, () =>
					{
						if (gv.TryTakeMoney(50))
						{
							TakeSoul(Purchase);
						}
						else
						{
							UiOverlay.Instance.OkPopup("...", LOC.UI.pixelglassesdotic_NotEnoughMoney, () =>
							{
								TakeSoul(Purchase);
							});
						}
					}, () =>
					{
						UiOverlay.Instance.OkPopup(LOC.UI.pixelglassesdotic_GoAway, LOC.UI.pixelglassesdotic_StopWastingTime);
					});
				}
			});
			return;
		}
		UiOverlay.Instance.SimplePopup(LOC.UI.pixelglassesdotic_Dumb, LOC.UI.pixelglassesdotic_AlreadyOwn, () =>
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("soultaken"))
			{
				UiOverlay.Instance.OkPopup(LOC.UI.pixelglassesdotic_What, LOC.UI.pixelglassesdotic_WhatPrompt, () =>
				{
				});
			}
			else
			{
				UiOverlay.Instance.OkPopup(LOC.UI.pixelglassesdotic_SoulHarvest, LOC.UI.pixelglassesdotic_SoulHarvestPrompt, () =>
				{
					TakeSoul(null);
				});
			}
		}, () =>
		{
			UiOverlay.Instance.OkPopup(LOC.UI.pixelglassesdotic_SoulNotTaken, LOC.UI.pixelglassesdotic_SoulNotTakenPrompt, () =>
			{
			});
		});
	}

	private IEnumerator FadeColors()
	{
		Color oldColor = ColorPaletteManager.DefaultPartPalette.GetRandomColor().Color;
		Color newColor = ColorPaletteManager.DefaultPartPalette.GetRandomColor().Color;
		float secondsPerFade = 1f;
		while (true)
		{
			float timer = 0f;
			while (timer < secondsPerFade)
			{
				PixelGlassesText.color = Color.Lerp(oldColor, newColor, timer / secondsPerFade);
				timer += Time.deltaTime;
				yield return null;
			}
			oldColor = newColor;
			newColor = ColorPaletteManager.DefaultPartPalette.GetRandomColor().Color;
		}
	}

	public void Awake()
	{
		StartCoroutine(FadeColors());
	}

	public void FixedUpdate()
	{
		float num = 1f + Mathf.Sin(Time.fixedTime * 3f) * 0.05f;
		PixelGlassesText.transform.localScale = new Vector3(num, num, num);
		PixelGlassesText.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Cos(Time.fixedTime * 3f));
	}
}
