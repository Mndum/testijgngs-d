using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CookingMinigameInputs : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct Map1Actions(CookingMinigameInputs wrapper)
	{
		private CookingMinigameInputs m_Wrapper = wrapper;

		public InputAction LeftInput => m_Wrapper.m_Map1_LeftInput;

		public InputAction RightInput => m_Wrapper.m_Map1_RightInput;

		public InputAction ClickAnywhere => m_Wrapper.m_Map1_ClickAnywhere;

		public bool enabled => Get().enabled;

		public InputActionMap Get()
		{
			return m_Wrapper.m_Map1;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator InputActionMap(Map1Actions set)
		{
			return set.Get();
		}

		public void AddCallbacks(IMap1Actions instance)
		{
			if (instance != null && !m_Wrapper.m_Map1ActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_Map1ActionsCallbackInterfaces.Add(instance);
				LeftInput.started += instance.OnLeftInput;
				LeftInput.performed += instance.OnLeftInput;
				LeftInput.canceled += instance.OnLeftInput;
				RightInput.started += instance.OnRightInput;
				RightInput.performed += instance.OnRightInput;
				RightInput.canceled += instance.OnRightInput;
				ClickAnywhere.started += instance.OnClickAnywhere;
				ClickAnywhere.performed += instance.OnClickAnywhere;
				ClickAnywhere.canceled += instance.OnClickAnywhere;
			}
		}

		private void UnregisterCallbacks(IMap1Actions instance)
		{
			LeftInput.started -= instance.OnLeftInput;
			LeftInput.performed -= instance.OnLeftInput;
			LeftInput.canceled -= instance.OnLeftInput;
			RightInput.started -= instance.OnRightInput;
			RightInput.performed -= instance.OnRightInput;
			RightInput.canceled -= instance.OnRightInput;
			ClickAnywhere.started -= instance.OnClickAnywhere;
			ClickAnywhere.performed -= instance.OnClickAnywhere;
			ClickAnywhere.canceled -= instance.OnClickAnywhere;
		}

		public void RemoveCallbacks(IMap1Actions instance)
		{
			if (m_Wrapper.m_Map1ActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(IMap1Actions instance)
		{
			foreach (IMap1Actions map1ActionsCallbackInterface in m_Wrapper.m_Map1ActionsCallbackInterfaces)
			{
				UnregisterCallbacks(map1ActionsCallbackInterface);
			}
			m_Wrapper.m_Map1ActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public interface IMap1Actions
	{
		void OnLeftInput(InputAction.CallbackContext context);

		void OnRightInput(InputAction.CallbackContext context);

		void OnClickAnywhere(InputAction.CallbackContext context);
	}

	private readonly InputActionMap m_Map1;

	private List<IMap1Actions> m_Map1ActionsCallbackInterfaces = new List<IMap1Actions>();

	private readonly InputAction m_Map1_LeftInput;

	private readonly InputAction m_Map1_RightInput;

	private readonly InputAction m_Map1_ClickAnywhere;

	public InputActionAsset asset { get; }

	public InputBinding? bindingMask
	{
		get
		{
			return asset.bindingMask;
		}
		set
		{
			asset.bindingMask = value;
		}
	}

	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return asset.devices;
		}
		set
		{
			asset.devices = value;
		}
	}

	public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

	public IEnumerable<InputBinding> bindings => asset.bindings;

	public Map1Actions Map1 => new Map1Actions(this);

	public CookingMinigameInputs()
	{
		asset = InputActionAsset.FromJson("{\n    \"version\": 1,\n    \"name\": \"CookingMinigameInputs\",\n    \"maps\": [\n        {\n            \"name\": \"Map1\",\n            \"id\": \"559ec4be-95f3-4354-b2f8-f13fb567d291\",\n            \"actions\": [\n                {\n                    \"name\": \"Left Input\",\n                    \"type\": \"Button\",\n                    \"id\": \"54cac518-d489-4fec-9326-4ead19c91afa\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Right Input\",\n                    \"type\": \"Button\",\n                    \"id\": \"8d2e1865-72a9-4a34-9801-c1b02118c1f2\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Click Anywhere\",\n                    \"type\": \"Button\",\n                    \"id\": \"f44107fa-3f9a-4801-8a1e-f43119ce92a7\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"3f299e58-ceea-442b-83ed-5873e36f82ea\",\n                    \"path\": \"<Keyboard>/z\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Left Input\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3fdfdc96-98a3-454b-a594-038f664d78e4\",\n                    \"path\": \"<Keyboard>/x\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Right Input\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"One Modifier\",\n                    \"id\": \"18f3a09a-946a-4cc0-bbbf-402f2ea6d133\",\n                    \"path\": \"OneModifier\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Click Anywhere\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Modifier\",\n                    \"id\": \"66a84d41-0e91-4e9b-adfd-727b9f6aa658\",\n                    \"path\": \"<Mouse>/press\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Click Anywhere\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Binding\",\n                    \"id\": \"f0e0b34a-14ea-4ab5-91c8-c9a142c64397\",\n                    \"path\": \"<Mouse>/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Click Anywhere\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"One Modifier\",\n                    \"id\": \"c649b946-9ed5-464a-9f23-6843aa938d9c\",\n                    \"path\": \"OneModifier\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Click Anywhere\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Modifier\",\n                    \"id\": \"ed0f14a6-6757-415e-b910-48c26cd7c64a\",\n                    \"path\": \"<Touchscreen>/Press\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Click Anywhere\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Binding\",\n                    \"id\": \"b2ecedd1-e5a5-4b5f-9b18-f5680d9d1472\",\n                    \"path\": \"<Touchscreen>/primaryTouch/startPosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Click Anywhere\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": []\n}");
		m_Map1 = asset.FindActionMap("Map1", throwIfNotFound: true);
		m_Map1_LeftInput = m_Map1.FindAction("Left Input", throwIfNotFound: true);
		m_Map1_RightInput = m_Map1.FindAction("Right Input", throwIfNotFound: true);
		m_Map1_ClickAnywhere = m_Map1.FindAction("Click Anywhere", throwIfNotFound: true);
	}

	~CookingMinigameInputs()
	{
	}

	public void Dispose()
	{
		UnityEngine.Object.Destroy(asset);
	}

	public bool Contains(InputAction action)
	{
		return asset.Contains(action);
	}

	public IEnumerator<InputAction> GetEnumerator()
	{
		return asset.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Enable()
	{
		asset.Enable();
	}

	public void Disable()
	{
		asset.Disable();
	}

	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return asset.FindBinding(bindingMask, out action);
	}
}
