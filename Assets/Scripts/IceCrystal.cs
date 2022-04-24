using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystal : MonoBehaviour
{
    [SerializeField] private float _startTime;
    [SerializeField] private MergeCheck _mergeCheck;
    [SerializeField] private bool _isTouches = false;

    private void Update()
    {
        _startTime += Time.deltaTime;

        if (_startTime > 3 && _isTouches)
        {
            switch (_mergeCheck.CurrentStatus)
            {
                case MergeCheck.Status.Hot: _mergeCheck.CurrentStatus = MergeCheck.Status.Warm; break;

                case MergeCheck.Status.Warm : _mergeCheck.CurrentStatus = MergeCheck.Status.Neutral; break;

                case MergeCheck.Status.Neutral : _mergeCheck.CurrentStatus = MergeCheck.Status.Cold; break;

                case MergeCheck.Status.Cold : _mergeCheck.CurrentStatus = MergeCheck.Status.Frozen; break;

                case MergeCheck.Status.Frozen : _mergeCheck.CurrentStatus = MergeCheck.Status.Spoiled; break;

                default: break;
            }
            _startTime = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _startTime = 0;
        _mergeCheck = other.gameObject.GetComponent<MergeCheck>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ingredient")
        {
            _isTouches = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isTouches = false;
        _mergeCheck = null;
    }

}


