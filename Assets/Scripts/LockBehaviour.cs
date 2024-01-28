using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
public class LockBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<int> _expectedCombination;
    [SerializeField]
    private List<LockDialBehaviour> _lockDials;
    [SerializeField]
    private Transform _wireTransform;
    private AudioSource _audioSource;

    private bool _isOpen;
    private bool _hasBeenOpened;

    private void Awake()
    {
        _isOpen = false;
        _hasBeenOpened = false;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
            TryOpen(debug: true);
    }

    public bool CombinationIsCorrect()
    {
        int combinationCount = _expectedCombination.Count;
        int dialsCount = _lockDials.Count;
        if (dialsCount != combinationCount)
        {
            Debug.LogError("[LockBehaviour] Number of dials is different than the expected combination, this lock is impossible to open");
            return false;
        }

        for (int i = 0; i < dialsCount; ++i)
        {
            if (_lockDials[i].GetCurrentNumber() != _expectedCombination[i])
                return false;
        }

        return true;
    }

    public void TryOpen(bool debug = false)
    {
        if (_isOpen)
            return;

        if (!CombinationIsCorrect())
        {
            if (debug)
            {
                Debug.Log("[LockBehaviour] NOT OPENED");
                Debug.Log("EXPECTED COMBINATION");
                foreach (int number in _expectedCombination)
                    Debug.Log(number);
                Debug.Log("ENTERED COMBINATION");
                foreach (LockDialBehaviour lockDial in _lockDials)
                    Debug.Log(lockDial.GetCurrentNumber());
            }
            return;
        }

        _isOpen = true;
        _hasBeenOpened = true;
        if (debug) Debug.Log("[LockBehaviour] OPENED");
        DOTween.Sequence()
            .Append(_wireTransform.DOBlendableLocalMoveBy(new Vector3(0.0f, 0.002f, 0.0f), 0.5f))
            .Append(_wireTransform.DOLocalRotate(new Vector3(0.0f, 130.0f, 0.0f), 0.5f));
        _audioSource.Play();
    }

    public bool HasBeenOpened()
    {
        return _hasBeenOpened;
    }
}
