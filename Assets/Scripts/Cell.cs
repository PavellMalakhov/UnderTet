using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public event Action<Vector3> CursorEntered;

    private void OnMouseEnter()
    {
        CursorEntered?.Invoke(transform.position);
    }
}
