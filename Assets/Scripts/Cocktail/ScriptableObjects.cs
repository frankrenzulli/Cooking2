using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableObjects : ScriptableObject
{

    public int vodka;
    public int lemonSoda;
    public int gin;
    public int tonica;
    public int limeSlice;

    public Color FirstIngredientColor;
    public Color SecondIngredientColor;

    public string CocktailName;

    public Sprite firstIngredientSprite;
    public Sprite secondIngredientSprite;
}
