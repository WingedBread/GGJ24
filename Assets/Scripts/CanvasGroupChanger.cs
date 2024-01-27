using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasGroupChanger : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroupFrom;
    [SerializeField]
    private CanvasGroup canvasGroupTo;

    public void ChangeCanvasGroup()
    {
        canvasGroupFrom.alpha = 0;
        canvasGroupFrom.interactable = false;
        canvasGroupFrom.blocksRaycasts = false;

        canvasGroupTo.alpha = 1;
        canvasGroupTo.interactable = true;
        canvasGroupTo.blocksRaycasts = true;
    }
}
