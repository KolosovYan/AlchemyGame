using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MergeIngredients : MonoBehaviour
{
    [SerializeField] public PotionRecipeInfo CurrentPotionRecipe;

    public Wallet Wallet;

    private PotionRecipeInfo _nextRecipeStep;
    private PotionRecipeInfo _previousRecipeStep;

    [SerializeField] private List<PotionRecipeInfo> PotionRecipes;

    [SerializeField] private float _timeToCreate;

    [SerializeField] private float _waitingTime = 10;

    [SerializeField] private float _timeToNewPotion;

    private UI_Manager _ui_Manager;

    public TrackableBehaviour TheTrackable;

    private GameObject _createdObject;

    private GameObject _oldCreatedObject;

    private MergeCheck _createdObjectMerge;

    public bool IsCreating;

    private string _recipe;

    public void Start()
    {
        _ui_Manager = GetComponent<UI_Manager>();
        NewRandomRecipe();
    }

    private bool CheckRecipe(MergeCheck x)
    {
        for (int i = 0; i != CurrentPotionRecipe.Ingredients.Count;)
        {
            if (x.ID == CurrentPotionRecipe.Ingredients[i].Ingredient_ID)
            {
                if (x.CurrentStatus == CurrentPotionRecipe.Ingredients[i].Status)
                {
                    return true;
                }

                else
                {
                    i++;
                }
            }

            else
            {
                i++;
            }
        }

        return false;
    }

    public bool CreationVerification(MergeCheck x1, MergeCheck x2)
    {
        if (CheckRecipe(x1))
        {
            if (CheckRecipe(x2))
            {
                CreatePotion();
                return true;
            }
            else
            {
                Debug.Log("Wrong Recipe");
                PreviousRecipe();
                return false;
            }
        }
        else
        {
            Debug.Log("Wrong Recipe");
            PreviousRecipe();
            return false;
        }
    }

    private void PreviousRecipe()
    {
        if (_previousRecipeStep != null)
        {
            _nextRecipeStep = CurrentPotionRecipe;
            CurrentPotionRecipe = _previousRecipeStep;
            _previousRecipeStep = null;
            _ui_Manager.DestroyButtonSetActive(true);
            SetRecipe();
        }
    }

    public void DestroyWrongPotion()
    {
        Destroy(_oldCreatedObject);
        _ui_Manager.DestroyButtonSetActive(false);
        if (_ui_Manager.SellButton.activeSelf == true)
        {
            _ui_Manager.SellButtonSetActive(false);
        }
    }

    public void CreatePotion()
    {
        _createdObject = Instantiate(CurrentPotionRecipe.PotionPrefab);
        _createdObject.name = CurrentPotionRecipe.PotionPrefab.name;
        _createdObject.transform.SetParent(TheTrackable.transform);
        _createdObject.transform.localPosition = new Vector3(0, CurrentPotionRecipe.PotionPrefab.transform.position.y, 0);
        _createdObject.transform.localRotation = Quaternion.identity;
        _createdObject.transform.localScale = new Vector3(10f, 10f, 10f);
        _createdObjectMerge = _createdObject.GetComponent<MergeCheck>();
        if ((_createdObjectMerge.ID == CurrentPotionRecipe.ID) && (_nextRecipeStep == null))
        {
            _ui_Manager.SellButtonSetActive(true);
            _ui_Manager.ChangeText("Congratulations, you did it!");
            _previousRecipeStep = null;
        }

        else if ((_createdObjectMerge.ID == CurrentPotionRecipe.ID) && (_nextRecipeStep != null))
        {
            _previousRecipeStep = CurrentPotionRecipe;
            CurrentPotionRecipe = _nextRecipeStep;
            _nextRecipeStep = null;
            _oldCreatedObject = _createdObject;
            SetRecipe();
        }
    }

    private void SetRecipe()
    {
        _recipe = CurrentPotionRecipe.Ingredients[0].Ingredient_Name + "(" + CurrentPotionRecipe.Ingredients[0].Status + ")" + " + " + CurrentPotionRecipe.Ingredients[1].Ingredient_Name + "(" + CurrentPotionRecipe.Ingredients[1].Status + ")";
        _ui_Manager.ChangeRecipe(_recipe);
    }

    private void NewRandomRecipe()
    {
        CurrentPotionRecipe = PotionRecipes[Random.Range(0, PotionRecipes.Count)];
        _ui_Manager.ChangeText("Create the " + CurrentPotionRecipe.PotionName);
        _timeToCreate = CurrentPotionRecipe.PotionTime;
        IsCreating = true;
        if (CurrentPotionRecipe.CurrentRecipeType == PotionRecipeInfo.RecipeType.Hard)
        {
            _nextRecipeStep = CurrentPotionRecipe;
            CurrentPotionRecipe = _nextRecipeStep.FirstStepRecipe;
            SetRecipe();
        }
        else
        {
            SetRecipe();
        }
    }

    public void SellPotion()
    {
        Wallet.MoneyPlus(CurrentPotionRecipe.PotionCost);
        Destroy(_createdObject);
        Destroy(_oldCreatedObject);
        IsCreating = false;
        _ui_Manager.ChangeRecipe();
        _ui_Manager.SellButtonSetActive(false);
        CurrentPotionRecipe = null;

    }

    public void Update()
    {
        if (IsCreating)
        {
            _timeToCreate -= Time.deltaTime;
            _ui_Manager.TimerText.text = _timeToCreate.ToString("0");
            if (_timeToCreate <= 0)
            {
                _ui_Manager.TimerText.text = "Time is over";
            }
        }
        else if (!IsCreating)
        {
            _timeToNewPotion -= Time.deltaTime;
            _ui_Manager.TimerText.text = ("Next potion: " + _timeToNewPotion.ToString("0"));
            if (_timeToNewPotion <= 0)
            {
                NewRandomRecipe();
                _timeToNewPotion = _waitingTime;
            }
        }
    }
}
