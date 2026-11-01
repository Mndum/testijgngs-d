using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class SingletonLoadableMonoBehaviour<T> : MonoBehaviour where T : SingletonLoadableMonoBehaviour<T>
{
	private static T _instance;

	public static T Instance => _instance;

	public bool IsLoaded { get; private set; }

	public abstract Type[] GetDependenciesArray();

	public void Awake()
	{
		_instance = (T)this;
		LoadingManager.RegisterToLoad(new LoadingManager.LoadableReferenceSingleton<T>(GetDependenciesArray(), null, (T)this));
	}

	protected abstract UniTask LoadInner(IProgressManager progressManager);

	public async UniTask Load(IProgressManager progressManager)
	{
		await LoadInner(progressManager);
		IsLoaded = true;
	}
}
