using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockDialBehaviour : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorOverride;

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

    // TODO: Only perform these when in interaction mode
    private void OnMouseUpAsButton()
    {
        IncreaseNumber();
    }

    // TODO: Only perform these when in interaction mode
    private void OnMouseEnter()
    {
        Cursor.SetCursor(_cursorOverride, Vector2.zero, CursorMode.Auto);
    }

    // TODO: Only perform these when in interaction mode
    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
        _transform.localRotation = Quaternion.Euler(0.0f, _angleDelta * _currentNumber * direction, 0.0f);
    }

    public int GetCurrentNumber()
    {
        return _currentNumber;
    }
}
