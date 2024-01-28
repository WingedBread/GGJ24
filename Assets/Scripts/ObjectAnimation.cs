using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectAnimation : MonoBehaviour
{
    [SerializeField]
    Vector3 openRotation;
    [SerializeField]
    Vector3 closeRotation;
    [SerializeField]
    float time;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClose()
    {
        if (isOpen)
        {
            transform.DOLocalRotate(closeRotation, time).OnComplete(() => SetOpenState(false));
        }
        else
        {
            transform.DOLocalRotate(openRotation, time).OnComplete(() => SetOpenState(true));
        }
    }

    public bool isObjectOpen()
    {
        return isOpen;
    }

    private void SetOpenState(bool state)
    {
        isOpen = state;
    }
}
