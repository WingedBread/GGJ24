using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorAnimations : MonoBehaviour
{
    [SerializeField]
    GameObject doorArmarioLeft; //125
    [SerializeField]
    GameObject doorArmarioRight; //-125
    [SerializeField]
    GameObject doorBedroom; //-125
    [SerializeField]
    GameObject doorBathroom; //125

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ArmarioLeft(bool open)
    {
        if (open)
        {
            //doorArmarioLeft.transform.DOLocalRotate(new Vector3(0, 125, 0));
        }
    }
}
