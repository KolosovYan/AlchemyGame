using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private GameObject _parentGameObject;
    private MergeCheck _mergeCheck;
    private TextMesh _nameText;
    private TextMesh _statusText;

    void Start()
    {
        _parentGameObject = this.transform.parent.gameObject;
        _mergeCheck = _parentGameObject.GetComponent<MergeCheck>();
        _nameText = gameObject.GetComponent<TextMesh>();
        _nameText.text = _parentGameObject.name;
        _statusText = this.transform.GetChild(0).GetComponent<TextMesh>();

    }

    void Update()
    {
        if (_statusText != null)
        {
            _statusText.text = _mergeCheck.CurrentStatus.ToString();
        }
    }
}
