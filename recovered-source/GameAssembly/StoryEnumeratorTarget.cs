using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;

public class StoryEnumeratorTarget : StoryBranchTarget
{
	private Func<IEnumerator> _enumeratorGenerator;

	public StoryEnumeratorTarget(Func<IEnumerator> enumeratorGenerator)
	{
		_enumeratorGenerator = enumeratorGenerator;
	}

	public override void Activate(Action onFinished)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.StartCoroutine(_enumeratorGenerator());
	}

	public override string ToString()
	{
		return "Custom Enumerator";
	}

	public override Result CheckValidity(Dictionary<string, Block> blocks)
	{
		return Result.Success;
	}
}
