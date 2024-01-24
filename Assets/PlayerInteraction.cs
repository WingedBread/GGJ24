using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField, Range(0.0f, 10.0f)]
    private float _maxDistance;

    private InteractableBehaviour _lastInteractableHighlighted;

    private void Awake()
    {
        _lastInteractableHighlighted = null;
    }

    private void Update()
    {
        _lastInteractableHighlighted?.DisableHighlight();

        InteractableBehaviour interactable = FindInteractableObjectsInSight();
        if (interactable == null)
            return;

        interactable.EnableHighlight();

        if (Input.GetKeyUp(KeyCode.E))
            interactable.Interact(); // TODO: Enter interaction mode in game manager

        _lastInteractableHighlighted = interactable;
    }

    private InteractableBehaviour FindInteractableObjectsInSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _maxDistance, _layerMask))
        {
            InteractableBehaviour interactable = hit.transform.GetComponent<InteractableBehaviour>();
            return interactable;
        }

        return null;
    }

}
