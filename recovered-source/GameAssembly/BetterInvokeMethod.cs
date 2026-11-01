using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Cysharp.Threading.Tasks;
using Fungus;
using UnityEngine;
using UnityEngine.Events;

[CommandInfo("My", "Better Invoke Method", "Invokes a method of a component via reflection. Supports passing multiple parameters and storing returned values in a Fungus variable.", 0)]
public class BetterInvokeMethod : Command
{
	public enum FunctionType
	{
		Normal,
		Enumerator,
		UniTask
	}

	[Tooltip("A description of what this command does. Appears in the command summary.")]
	[SerializeField]
	public string description = "";

	[Tooltip("GameObject containing the component method to be invoked")]
	[SerializeField]
	public GameObject targetObject;

	[HideInInspector]
	[Tooltip("Name of assembly containing the target component")]
	[SerializeField]
	public string targetComponentAssemblyName;

	[HideInInspector]
	[Tooltip("Full name of the target component")]
	[SerializeField]
	public string targetComponentFullname;

	[HideInInspector]
	[Tooltip("Display name of the target component")]
	[SerializeField]
	public string targetComponentText;

	[HideInInspector]
	[Tooltip("Name of target method to invoke on the target component")]
	[SerializeField]
	public string targetMethod;

	[HideInInspector]
	[Tooltip("Display name of target method to invoke on the target component")]
	[SerializeField]
	public string targetMethodText;

	[HideInInspector]
	[Tooltip("List of parameters to pass to the invoked method")]
	[SerializeField]
	public BetterInvokeMethodParameter[] methodParameters;

	[HideInInspector]
	[Tooltip("If true, store the return value in a flowchart variable of the same type.")]
	[SerializeField]
	public bool saveReturnValue;

	[HideInInspector]
	[Tooltip("Name of Fungus variable to store the return value in")]
	[SerializeField]
	public string returnValueVariableKey;

	[HideInInspector]
	[Tooltip("The type of the return value")]
	[SerializeField]
	public string returnValueType;

	[HideInInspector]
	[Tooltip("If true, list all inherited methods for the component")]
	[SerializeField]
	public bool showInherited;

	[HideInInspector]
	[Tooltip("The coroutine call behavior for methods that return IEnumerator")]
	[SerializeField]
	public CallMode callMode;

	protected Type componentType;

	protected Component objComponent;

	protected Type[] parameterTypes;

	protected MethodInfo objMethod;

	public Component TargetComponent => objComponent;

	public MethodInfo TargetMethod => objMethod;

	public virtual GameObject TargetObject => targetObject;

	public void Init()
	{
		if (!(componentType == null) || BetterInvokeMethodHelper.TryFindTypeByName(targetComponentAssemblyName, out componentType))
		{
			if (objComponent == null)
			{
				objComponent = targetObject.GetComponent(componentType);
			}
			if (parameterTypes == null)
			{
				parameterTypes = GetParameterTypes();
			}
			if (objMethod == null)
			{
				objMethod = UnityEventBase.GetValidMethodInfo(objComponent, targetMethod, parameterTypes);
			}
		}
	}

	private IEnumerator ExecuteCoroutine()
	{
		yield return StartCoroutine((IEnumerator)objMethod.Invoke(objComponent, GetParameterValues()));
		if (callMode == CallMode.WaitUntilFinished)
		{
			Continue();
		}
	}

	private async UniTask ExecuteUniTask()
	{
		await (UniTask)objMethod.Invoke(objComponent, GetParameterValues());
		if (callMode == CallMode.WaitUntilFinished)
		{
			Continue();
		}
	}

	private Type[] GetParameterTypes()
	{
		Type[] array = new Type[methodParameters.Length];
		for (int i = 0; i < methodParameters.Length; i++)
		{
			if (BetterInvokeMethodHelper.TryFindTypeByName(methodParameters[i].objValue.typeName, out var t))
			{
				array[i] = t;
			}
			else
			{
				array[i] = null;
			}
		}
		return array;
	}

