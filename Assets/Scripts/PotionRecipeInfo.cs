using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionRecipeInfo", menuName = "Gameplay/New Potion")]
public class PotionRecipeInfo : ScriptableObject
{
    [SerializeField] private float _potionTime;

    public PotionRecipeInfo FirstStepRecipe;

    public GameObject PotionPrefab;

    public MergeCheck PotionMergeCheck => PotionPrefab.GetComponent<MergeCheck>();

    public int PotionCost => PotionMergeCheck.Cost;
    public float PotionTime => _potionTime;
    public string PotionName => PotionPrefab.name;

    public int ID => PotionMergeCheck.ID;

    public RecipeType CurrentRecipeType;
    public enum RecipeType
    {
        Simple,
        Hard,
    };

    [SerializeField] public List<IngredientInfo> Ingredients;

    [System.Serializable] public class IngredientInfo
    {
        public GameObject IngredientObject;
        public string Ingredient_Name => IngredientObject.name;
        public int Ingredient_ID => IngredientObject.GetComponent<MergeCheck>().ID;

        public MergeCheck.Status Status;
    }

}
