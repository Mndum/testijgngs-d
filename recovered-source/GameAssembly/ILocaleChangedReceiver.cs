using UnityEngine.Localization;

public interface ILocaleChangedReceiver
{
	void OnLocaleChanged(Locale newLocale);
}
