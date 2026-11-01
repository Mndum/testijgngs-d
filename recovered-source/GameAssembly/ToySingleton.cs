using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public sealed class ToySingleton : SingletonMonoBehaviour<ToySingleton>
{
	public class ToyGenerator
	{
		public Func<ToyGenerator, IToy> GenerateToyFunc;

		public string Id;

		public SimpleLocalizedString NiceName;

		public Action InfoButtonAction;
	}

	private readonly List<ToyGenerator> toyGenerators = new List<ToyGenerator>();

	public bool AnyToyAvailable => toyGenerators.Any();

	public ReadOnlyCollection<ToyGenerator> ToyGenerators => toyGenerators.AsReadOnly();

	public IToy CurrentToy { get; private set; }

	public string CurrentToyDescription
	{
		get
		{
			if (CurrentToy == null)
			{
				return LOC.UI.Options_NoToy;
			}
			return CurrentToy.StatusDescription;
		}
	}

	public bool ShowToySettings { get; set; }

	private void OnDisable()
	{
		CurrentToy?.Close();
	}

	public void DisconnectCurrentToy()
	{
		if (CurrentToy != null)
		{
			CurrentToy.Close();
			CurrentToy = null;
		}
	}

	public void SetToy(string toyId)
	{
		DisconnectCurrentToy();
		ToyGenerator toyGenerator = toyGenerators.FirstOrDefault((ToyGenerator t) => t.Id == toyId);
		if (toyGenerator != null)
		{
			SetToy(toyGenerator);
		}
	}

	public void SetToy(ToyGenerator toyGenerator)
	{
		DisconnectCurrentToy();
		ShowToySettings = true;
		CurrentToy = toyGenerator.GenerateToyFunc(toyGenerator);
	}
}