	private object[] GetParameterValues()
	{
		object[] array = new object[methodParameters.Length];
		for (int i = 0; i < methodParameters.Length; i++)
		{
			BetterInvokeMethodParameter betterInvokeMethodParameter = methodParameters[i];
			object value;
			if (string.IsNullOrEmpty(betterInvokeMethodParameter.variableKey))
			{
				array[i] = betterInvokeMethodParameter.objValue.GetValue();
			}
			else if (TryGetVariable(betterInvokeMethodParameter.variableKey, out value))
			{
				array[i] = value;
			}
			else
			{
				array[i] = null;
			}
		}
		return array;
	}

	private bool TryGetVariable(string key, out object value)
	{
		value = null;
		Variable variable = GetFlowchart().GetVariable(key);
		if (variable == null)
		{
			return false;
		}
		value = variable.GetValue();
		return true;
	}

	private void SetVariable(string key, object value)
	{
		Variable variable = GetFlowchart().GetVariable(key);
		if (variable == null)
		{
			Debug.LogError($"Variable \"{key}\" not found! Cannot set it to {value}");
		}
		variable.Apply(SetOperator.Assign, value);
	}

	private FunctionType DetermineFunctionType()
	{
		if (returnValueType == "System.Collections.IEnumerator")
		{
			return FunctionType.Enumerator;
		}
		if (returnValueType == "Cysharp.Threading.Tasks.UniTask")
		{
			return FunctionType.UniTask;
		}
		return FunctionType.Normal;
	}

	public override void OnEnter()
	{
		Init();
		try
		{
			if (targetObject == null || string.IsNullOrEmpty(targetComponentAssemblyName) || string.IsNullOrEmpty(targetMethod))
			{
				Continue();
				return;
			}
			FunctionType functionType = DetermineFunctionType();
			if (functionType != FunctionType.Normal)
			{
				if (functionType == FunctionType.Enumerator)
				{
					StartCoroutine(ExecuteCoroutine());
				}
				else
				{
					ExecuteUniTask().Forget();
				}
				if (callMode == CallMode.Continue)
				{
					Continue();
				}
				else if (callMode == CallMode.Stop)
				{
					StopParentBlock();
				}
			}
			else
			{
				object value = objMethod.Invoke(objComponent, GetParameterValues());
				if (saveReturnValue)
				{
					SetVariable(returnValueVariableKey, value);
				}
				Continue();
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			Continue();
		}
	}

	public override Color GetButtonColor()
	{
		return new Color32(235, 191, 217, 255);
	}

	public override string GetSummary()
	{
		if (targetObject == null)
		{
			return "Error: targetObject is not assigned";
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(targetObject.name);
			stringBuilder.Append(".");
			stringBuilder.Append(targetComponentText);
			stringBuilder.Append(".");
			stringBuilder.Append(targetMethod);
			stringBuilder.Append("(");
			for (int i = 0; i < methodParameters.Length; i++)
			{
				BetterInvokeMethodParameter betterInvokeMethodParameter = methodParameters[i];
				stringBuilder.Append(ReflectionHelper.GetType(betterInvokeMethodParameter.objValue.typeName).Name);
				stringBuilder.Append("(");
				if (string.IsNullOrWhiteSpace(betterInvokeMethodParameter.variableKey))
				{
					if (BetterInvokeMethodHelper.IsUnityObject(betterInvokeMethodParameter.objValue.typeName))
					{
						if (betterInvokeMethodParameter.objValue.objectValue != null)
						{
							stringBuilder.Append(betterInvokeMethodParameter.objValue.objectValue.name);
						}
						else
						{
							stringBuilder.Append("null");
						}
					}
					else
					{
						stringBuilder.Append(betterInvokeMethodParameter.objValue.value);
					}
				}
				else
				{
					stringBuilder.Append("var.");
					stringBuilder.Append(betterInvokeMethodParameter.variableKey);
				}
				stringBuilder.Append(")");
				if (i != methodParameters.Length - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(")");
			if (!string.IsNullOrEmpty(description))
			{
				stringBuilder.Append(" //");
				stringBuilder.Append(description);
			}
			return stringBuilder.ToString();
		}
	}
}
