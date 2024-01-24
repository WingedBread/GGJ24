using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableBehaviour : MonoBehaviour
{
    [SerializeField]
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    // NOTE: For some weird reason outline only works properly when they start enabled in the scene.
    // To work around this, they start enabled but are automatically disabled after one frame in this
    // Start function
    private IEnumerator Start()
    {
        yield return null;
        _outline.enabled = false;
    }

    public void EnableHighlight()
    {
        _outline.enabled = true;
    }

    public void DisableHighlight()
    {
        _outline.enabled = false;
    }

    public void Interact()
    {
        Debug.Log("Interacting");
    }
}
