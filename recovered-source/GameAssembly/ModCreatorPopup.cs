using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IC.AutomaticReference;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ModCreatorPopup : PrefabPopup<ModCreatorPopup>, IPopupCloseScriptHandler
{
	[SerializeField]
	private ButtonList _topButtonList;

	[SerializeField]
	private SortableItemList _itemItemList;

	private SortableItemList.ItemListAdapter<ItemPrefab> _itemItemListAdapter;

	[SerializeField]
	private SortableItemList _textureList;

	private SortableItemList.ItemListAdapter<PackedTexturesContainer> _textureListAdapter;

	[SerializeField]
	private RectTransform _leftRect;

	[SerializeField]
	private Button _exportModButton;

	[FormerlySerializedAs("_createItemButton")]
	[SerializeField]
	private Button _newItemButton;

	[SerializeField]
	private RectTransform _infoRect;

	[SerializeField]
	private Button _otherSelectedButton;

	private TextMeshProUGUI _descriptionText;

	private Button _genericDeleteButton;

	private RectTransform _packedTextureInfoRectTransform;

	private TextMeshProUGUI _packedTextureInfo;

	private TextMeshProUGUI _packedTextureAdditionalInfo;

	private Image _packedTexturePreviewImage;

	private int _currentPackedTexture;

	private Button _previousPackedTextureButton;

	private Button _deletePackedTextureButton;

	private Button _nextPackedTextureButton;

	private Button _specialPackedTextureButton;

	[SerializeField]
	private RectTransform _rightRect;

	[FormerlySerializedAs("_createTextureButton")]
	[SerializeField]
	private Button _createPackedTextureContainerButton;

	[FormerlySerializedAs("_otherTexturesButton")]
	[SerializeField]
	private Button _otherPackedTextureContainersButton;

	public List<ItemPrefab> _itemPrefabs = new List<ItemPrefab>();

	private ModCreationLive2DControllersContainer _live2DControllers;

	public override bool DontDrawPrevious => true;

	public override bool CanClose()
	{
		return false;
	}

	public void CloseFromPopupCloseScript()
	{
		UiOverlay.Instance.SimplePopup("Closing the mod creation window!", "Are you sure you want to close this window?(All unsaved data will be lost forever!)", () =>
		{
			CloseFromUIOverlay();
			SingletonMonoBehaviour<AutomaticReferenceSingleton>.Instance.ForceCleaning();
		});
	}

	private void FindReferences()
	{
		_newItemButton.onClick.SetListener(CreateItemButtonClicked);
		Transform transform = _infoRect.Find("Inner info");
		_descriptionText = transform.Find("Description").GetComponent<ScrollViewTMP>().TextMeshProUGUI;
		_packedTextureInfoRectTransform = (RectTransform)transform.Find("Texture");
		_packedTextureInfo = _packedTextureInfoRectTransform.Find("TextureInfo").GetComponent<TextMeshProUGUI>();
		_otherSelectedButton.onClick.SetListener(OtherSelectedButtonClicked);
		_packedTextureAdditionalInfo = _packedTextureInfoRectTransform.Find("TextureAdditionalInfo").GetComponent<TextMeshProUGUI>();
		_packedTexturePreviewImage = _packedTextureInfoRectTransform.Find("Image").GetComponent<Image>();
		_specialPackedTextureButton = _packedTextureInfoRectTransform.Find("Special").GetComponent<Button>();
		_specialPackedTextureButton.onClick.SetListener(SpecialButtonClicked);
		_deletePackedTextureButton = _packedTextureInfoRectTransform.Find("Delete").GetComponent<Button>();
		_deletePackedTextureButton.onClick.SetListener(DeleteTextureButtonClicked);
		_previousPackedTextureButton = _packedTextureInfoRectTransform.Find("Left").GetComponent<Button>();
		_previousPackedTextureButton.onClick.SetListener(PreviousPackedTextureButtonClicked);
		_nextPackedTextureButton = _packedTextureInfoRectTransform.Find("Right").GetComponent<Button>();
		_nextPackedTextureButton.onClick.SetListener(NextPackedTextureButtonClicked);
		_createPackedTextureContainerButton.onClick.SetListener(CreatePackedTextureContainerButtonClicked);
		_otherPackedTextureContainersButton.onClick.SetListener(OtherPackedTextureContainersButtonClicked);
		_exportModButton.onClick.SetListener(ExportModButtonClicked);
	}

	private void OtherSelectedButtonClicked()
	{
		if (_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var _) && _textureListAdapter.TryGetCurrentlyFocusedItem(out var texture))
		{
			List<PopupChoice> choices = new List<PopupChoice>
			{
				new PopupChoice("Export selected packed texture", () =>
				{
					ExportPackedTextureContainer(texture);
				}),
				new PopupChoice("Merge with a different packed texture", () =>
				{
					MergePackedTextureContainerPopup(texture);
				})
			};
			UiOverlay.Instance.OpenContextMenuPopup(choices);
		}
	}

	private void MergePackedTextureContainerPopup(PackedTexturesContainer texture)
	{
		if (!_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var item) || item.Textures == null)
		{
			return;
		}
		if (item.Textures.Length <= 1)
		{
			UiOverlay.Instance.OkPopup("Error!", "There isn't any packed texture container you can merge with!");
			return;
		}
		CompoundItemList.StartBuilder<PackedTexturesContainer, StringAdvancedItemListItemPrefab>().WithToStringOnly((PackedTexturesContainer a, StringRepresentationHelperEnum b) => a.GetNameUsingAffectedControllersId()).WithAlphabeticalSortingOnly()
			.WithAllFolder()
			.WithDefaultStringAdvancedPrefabSetItem()
			.WithDefaultListItemPool()
			.Build(item.Textures.Where((PackedTexturesContainer t) => t != texture).ToFrozenDataSource())
			.ToChooseItemsComplexPopupBuilderGenericString()
			.WithSingularStartingSelectionSingleItemAllowed()
			.WithSuccessfulOnlyOnFinished((Selection<PackedTexturesContainer> selection) =>
			{
				if (selection.TryGetFirstItem(out var item2))
				{
					item.Textures = item.Textures.RemoveFirstArrayCopy(texture);
					item2.PackedTextures.AddRange(texture.PackedTextures);
					item2.Sort();
					_textureList.UpdateAll();
					_textureListAdapter.Focus(item2);
					UiOverlay.Instance.OkPopup("Done", "Merged selected packed texture!");
				}
			})
			.OpenPopup("Choose packed texture container to merge with", "Choose which packed texture container you want to merge selected one with!", allowClosing: true, showCancel: true);
	}

	private void OtherPackedTextureContainersButtonClicked()
	{
		if (_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var _))
		{
			List<PopupChoice> choices = new List<PopupChoice>
			{
				new PopupChoice("Optimize selected item's textures", OptimizePackedTexturesPopup),
				new PopupChoice("Validate selected item's textures", ValidatePackedTexturesPopup),
				new PopupChoice("Extract bounds guides selected item's textures", ExtractBoundGuidesPackedTexturesPopup)
			};
			UiOverlay.Instance.OpenContextMenuPopup(choices);
		}
	}

	private void OptimizePackedTexturesPopup()
	{
		if (_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var item) && item.Textures != null && item.Textures.Length != 0)
		{
			UiOverlay.Instance.SimplePopup("Are you sure?", "Are you sure you want to optimize the textures in " + item.Name + "?\nOptimization will merge all drawables with the same:\n-Layer\n-Name\n-ColorIndex\n-Content\n", () =>
			{
				PackedTexturesContainer[] packedTexturesContainers = item.Textures;
				Result result = PackedTexturesContainer.Optimize(ref packedTexturesContainers);
				item.Textures = packedTexturesContainers;
				_itemItemList.UpdateAll();
				_textureList.UpdateAll();
				UiOverlay.Instance.OkPopup("Result", "Result: " + (result.IsSuccess ? "Success" : "Failure") + "\n" + result.GetDescription());
			}, () =>
			{
			});
		}
	}

	private void ValidatePackedTexturesPopup()
	{
		if (!_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		HashSet<ControllerEnum> set;
		using (item.Textures.SelectMany((PackedTexturesContainer t) => t.GetAffectedControllers()).ToHashSetPooled(out set))
		{
			if (item.IsEquipable(out var slotEquipData))
			{
				List<string> elements;
				using (slotEquipData.FillActiveElementsDefaultPooled(out elements))
				{
					stringBuilder.AppendLine("\nSlot \"" + slotEquipData.TargetSlot.Id + "\":\n");
					using ModCreationLive2DControllersContainer modCreationLive2DControllersContainer = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetModCreationData("Quick mod with items");
					foreach (ControllerEnum item2 in set)
					{
						Live2DController controller = modCreationLive2DControllersContainer.GetController(item2);
						string targetId = item2.GetControllerId();
						List<PackedTexturesContainer.PackedDrawable> rightItems = item.Textures.SelectMany((PackedTexturesContainer x) => x.GetPackedDrawablesByTarget(targetId)).ToList();
						stringBuilder.AppendLine("ControllerId: " + controller.ControllerEnum.GetControllerId());
						foreach (string item3 in elements)
						{
							Live2DController.Live2DControllerElement elementById = controller.GetElementById(item3);
							if (elementById != null)
							{
								List<Live2DController.Part> leftItems = elementById.AllPartsNonSegmented(controller).ToList();
								List<string> list = (from t in GlobalFunctions.LeftOuterJoin(leftItems, (Live2DController.Part t) => t.FullName, rightItems, (PackedTexturesContainer.PackedDrawable t) => t.Name)
									where t.Item2 == null
									select t.Item1.FullName).ToList();
								List<string> list2 = (from t in GlobalFunctions.RightOuterJoin(leftItems, (Live2DController.Part t) => t.FullName, rightItems, (PackedTexturesContainer.PackedDrawable t) => t.Name)
									where t.Item1 == null
									select t.Item2.Name).ToList();
								stringBuilder.AppendLine("\nElement \"" + item3 + "\":\n");
								if (list.Count > 0)
								{
									stringBuilder.AppendLine(("Missing drawables: " + string.Join(", ", list)).TMPColorSurround(Color.red));
								}
								else
								{
									stringBuilder.AppendLine("All required drawables present!".TMPColorSurround(Color.green));
								}
								if (list2.Count > 0)
								{
									stringBuilder.AppendLine(("Added drawables: " + string.Join(", ", list2)).TMPColorSurround(Color.yellow));
								}
							}
							else
							{
								stringBuilder.AppendLine("Special slot \"" + slotEquipData.TargetSlot.Id + "\"");
							}
						}
						stringBuilder.AppendLine();
					}
				}
			}
			UiOverlay.Instance.OkPopup("Validation results", stringBuilder.ToString());
		}
	}

	private void ExtractBoundGuidesPackedTexturesPopup()
	{
		if (!_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			return;
		}
		if (item.Textures == null)
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_Error, "No textures found in this item!", null);
			return;
		}
		string tmpModPath = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.TmpModPath;
		SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.PrepareTmpPath();
		for (int i = 0; i < item.Textures.Length; i++)
		{
			PackedTexturesContainer packedTexturesContainer = item.Textures[i];
			foreach (IGrouping<int, PackedTexturesContainer.PackedTexture> texturesInLayer in packedTexturesContainer.TexturesInLayers)
			{
				int key = texturesInLayer.Key;
				foreach (var (packedTexture, index) in texturesInLayer.Select((PackedTexturesContainer.PackedTexture texture, int item2) => (texture: texture, i: item2)))
				{
					if (packedTexture.TryDrawBounds(out var boundsTexture, GetOrCreateLive2DControllers()))
					{
						string text = packedTexturesContainer.TextureNameGenerator("bounds" + (i + 1) + "_" + packedTexture.TargetsNames, index, key);
						File.WriteAllBytes(tmpModPath + "/" + text, boundsTexture.EncodeToPNG());
						UnityEngine.Object.Destroy(boundsTexture);
					}
				}
			}
		}
		byte[] data = MyZipUtility.CreateZipFile(tmpModPath);
		UiOverlay.Instance.FileSavePopup("guides.zip", data, null);
	}

	private void ExportModButtonClicked()
	{
		Mod.CreateItemMod(_itemPrefabs).SaveToFilePopup();
	}

	private void DeleteTextureButtonClicked()
	{
		if (_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var _) && _textureListAdapter.TryGetCurrentlyFocusedItem(out var texture) && texture.PackedTextures.TryGetValue(_currentPackedTexture, out var packedTexture))
		{
			UiOverlay.Instance.SimplePopup("Deleting the texture", "Are you sure you want to delete the currently selected texture from the packed texture \"" + texture.GetNameUsingAffectedControllersId() + "\"?\n", () =>
			{
				texture.PackedTextures.Remove(packedTexture);
				_textureList.UpdateAll();
			});
		}
	}

	private void SpecialButtonClicked()
	{
		if (!_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var item) || !_textureListAdapter.TryGetCurrentlyFocusedItem(out var container))
		{
			return;
		}
		List<PopupChoice> list = new List<PopupChoice>();
		if (container.PackedTextures.TryGetValue(_currentPackedTexture, out var packedTexture))
		{
			int currentLayer = packedTexture.Layer;
			List<PackedTexturesContainer.PackedTexture> layerTextures = container.PackedTextures.Where((PackedTexturesContainer.PackedTexture t) => t.Layer == currentLayer).ToList();
			int offset = 1;
			list.Add(new PopupChoice
			{
				Text = "Change packed texture layer",
				Action = () =>
				{
					UiOverlay.Instance.InputPopup("New layer number", "Choose what layer to move this packed texture to. (Positive number or 0)", (string s) =>
					{
						if (int.TryParse(s, out var result) && result >= 0)
						{
							packedTexture.Layer = result;
							container.Sort();
							_textureList.UpdateAll();
						}
						else
						{
							UiOverlay.Instance.OkPopup("Error", "\"" + s + "\" is not a valid layer!");
						}
					}, currentLayer.ToString(), TMP_InputField.ContentType.IntegerNumber);
				}
			});
			list.Add(new PopupChoice
			{
				Text = "Duplicate packed texture",
				Action = () =>
				{
					UiOverlay.Instance.SimplePopup("Duplicating the packed texture", "Are you sure you want to duplicate the currently selected packed texture?\n", () =>
					{
						container.DuplicateTexture(packedTexture, offset);
						_textureList.UpdateAll();
					});
				}
			});
			list.Add(new PopupChoice
			{
				Text = "Export image",
				Action = () =>
				{
					UiOverlay.Instance.FileSavePopup("texture.png", packedTexture.GetTexture().DeCompressAndConvertToPng().PngData, null);
				}
			});
			list.Add(new PopupChoice
			{
				Text = "Extract bounds guide",
				Action = () =>
				{
					if (packedTexture.TryDrawBounds(out var boundsTexture, GetOrCreateLive2DControllers()))
					{
						UiOverlay.Instance.FileSavePopup("bounds.png", boundsTexture.EncodeToPNG(), null);
						UnityEngine.Object.Destroy(boundsTexture);
					}
					else
					{
						UiOverlay.Instance.OkPopup("Error!", "Something went wrong! Unable to draw bounds!");
					}
				}
			});
			list.Add(new PopupChoice
			{
				Text = "Replace image",
				Action = () =>
				{
					UiOverlay.Instance.FileLoadPopup("Choose the png file to use!", (MyFileBrowserSingleton.UploadResult t) =>
					{
						if (t.GotFiles)
						{
							try
							{
								byte[] data = t.Results.First().Data;
								Texture2D texture = ImageUtility.LoadTexture(data);
								Texture2D texture2 = packedTexture.GetTexture();
								if (texture.width == texture2.width && texture.height == texture2.height)
								{
									packedTexture.SetRuntimeTexture(texture);
									SyncPackedTextureInfo(container);
								}
								else
								{
									UiOverlay.Instance.SimplePopup("Replacing the image", "New image has a different size. Are you sure you want to replace the old one with the new one?", () =>
									{
										packedTexture.SetRuntimeTexture(texture);
										SyncPackedTextureInfo(container);
									});
								}
							}
							catch (Exception ex)
							{
								Debug.LogException(ex, this);
								UiOverlay.Instance.OkPopup("Error!", "Something went wrong while loading the image.\nLog\n" + ex.Message);
							}
						}
					}, new List<MyFileBrowserSingleton.FileType>
					{
						new MyFileBrowserSingleton.FileType("Png", ".png")
					});
				}
			});
			if (layerTextures.Count > 1)
			{
				list.Add(new PopupChoice
				{
					Text = "Merge layer",
					Action = () =>
					{
						UiOverlay.Instance.SimplePopup("Merging the layer", $"Are you sure you want to merge the layer: {currentLayer} targets: {packedTexture.TargetsNames}?\n", () =>
						{
							container.JoinLayer(currentLayer, packedTexture.Targets);
							_textureList.UpdateAll();
						});
					}
				});
				list.Add(new PopupChoice
				{
					Text = "Duplicate layer",
					Action = () =>
					{
						UiOverlay.Instance.SimplePopup("Duplicating the layer", $"Are you sure you want to duplicate the layer of the currently selected packed texture {packedTexture.Layer}?\n" + "This layer has " + StringUtils.NounDisplay(layerTextures.Count, "packed texture") + ".\n" + $"New layer will be {currentLayer + offset}.", () =>
						{
							container.DuplicateLayer(packedTexture.Layer, offset);
							_textureList.UpdateAll();
						});
					}
				});
			}
			if (packedTexture.PackedDrawables.Count > 1)
			{
				list.Add(new PopupChoice
				{
					Text = "Split packed texture",
					Action = () =>
					{
						UiOverlay.Instance.SimplePopup("Splitting the texture", "Are you sure you want to split the currently selected packed texture into two?\n", () =>
						{
							CompoundItemList.StartBuilder<PackedTexturesContainer.PackedDrawable, StringAdvancedItemListItemPrefab>().WithToStringOnly((PackedTexturesContainer.PackedDrawable a, StringRepresentationHelperEnum b) => a.Name).WithAlphabeticalSortingOnly()
								.WithAllFolder()
								.WithDefaultStringAdvancedPrefabSetItem()
								.WithDefaultListItemPool()
								.Build(packedTexture.PackedDrawables.ToDirectDataSource())
								.ToChooseItemsComplexPopupBuilderGenericString()
								.WithSingularStartingSelection()
								.WithSuccessfulOnlyOnFinished((Selection<PackedTexturesContainer.PackedDrawable> selection) =>
								{
									container.SplitTexture(selection.GetAllItems().ToList(), packedTexture);
									_textureList.UpdateAll();
								})
								.OpenPopup("Splitting the texture", "Choose what drawables to extract from the packed texture", allowClosing: true, showCancel: true);
						});
					}
				});
			}
			list.Add(new PopupChoice
			{
				Text = "Change targets",
				Action = () =>
				{
					UiOverlay.Instance.SimplePopup("Are you sure?", "Are you sure you want to change the targets of this texture? Only do it if you know what you are doing. Otherwise you might break this item.", () =>
					{
						ChooseLive2DControllers("Choose targets", "Choose the new targets. Current targets: " + packedTexture.TargetsNames, (List<Live2DController> c) =>
						{
							packedTexture.Targets = c.Select((Live2DController t) => t.ControllerEnum.GetControllerId()).ToArray();
							_textureList.UpdateAll();
						}, null);
					});
				}
			});
			if (container.PackedTextures.Count > 1)
			{
				list.Add(new PopupChoice
				{
					Text = "Extract to a separate packed texture",
					Action = () =>
					{
						UiOverlay.Instance.SimplePopup("Are you sure?", "Are you sure you want extract this texture to it's own packed texture?", () =>
						{
							container.PackedTextures.Remove(packedTexture);
							container.Sort();
							PackedTexturesContainer packedTexturesContainer = PackedTexturesContainer.CreateEmpty();
							packedTexturesContainer.PackedTextures.Add(packedTexture);
							item.AddTexture(packedTexturesContainer);
							_textureList.UpdateAll();
						});
					}
				});
			}
		}
		UiOverlay.Instance.OpenContextMenuPopup(list);
	}

	private void NextPackedTextureButtonClicked()
	{
		if (_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var _) && _textureListAdapter.TryGetCurrentlyFocusedItem(out var item2) && _currentPackedTexture < item2.PackedTextures.Count - 1)
		{
			_currentPackedTexture++;
			SyncPackedTextureInfo(item2);
		}
	}

	private void PreviousPackedTextureButtonClicked()
	{
		if (_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var _) && _textureListAdapter.TryGetCurrentlyFocusedItem(out var item2) && _currentPackedTexture > 0)
		{
			_currentPackedTexture--;
			SyncPackedTextureInfo(item2);
		}
	}

	private void SyncPackedTextureInfo(PackedTexturesContainer itemTexture)
	{
		if (itemTexture.PackedTextures.Count > 0)
		{
			_currentPackedTexture %= itemTexture.PackedTextures.Count;
			PackedTexturesContainer.PackedTexture packedTexture = itemTexture.PackedTextures[_currentPackedTexture];
			_previousPackedTextureButton.interactable = _currentPackedTexture - 1 >= 0;
			_nextPackedTextureButton.interactable = _currentPackedTexture + 1 < itemTexture.PackedTextures.Count;
			_packedTextureInfo.text = $"Texture {_currentPackedTexture + 1}/{itemTexture.PackedTextures.Count} Layer {packedTexture.Layer}";
			Texture2D texture = packedTexture.GetTexture();
			_packedTextureAdditionalInfo.text = $"Targets: {packedTexture.TargetsNames} Size: {texture.width}x{texture.height}";
			Button deletePackedTextureButton = _deletePackedTextureButton;
			bool interactable = (_specialPackedTextureButton.interactable = true);
			deletePackedTextureButton.interactable = interactable;
			_packedTexturePreviewImage.sprite = ImageUtility.ConvertTextureToSprite(texture, 100f, SpriteMeshType.FullRect);
			_packedTexturePreviewImage.preserveAspect = true;
			_packedTexturePreviewImage.gameObject.SetActive(value: true);
		}
		else
		{
			Button previousPackedTextureButton = _previousPackedTextureButton;
			Button nextPackedTextureButton = _nextPackedTextureButton;
			Button deletePackedTextureButton2 = _deletePackedTextureButton;
			bool flag2 = (_specialPackedTextureButton.interactable = false);
			bool flag4 = (deletePackedTextureButton2.interactable = flag2);
			bool interactable = (nextPackedTextureButton.interactable = flag4);
			previousPackedTextureButton.interactable = interactable;
			_packedTextureInfo.text = "No textures!";
			_packedTextureAdditionalInfo.text = "";
			_packedTexturePreviewImage.gameObject.SetActive(value: false);
		}
	}

	private void AddItemTexture(ItemPrefab item, PackedTexturesContainer container)
	{
		if (container != null)
		{
			item.AddTexture(container);
			_textureList.UpdateAll();
		}
		else
		{
			UiOverlay.Instance.OkPopup("Failed!", "Container was empty and couldn't be added!");
		}
	}

	public void ChooseLive2DController(string title, string description, Action<Live2DController> yesAction)
	{
		CompoundItemList.StartBuilder<Live2DController, StringAdvancedItemListItemPrefab>().WithToStringOnly((Live2DController a, StringRepresentationHelperEnum b) => a.ControllerEnum.GetControllerId()).WithBasicToStringEverything()
			.Build(GetOrCreateLive2DControllers().Controllers.ToDirectDataSource())
			.ToChooseItemsComplexPopupBuilderGenericString()
			.WithSingularStartingSelectionSingleItemAllowed()
			.WithSuccessfulOnlyOnFinished((Selection<Live2DController> selection) =>
			{
				if (selection.TryGetFirstItem(out var item))
				{
					yesAction?.Invoke(item);
				}
			})
			.OpenPopup(title, description, allowClosing: true, showCancel: true);
	}

	public void ChooseLive2DControllers(string title, string description, Action<List<Live2DController>> yesAction, Action noAction)
	{
		CompoundItemList.StartBuilder<Live2DController, StringAdvancedItemListItemPrefab>().WithToStringOnly((Live2DController a, StringRepresentationHelperEnum b) => a.ControllerEnum.GetControllerId()).WithBasicToStringEverything()
			.Build(GetOrCreateLive2DControllers().Controllers.ToDirectDataSource())
			.ToChooseItemsComplexPopupBuilderGenericString()
			.WithSingularStartingSelection()
			.WithSuccessfulOnlyOnFinished((Selection<Live2DController> selection) =>
			{
				if (selection.SelectedItemsCount > 0)
				{
					yesAction?.Invoke(selection.GetAllItems().ToList());
				}
			})
			.OpenPopup(title, description, allowClosing: true, showCancel: true);
	}

	public void MakeItemTextureFromParts(ItemPrefab item, Live2DController controller, List<Live2DController.Part> allParts)
	{
		if (allParts.Count > 1)
		{
			UiOverlay.Instance.Popup("Creating a packed texture", "Do you want to choose a subset of drawables or use them all?", new List<PopupChoice>
			{
				new PopupChoice("Choose", () =>
				{
					CompoundItemList.StartBuilder<Live2DController.Part, StringAdvancedItemListItemPrefab>().WithToStringOnly((Live2DController.Part a, StringRepresentationHelperEnum b) => a.FullName).WithBasicToStringEverything()
						.Build(allParts.ToDirectDataSource())
						.ToChooseItemsComplexPopupBuilderGenericString()
						.WithSingularStartingSelection()
						.WithSuccessfulOnlyOnFinished((Selection<Live2DController.Part> selection) =>
						{
							if (selection.SelectedItemsCount > 0)
							{
								PackedTexturesContainer container2 = SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.PackRenderers(selection.GetAllItems().ToList(), separateTextures: false, 0);
								AddItemTexture(item, container2);
							}
						})
						.OpenPopup("Choose drawables", "Choose drawables to include in the texture", allowClosing: true, showCancel: true);
				}),
				new PopupChoice("Use them all", () =>
				{
					PackedTexturesContainer container2 = SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.PackRenderers(allParts, separateTextures: false, 0);
					AddItemTexture(item, container2);
				})
			});
		}
		else
		{
			PackedTexturesContainer container = SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.PackRenderers(allParts, separateTextures: false, 0);
			AddItemTexture(item, container);
		}
	}

	private void CreatePackedTextureContainerButtonClicked()
	{
		if (!_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			return;
		}
		UiOverlay.Instance.OpenContextMenuPopup(new List<PopupChoice>
		{
			new PopupChoice("Generate", () =>
			{
				UiOverlay.Instance.OpenContextMenuPopup(new List<PopupChoice>
				{
					new PopupChoice
					{
						Text = "From drawables (advanced)",
						Action = () =>
						{
							ChooseLive2DController("Controller", "Choose controller to get drawables from.", (Live2DController controller) =>
							{
								CompoundItemList.StartBuilder<Live2DController.Part, StringAdvancedItemListItemPrefab>().WithToStringOnly((Live2DController.Part a, StringRepresentationHelperEnum b) => a.FullName).WithBasicToStringEverything()
									.Build(controller.GetParts().ToFrozenDataSource())
									.ToChooseItemsComplexPopupBuilderGenericString()
									.WithSingularStartingSelection()
									.WithSuccessfulOnlyOnFinished((Selection<Live2DController.Part> selection) =>
									{
										if (selection.SelectedItemsCount > 0)
										{
											MakeItemTextureFromParts(item, controller, selection.GetAllItems().ToList());
										}
										else
										{
											UiOverlay.Instance.OkPopup("No drawables!", "Chosen controller/s don't have any drawables!");
										}
									})
									.OpenPopup("Choose items", "Choose drawables to use", allowClosing: true, showCancel: true);
							});
						}
					},
					new PopupChoice
					{
						Text = "From group (hard)",
						Action = () =>
						{
							ChooseLive2DController("Controller", "Choose controller to get groups from.", (Live2DController controller) =>
							{
								CompoundItemList.StartBuilder<KeyValuePair<Live2DController.ColoringGroupEnum, Live2DController.ColoringGroup>, StringAdvancedItemListItemPrefab>().WithToStringOnly((KeyValuePair<Live2DController.ColoringGroupEnum, Live2DController.ColoringGroup> a, StringRepresentationHelperEnum b) => a.Key.ToString()).WithBasicToStringEverything()
									.Build(controller.GetGroups().ToFrozenDataSource())
									.ToChooseItemsComplexPopupBuilderGenericString()
									.WithSingularStartingSelection()
									.WithSuccessfulOnlyOnFinished((Selection<KeyValuePair<Live2DController.ColoringGroupEnum, Live2DController.ColoringGroup>> selection) =>
									{
										List<Live2DController.Part> list = selection.GetAllItems().SelectMany((KeyValuePair<Live2DController.ColoringGroupEnum, Live2DController.ColoringGroup> c) => c.Value.Parts).ToList();
										if (list.Count > 0)
										{
											MakeItemTextureFromParts(item, controller, list);
										}
										else
										{
											UiOverlay.Instance.OkPopup("No drawables!", "Chosen group/s don't have any drawables!");
										}
									})
									.OpenPopup("Choose items", "Choose groups to use drawables from.", allowClosing: true, showCancel: true);
							});
						}
					},
					new PopupChoice
					{
						Text = "From element (simple)",
						Action = () =>
						{
							ChooseLive2DController("Controller", "Choose controller to get slots from.", (Live2DController controller) =>
							{
								CompoundItemList.StartBuilder<Live2DController.Live2DControllerElement, StringAdvancedItemListItemPrefab>().WithToStringOnly((Live2DController.Live2DControllerElement a, StringRepresentationHelperEnum b) => a.id).WithBasicToStringEverything()
									.Build(controller.GetAllElements().ToFrozenDataSource())
									.ToChooseItemsComplexPopupBuilderGenericString()
									.WithSingularStartingSelection()
									.WithSuccessfulOnlyOnFinished((Selection<Live2DController.Live2DControllerElement> selection) =>
									{
										List<Live2DController.Part> list = selection.GetAllItems().SelectMany((Live2DController.Live2DControllerElement c) => c.AllPartsNonSegmented(controller)).ToList();
										if (list.Count > 0)
										{
											MakeItemTextureFromParts(item, controller, list);
										}
										else
										{
											UiOverlay.Instance.OkPopup("No drawables!", "Chosen group/s don't have any drawables!");
										}
									})
									.OpenPopup("Choose an element", "Choose a slot to use drawables from.", allowClosing: true, showCancel: true);
							});
						}
					},
					new PopupChoice
					{
						Text = "From item (simple)",
						Action = () =>
						{
							CompoundItemList.StartBuilder<ItemPrefab, StringAdvancedItemListItemPrefab>().WithDefaultItemPrefabName().WithDefaultItemPrefabSorting()
								.WithDefaultItemPrefabFolders()
								.WithDefaultStringAdvancedPrefabSetItem()
								.WithDefaultListItemPool()
								.Build((from x in ItemPrefab.ItemPrefabManager.AllItems
									where x.prefab.Equipable
									select x.prefab).ToFrozenDataSource())
								.ToChooseItemsComplexPopupBuilderItemPrefab()
								.WithSingularStartingSelection()
								.WithSuccessfulOnlyOnFinished((Selection<ItemPrefab> selection) =>
								{
									if (selection.TryGetFirstItem(out var item2))
									{
										ItemPrefab itemPrefab = item2.CloneWithTextures(GetOrCreateLive2DControllers());
										if (itemPrefab.HasAnyTextures)
										{
											CompoundItemList.StartBuilder<PackedTexturesContainer, StringAdvancedItemListItemPrefab>().WithToStringOnly((PackedTexturesContainer a, StringRepresentationHelperEnum t) => a.GetNameUsingAffectedControllersId()).WithBasicToStringEverything()
												.Build(itemPrefab.Textures.ToDirectDataSource())
												.ToChooseItemsComplexPopupBuilderGenericString()
												.WithSingularStartingSelection()
												.WithSuccessfulOnlyOnFinished((Selection<PackedTexturesContainer> selection2) =>
												{
													foreach (PackedTexturesContainer allItem in selection2.GetAllItems())
													{
														AddItemTexture(item, allItem);
													}
												})
												.OpenPopup("Choose packed textures", "Choose which packed textures from " + itemPrefab.Name + " you want to copy.", allowClosing: true, showCancel: true);
										}
										else
										{
											UiOverlay.Instance.OkPopup("No packed textures", "This item doesn't have any packed textures!");
										}
									}
								})
								.OpenPopup("Choose item", "Choose items to copy packed textures from.", allowClosing: true, showCancel: true);
						}
					}
				});
			}),
			new PopupChoice("From external file", () =>
			{
				UiOverlay.Instance.FileLoadPopup("Choose the json file and all textures!", (MyFileBrowserSingleton.UploadResult t) =>
				{
					if (t.GotFiles)
					{
						try
						{
							List<ModFileBase> list = t.Results.Where((MyFileBrowserSingleton.UploadResult.FileResult a) => a.Success).Select((Func<MyFileBrowserSingleton.UploadResult.FileResult, ModFileBase>)((MyFileBrowserSingleton.UploadResult.FileResult b) => new ModFileInMemory(b.Path.Replace('\\', '/'), b.Data))).ToList();
							ModFileBase modFileBase = list.FirstOrDefault((ModFileBase modFileBase2) => modFileBase2.NormalizedPath.EndsWith(".json"));
							if (modFileBase == null)
							{
								throw new FileNotFoundException("Missing json file with the container format!");
							}
							AddItemTexture(item, PackedTexturesContainer.LoadFromModFiles(modFileBase.NormalizedPath, list.AsReadOnly()));
						}
						catch (Exception ex)
						{
							Debug.LogException(ex, this);
							UiOverlay.Instance.OkPopup("Error!", "Something went wrong while loading the container.\nLog\n" + ex.Message);
						}
					}
				}, null, multifile: true);
			})
		});
	}

	private void CreateItemButtonClicked()
	{
		CompoundItemList.StartBuilder<ItemPrefab, StringAdvancedItemListItemPrefab>().WithDefaultItemPrefabName().WithDefaultItemPrefabSorting()
			.WithDefaultItemPrefabFolders()
			.WithDefaultStringAdvancedPrefabSetItem()
			.WithDefaultListItemPool()
			.Build(ItemPrefab.ItemPrefabManager.AllNonDefaultItems.Select(((GameId id, ItemPrefab prefab) x) => x.prefab).ToFrozenDataSource())
			.ToChooseItemsComplexPopupBuilderItemPrefab()
			.WithSingularStartingSelection()
			.WithOnFinished((UiOverlay.PopupFinishTypesEnum s, Selection<ItemPrefab> selection) =>
			{
				if (s == UiOverlay.PopupFinishTypesEnum.Successful)
				{
					_itemPrefabs.AddRange(from x in selection.GetAllItems()
						select x.CloneWithTextures(GetOrCreateLive2DControllers()));
					_itemItemList.UpdateAll();
				}
			})
			.OpenPopup("Creating items", "Choose items to use as a base for new items. Remember that some of the items will NOT have textures. For example pixel glasses.", allowClosing: true, showCancel: true);
	}

	private void ExportPackedTextureContainer(PackedTexturesContainer texture)
	{
		string tmpModPath = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.TmpModPath;
		SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.PrepareTmpPath();
		texture.SaveToPath(tmpModPath + "/" + GlobalFunctions.ReplaceInvalidChars("packedTexture.json"));
		byte[] data = MyZipUtility.CreateZipFile(tmpModPath);
		UiOverlay.Instance.FileSavePopup("packedTexture.zip", data, null);
	}

	private void SwitchDisplayedItem(ItemPrefab item, int i)
	{
		if (item != null)
		{
			_createPackedTextureContainerButton.gameObject.SetActive(value: true);
			_otherPackedTextureContainersButton.gameObject.SetActive(value: true);
			_otherSelectedButton.gameObject.SetActive(value: false);
			_descriptionText.text = item.ToString();
			_textureList.Focus(-1);
			_textureList.UpdateAll();
		}
		else
		{
			_createPackedTextureContainerButton.gameObject.SetActive(value: false);
			_otherPackedTextureContainersButton.gameObject.SetActive(value: false);
			_otherSelectedButton.gameObject.SetActive(value: false);
			_descriptionText.text = "No item or packed texture selected!";
		}
		_packedTextureInfoRectTransform.gameObject.SetActive(value: false);
	}

	private void SwitchDisplayedPackedTextureContainer(PackedTexturesContainer itemTexture, int i)
	{
		if (!_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var _))
		{
			return;
		}
		if (itemTexture != null)
		{
			_packedTextureInfoRectTransform.gameObject.SetActive(value: true);
			_otherSelectedButton.gameObject.SetActive(value: true);
			List<ControllerEnum> affectedControllers = itemTexture.GetAffectedControllers();
			List<string> values = itemTexture.PackedDrawables.Select((PackedTexturesContainer.PackedDrawable t) => t.Name).Distinct().ToList();
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendLine("Affects: " + string.Join(", ", affectedControllers.Select((ControllerEnum t) => t.GetControllerId())));
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Drawables: " + string.Join(", ", values));
				SyncPackedTextureInfo(itemTexture);
				_descriptionText.SetText(stringBuilder);
				return;
			}
		}
		_otherSelectedButton.gameObject.SetActive(value: false);
		_packedTextureInfoRectTransform.gameObject.SetActive(value: false);
	}

	private void UpdateTopButtons()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (_topButtonList.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddCommonSmall();
			buttonListWrapper.TextFunc = () => "Lua Documentation";
			buttonListWrapper.OnClick = () =>
			{
				string data = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GenerateLuaDocumentation();
				UiOverlay.Instance.FileSavePopup("luaDocumentation.txt", data, null);
			};
			ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddCommonSmall();
			buttonListWrapper2.TextFunc = () => "Personality Lua";
			buttonListWrapper2.OnClick = () =>
			{
				string data = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GenerateEmptyPersonalityDefinition();
				UiOverlay.Instance.FileSavePopup("personalityDefinition.lua", data, null);
			};
		}
	}

	public void Awake()
	{
		FindReferences();
		UpdateTopButtons();
		_itemItemListAdapter = _itemItemList.SetAdapter(DataSourceUtils.CreateMultipleUse(() => _itemPrefabs));
		_itemItemListAdapter.ItemUpdatedAction = (GameObject tmp, ItemPrefab item, int i, bool focused) =>
		{
			TextMeshProUGUI componentInChildren = tmp.transform.GetComponentInChildren<TextMeshProUGUI>();
			componentInChildren.text = item.Name;
			componentInChildren.fontStyle = (focused ? FontStyles.Bold : FontStyles.Normal);
			tmp.transform.Find("Trash Button").GetComponent<Button>().onClick.SetListener(() =>
			{
				if (_itemPrefabs.TryGetValue(i, out var value))
				{
					UiOverlay.Instance.SimplePopup("Deleting item", "Are you sure you want to delete \"" + value.Name + "\"?", () =>
					{
						_itemPrefabs.RemoveAt(i);
						_itemItemList.UpdateAll();
						_textureList.UpdateAll();
					});
				}
			});
		};
		_itemItemListAdapter.ItemFocusedAction = SwitchDisplayedItem;
		_textureListAdapter = _textureList.SetAdapter(DataSourceUtils.CreateMultipleUse(() =>
		{
			ItemPrefab currentlyFocusedObject = _itemItemListAdapter.CurrentlyFocusedObject;
			return (currentlyFocusedObject != null && currentlyFocusedObject.HasAnyTextures) ? currentlyFocusedObject.Textures : Array.Empty<PackedTexturesContainer>();
		}));
		_textureListAdapter.ItemUpdatedAction = (GameObject tmp, PackedTexturesContainer ptc, int i, bool focused) =>
		{
			TextMeshProUGUI componentInChildren = tmp.transform.GetComponentInChildren<TextMeshProUGUI>();
			componentInChildren.text = ptc.GetNameUsingAffectedControllersId();
			componentInChildren.fontStyle = (focused ? FontStyles.Bold : FontStyles.Normal);
			tmp.transform.Find("Trash Button").GetComponent<Button>().onClick.SetListener(() =>
			{
				if (_itemItemListAdapter.TryGetCurrentlyFocusedItem(out var item) && item.Textures != null && item.Textures.TryGetValue(i, out var value))
				{
					UiOverlay.Instance.SimplePopup("Deleting item", "Are you sure you want to delete packed texture \"" + value.GetNameUsingAffectedControllersId() + "\" from \"" + item.Name + "\"?", () =>
					{
						item.Textures = item.Textures.RemoveAtArrayCopy(i);
						_textureList.UpdateAll();
					});
				}
			});
		};
		_textureListAdapter.ItemFocusedAction = SwitchDisplayedPackedTextureContainer;
	}

	private ModCreationLive2DControllersContainer GetOrCreateLive2DControllers()
	{
		return _live2DControllers ?? (_live2DControllers = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetModCreationData("ModCreatorPopup"));
	}

	public void Open(List<ItemPrefab> items)
	{
		_itemPrefabs = items;
		base.Open();
		_itemItemList.UpdateAll();
		_textureList.UpdateAll();
	}

	public override void Open()
	{
		Open(new List<ItemPrefab>());
	}

	public override bool CloseInner()
	{
		_live2DControllers?.Dispose();
		_live2DControllers = null;
		return base.CloseInner();
	}
}
