using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CappedFramerate : MonoBehaviour
{
    [SerializeField]
    public int setTargetFPS = 60;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = setTargetFPS;
    }

}
