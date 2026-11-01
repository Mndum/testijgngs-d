using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class RoomSheep
{
	public void RoomCheatsButtonClicked()
	{
		if ((!((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("Z26WiJWca3SXa5eDgHaFcUVaj159Ynx0dVpXVA==", 15), 20), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("OhgTDWwhfh1pDgBDbAJvEQ==", 10), 24), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("MAxnZA==", 24), 18)) && !((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("YGt3emNPZHRrf157jnBJbmWHfmRiaH1lhENPYQ==", 8), 16), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("hkZEbV1TZ1+DX3tkQ1JNWw==", 7), 4), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("LRokdw==", 13), 24))) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("bEVrAGhxDQJ4LhJOOiUIRxxlWg0SFEJULGAFcw==", 18), 13), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("ekcUe05qFWN+Hl9hDwFABw==", 18), 19), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length < 10)
		{
			return;
		}
		int num = (int)Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("XnFxgYSTlGJhYllrYX+KX3VehlVTdE9RYz1ZXA==", 6), 21), 4)).GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("aWiRgZ6PfYlTh1eCb22Mim+TdH5liGx/", 21), 24), 20), BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
		List<PopupChoice> list = new List<PopupChoice>();
		if (num >= 10)
		{
			list.Add(new PopupChoice(LOC.UI.Cheat_Money, () =>
			{
				if (GameVariables.Current.money >= 2000000000)
				{
					Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("hGZndV5fiYBOUV5/", 22), 7)).GetProperty(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("XIWJgmB7Ynxaf0h5", 20), 15)).GetValue(null)
						.GetType()
						.GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("Xl1gX4NMhWZETFha", 22), 5))
						.Invoke(Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("eFV3UDxdTlF6d1pK", 6), 4)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("WVNvaElcbGxai4xh", 17), 19)).GetValue(null), new object[3]
						{
							LOC.UI.UI_Failed,
							LOC.UI.Cheat_MaxMoneyReached,
							null
						});
				}
				else
				{
					List<PopupChoice> list2 = new List<PopupChoice>();
					int[] array = new int[4] { 1000, 10000, 1000000, 100000000 };
					foreach (int money in array)
					{
						list2.Add(new PopupChoice(LOC.UI.Cheat_AddMoney(money), () =>
						{
							GameVariables.Current.MarkAsCheater();
							GameVariables.Current.AddMoney(money);
						}));
					}
					UiOverlay.Instance.OpenContextMenuPopup(list2);
				}
			}));
		}
		if (num >= 20)
		{
			list.Add(new PopupChoice(LOC.UI.Cheat_CumCheat, CumCheatsClicked));
		}
		if (num >= 20)
		{
			list.Add(new PopupChoice(LOC.UI.AddItem, UiOverlay.Instance.OpenAddItemCheat));
		}
		if (num >= 10)
		{
			list.Add(new PopupChoice(LOC.UI.Cheats_Other, OtherCheatsClicked));
		}
		if ((((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("eSsjNxQsFggfLTcRGXMzJxdHRBRjQH1uDRo+Ww==", 1), 6), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("ewomBm40GAN+YWMfaDENdQ==", 14), 12), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("bB96cA==", 14), 4)) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("fnoAIXsvPnB8Zm0ffTcKUTIXSXAOBW9LAAl8Xw==", 14), 3), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("Hy0AXzI8YG5+MGMdGCECBQ==", 15), 14), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("OBV+dg==", 12), 14))) && ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("Y3ppelRiUnNhjpZ0hIWGbltLamRYfWllelc9YA==", 11), 3), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("GhwwH1V3fmIObWA8XwcVDA==", 17), 19), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length >= 10 && !(OptionsStatic.DProofPref.Value != DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("LDRgPzRYGXELIl5uInhTKg==", 16), 18), 14)))
		{
			UiOverlay.Instance.OpenContextMenuPopup(list);
		}
	}

	private void OtherCheatsClicked()
	{
		if ((int)Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("aZ6hi2qObotacGN0mVBiaGmCk1paeldWjGxXWw==", 17), 2), 4)).GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("a2qTg6CRf4tViVmEcW+OjHGVdoBnim6B", 23), 24), 20), BindingFlags.Static | BindingFlags.Public).Invoke(null, null) < 10 || (!((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("aWNwb4x8jmmFfHNnZnVmXpZkTG+CYF1WhWVEcw==", 17), 22), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("XVVqc1lxc2h+TV9bTkiBXw==", 22), 2), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("RW5ycQ==", 5), 23)) && !((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("W25OYWaEkGlweGVbU1JXWGFWdFJBTnBSaHF8RA==", 3), 8), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("aU9vfm95Z1VGVHhefHFUXw==", 12), 0), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("UUdCcw==", 17), 22))) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("aoWBi3Fub395mWiGUZBTf3NHiG9wiIdwR2JabA==", 18), 20), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("YyA8KyEMWywYBiI0IBl/Eg==", 1), 15), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length < 10)
		{
			return;
		}
		List<PopupChoice> choices = new List<PopupChoice>
		{
			new PopupChoice(LOC.UI.Cheat_ResetChurch, () =>
			{
				StringBuilder stringBuilder;
				using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
				{
					stringBuilder.AppendLine(LOC.UI.Cheat_ResetChurchPrompt);
					stringBuilder.AppendColorTagStart(Color.red);
					stringBuilder.AppendLine(LOC.UI.Cheat_ResetChurchPromptWarning);
					UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, stringBuilder.ToString(), () =>
					{
						GameVariables.Current.MarkAsCheater();
						GameVariables.Current.ResetChurchStoryline();
						Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("N3BGZRcfEislVwQ+", 20), 22)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("T0VRZld0dmN6Xlxs", 7), 23)).GetValue(null)
							.GetType()
							.GetMethod(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("RzwRfUR3MhlwCFsG", 18), 22))
							.Invoke(Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("loqTa5KOgFeAZFVX", 17), 8)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("VWxoWH5Ue1hnP2FS", 13), 4)).GetValue(null), new object[3]
							{
								LOC.UI.UI_Done,
								LOC.UI.Cheat_ResetChurchDone,
								null
							});
					}, () =>
					{
					});
				}
			})
		};
		if ((((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("NGt6UysAGzIiLjVeGgJ9WTAAQll1AzEcDCduVg==", 3), 13), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("ZQ4+aQNSVxE8VWUKVWgzIg==", 23), 9), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("CQQEVw==", 7), 1)) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("c3p/NXQsBGR5ZyoLcDYwTQldWXwNBVY6DQpsXw==", 14), 15), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("c29xMhBAA1dwOSBVUgJieQ==", 21), 3), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("dGoXFw==", 23), 17))) && ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("Gg5sPyFMPjxpYhttfB4OdGgKAWIaFhJ1LTMheg==", 24), 5), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("jmJdeGVqgGOLfENoUHBgXw==", 10), 9), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length >= 10 && !(OptionsStatic.DProofPref.Value != DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("Wl9BXEBiPVlXeXJfPDlIVg==", 3), 0), 0)))
		{
			UiOverlay.Instance.OpenContextMenuPopup(choices);
		}
	}

	private void CumCheatsClicked()
	{
		if ((int)Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("ZG9YdVl4d4Rjg06BVFxxfmVLXW5hPVZ0Z0Q+ZA==", 12), 15), 4)).GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("SltwGEgpIHMBX1B3A35WSTYgGAksVm1z", 22), 13), 20), BindingFlags.Static | BindingFlags.Public).Invoke(null, null) < 20 || (!((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("NCN0aQo9IX8TOnt7YR8pQTw0TC4helttbWgKag==", 11), 9), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("godna1lEP1x/VVNiP0hxWA==", 5), 2), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("QTdHYw==", 1), 22)) && !((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("F2s3VyI5DF8mHSJxAgkuajA8d0tgGj1JHCNIVg==", 3), 0), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("fmhvZzoRdR45UXItdB9ABg==", 23), 20), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("MzFBZA==", 24), 7))) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("ZG96ep1SZ3ROgmF1gnRMblmLgWRWbIBleEdSYQ==", 12), 0), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("UIpOf3KScGtNWIV2XUdWZw==", 16), 16), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length < 10)
		{
			return;
		}
		List<PopupChoice> list = new List<PopupChoice>();
		list.Add(new PopupChoice(LOC.UI.Cheat_RestoreCum, () =>
		{
			GameVariables current = GameVariables.Current;
			float remainingCum = current.RemainingCum;
			float maxCum = current.MaxCum;
			if (remainingCum < maxCum)
			{
				GameVariables.Current.MarkAsCheater();
				GameVariables.Current.RemainingCum = GameVariables.Current.MaxCum;
				Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("UlVtXpJJklhfbll4", 19), 0)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("X4iJaF9uk2mMjIBb", 23), 2)).GetValue(null)
					.GetType()
					.GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("XFtebXR0bmiEb4BV", 20), 17))
					.Invoke(Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("MnJaDiMVWDoLBVVe", 12), 18)).GetProperty(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("IgRqOjRvBn4zaG9k", 16), 24)).GetValue(null), new object[3]
					{
						LOC.UI.UI_Success,
						LOC.UI.Cheat_CumRestorationOverflow,
						null
					});
			}
			else if (remainingCum < maxCum * 2f)
			{
				GameVariables.Current.MarkAsCheater();
				GameVariables.Current.RemainingCum = GameVariables.Current.MaxCum * 2f;
				Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("LjJhdgMAAgw9OB8i", 2), 7)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("SHF0VHJ1flx8ZVVP", 0), 11)).GetValue(null)
					.GetType()
					.GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("EyQwPAglBTYSEBZ2", 4), 6))
					.Invoke(Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("f0BWZ1mEeHJOfE5x", 5), 19)).GetProperty(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("PjRPGwUPPCleZTkK", 24), 14)).GetValue(null), new object[3]
					{
						LOC.UI.UI_Success,
						LOC.UI.Cheat_CumRestorationOverflow,
						null
					});
			}
			else
			{
				Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("e2OCalVcWHZFU3p0", 11), 9)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("AjJ5XDgHWUF7CD0X", 15), 10)).GetValue(null)
					.GetType()
					.GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("U1JVZGtrZV97ZndM", 11), 17))
					.Invoke(Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("O0tySV9fblA5Nmpl", 0), 1)).GetProperty(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("Sk13bkE9T2g7RjZl", 2), 7)).GetValue(null), new object[3]
					{
						LOC.UI.UI_Failed,
						LOC.UI.Cheat_CumRestorationAlreadyFull,
						null
					});
			}
		}));
		list.Add(new PopupChoice(LOC.UI.Cheat_IncreaseMaxCum, () =>
		{
			GameVariables.Current.MarkAsCheater();
			GameVariables.Current.RemainingCum += 50f;
			GameVariables.Current.MaxCum += 50f;
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendLine(LOC.UI.Cheat_IncreasedCumCapacity(50f, GameVariables.Current.MaxCum));
				if (GameVariables.Current.MaxCum >= 700f)
				{
					stringBuilder.AppendColorTagStart(Color.red);
					stringBuilder.AppendLine(LOC.UI.Cheat_IncreasedCumCapacityWarning);
				}
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, stringBuilder.ToString());
			}
		}));
		list.Add(new PopupChoice(LOC.UI.Cheat_ResetCumCapacity, () =>
		{
			GameVariables.Current.MarkAsCheater();
			GameVariables current = GameVariables.Current;
			float remainingCum = (GameVariables.Current.MaxCum = 20f);
			current.RemainingCum = remainingCum;
			Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("lGNUeW5IdIVjT0uD", 23), 22)).GetProperty(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("UoNZeVh5fnJSfWNw", 10), 17)).GetValue(null)
				.GetType()
				.GetMethod(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("PRNobwN9AxRuYmcK", 24), 6))
				.Invoke(Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("DBkQB2R7LQh9GjZj", 10), 14)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("XVN0X0+OhWWHS1xv", 21), 22)).GetValue(null), new object[3]
				{
					LOC.UI.UI_Success,
					LOC.UI.Cheat_ResetCumCapacitySuccess,
					null
				});
		}));
		if ((((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("XZKVf4pTh2hDh15vR2RkWYlgUHBhO11QeE1ZRw==", 5), 2), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("IQcPZlJuHB0KCwozCX5DaA==", 19), 17), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("T2Z6cA==", 15), 14)) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("eEMBHg4KDSsQC1g0DhlDIhMnQDQGWhUBDWp7ZA==", 0), 2), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("fA5/B08GEAB+ZyMNSWgKCg==", 14), 16), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("RlVNag==", 6), 21))) && ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("bqN0c5CTbHqPaZVsbIdma3uDY3RSXZNmS4xKeA==", 22), 0), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("f2V9aHJ2eFBef4JQXT9tWA==", 5), 1), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length >= 10 && !(OptionsStatic.DProofPref.Value != DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("f38iDHQaPmJ0XR05eSMKWQ==", 14), 7), 24)))
		{
			UiOverlay.Instance.OpenContextMenuPopup(list);
		}
	}
}
