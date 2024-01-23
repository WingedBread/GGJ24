using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<int> _expectedCombination;

    [SerializeField]
    private List<LockDialBehaviour> _lockDials;

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

    public void OpenSomething()
    {
        if (!CombinationIsCorrect())
        {
            Debug.Log("[LockBehaviour] NOT OPENED");
            return /* false*/;
        }

        Debug.Log("[LockBehaviour] OPENED");
        return /* true*/;
    }
}
