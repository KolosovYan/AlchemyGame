using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCheck : MonoBehaviour
{
    public int ID;
    public int Cost;

    public Status CurrentStatus = Status.Neutral;
    public enum Status
    {
        Frozen = -2,
        Cold = -1,
        Neutral = 0,
        Warm = 1,
        Hot = 2,
        Spoiled = 3
    };

    public MergeCheck ThisMergeCheck;
    public MergeCheck OtherMergeCheck;
    private GameObject _ingredient;
    private float _mergeDistance = 15f;
    private MergeIngredients _mergeIngr;
    public bool CanMerge = true;
    public Renderer GameObjectRendered;
    private bool _mergeResult;

    void Start()
    {
        ThisMergeCheck = GetComponent<MergeCheck>();
        _mergeIngr = GameObject.FindGameObjectWithTag("GameController").GetComponent<MergeIngredients>();
        GameObjectRendered = gameObject.GetComponent<Renderer>();
    }

    public void ImageLost()
    {
        ResetStatusToDefault(gameObject, ThisMergeCheck);
    }

    private void Merge()
    {
        if (Vector3.Distance(transform.position, _ingredient.transform.position) < _mergeDistance)
        {

            _mergeResult = _mergeIngr.CreationVerification(ThisMergeCheck, OtherMergeCheck);
            if (_mergeResult)
            {
                ResetStatusToDefault(gameObject, ThisMergeCheck);
                ResetStatusToDefault(_ingredient, OtherMergeCheck);

            }
            if (!_mergeResult)
            {
                IngredientSpoiled(ThisMergeCheck);
                IngredientSpoiled(OtherMergeCheck);
            }

        }
    }
    
    private void IngredientSpoiled(MergeCheck m)
    {
        m.GameObjectRendered.material.color = Color.black;
        m.CurrentStatus = Status.Spoiled;
    }

    private void ResetStatusToDefault(GameObject x, MergeCheck m)
    {
        x.SetActive(false);
        m.CurrentStatus = Status.Neutral;
        m.GameObjectRendered.material.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ingredient")
        {
            OtherMergeCheck = other.gameObject.GetComponent<MergeCheck>();
            if (OtherMergeCheck.ID < ID)
            {
                _ingredient = other.gameObject;
                if (CanMerge && OtherMergeCheck.CanMerge)
                {
                    Merge();
                }
            }
        }
    }
}
