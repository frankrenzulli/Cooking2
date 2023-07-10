using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableObjects : ScriptableObject
{
    [Header("Ingredients Required For Cocktail")]
    public int vodka;
    public int lemonSoda;
    public int gin;
    public int tonica;
    public int limeSlice;

    [Header("IngredientsColorReference")]
    public Color FirstIngredientColor;
    public Color SecondIngredientColor;

    public string CocktailName;

    [Header("IngredientsSprite")]
    public Sprite firstIngredientSprite;
    public Sprite secondIngredientSprite;
}
