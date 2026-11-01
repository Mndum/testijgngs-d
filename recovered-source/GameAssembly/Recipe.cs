using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameItems;

public class Recipe
{
	private readonly List<RecipeIngredient> _allIngredients = new List<RecipeIngredient>();

	private readonly GameId _expectedResult;

	private readonly Func<CookingResults, List<Item>, Item> _createDishItemFunc;

	public GameId ExpectedResult => _expectedResult;

	public ItemPrefab ExpectedResultPrefab => ItemPrefab.ItemPrefabManager.GetItemById(_expectedResult);

	public ReadOnlyCollection<RecipeIngredient> AllIngredients => _allIngredients.AsReadOnly();

	private Recipe(GameId expectedResult)
	{
		_expectedResult = expectedResult;
		_createDishItemFunc = (CookingResults results, List<Item> ItemList) => Item.CreateRandomItem(_expectedResult);
	}

	public static Recipe CreateInstance(GameId expectedResult)
	{
		return new Recipe(expectedResult);
	}

	public Recipe(GameId expectedResult, Func<CookingResults, List<Item>, Item> createDishItemFunc)
	{
		_expectedResult = expectedResult;
		_createDishItemFunc = createDishItemFunc;
	}

	public Item CreateDish(CookingResults results, List<Item> usedItems)
	{
		return _createDishItemFunc(results, usedItems);
	}

	public void AddIngredient(RecipeIngredient recipeIngredient)
	{
		_allIngredients.Add(recipeIngredient);
	}

	public void AddIngredientExact(ItemEnum itemEnum, int count = 1, bool isOptional = false)
	{
		_allIngredients.Add(new RecipeIngredientExactItem(itemEnum.ToGameId(), count, isOptional));
	}

	public void AddIngredientExact(GameId gameId, int count = 1, bool isOptional = false)
	{
		_allIngredients.Add(new RecipeIngredientExactItem(gameId, count, isOptional));
	}

	public void AddIngredientGenericType(EdibleTypeEnum type, int count = 1, bool isOptional = false)
	{
		_allIngredients.Add(new RecipeIngredientGenericType(type, count, isOptional));
	}

	public void AddIngredientFishMinWeight(float minWeight, int count = 1, bool isOptional = false)
	{
		_allIngredients.Add(new RecipeIngredientGenericFish(minWeight, count, isOptional));
	}
}
