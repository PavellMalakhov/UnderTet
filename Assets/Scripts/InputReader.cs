using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool _isSelect;
    private bool _isMoved;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isSelect = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMoved = true;
        }

        if (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began)
        {
            _isSelect = false;
            _isMoved = false;
        }
    }

    public bool GetSelect() => GetBoolAsTrigger(ref _isSelect);
    public bool GetMoved() => GetBoolAsTrigger(ref _isMoved);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
