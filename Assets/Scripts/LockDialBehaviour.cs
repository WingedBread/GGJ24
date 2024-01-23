using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockDialBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _numbersCount;
    [SerializeField]
    private bool _numbersIncreaseClockwise;

    private Transform _transform;
    private int _currentNumber;
    private float _angleDelta;

    private void Awake()
    {
        _transform = transform;
        _angleDelta = 1.0f / _numbersCount * 360.0f;
    }

    public void IncreaseNumber()
    {
        _currentNumber += 1;
        _currentNumber %= _numbersCount;
        SetRotation();
    }

    public void DecreaseNumber()
    {
        _currentNumber -= 1;
        _currentNumber += _numbersCount; // Prevent negative numbers that do not work well with the modulo operator
        _currentNumber %= _numbersCount;
        SetRotation();
    }

    private void SetRotation()
    {
        float direction = _numbersIncreaseClockwise ? 1.0f : -1.0f;
        _transform.localRotation = Quaternion.Euler(0.0f, 0.0f, _angleDelta * _currentNumber * direction); // TODO: Change to y-axis
    }

    public int GetCurrentNumber()
    {
        return _currentNumber;
    }
}
