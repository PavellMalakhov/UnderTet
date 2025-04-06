using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ScrollerCamera : MonoBehaviour
{
    private const string MouseY = nameof(MouseY);
    private const string MouseScrollWheel = nameof(MouseScrollWheel);

    private float _scrolSpeed = 2f;
    private float _targetPositionY;
    private float _scrollMax = 10.21f;
    private float _scrollMin = -30f;
    private float _offsetY;
    private Vector2 _mouseScrollDelta;

    private void Update()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Scroll();
        }

       _mouseScrollDelta = Input.mouseScrollDelta;

        if (_mouseScrollDelta != Vector2.zero)
        {
            Scroll(_mouseScrollDelta);
        }
    }

    public void Scroll()
    {
        _offsetY = Input.GetAxis(MouseY) * _scrolSpeed * Time.deltaTime;

        _targetPositionY = Math.Clamp(transform.position.y - _offsetY, _scrollMin, _scrollMax);

        transform.position = new Vector3(transform.position.x, _targetPositionY, transform.position.z);
    }

    public void Scroll(Vector2 mouseScrollDelta)
    {
        _offsetY = mouseScrollDelta.y;

        _targetPositionY = Math.Clamp(transform.position.y + _offsetY, _scrollMin, _scrollMax);

        transform.position = new Vector3(transform.position.x, _targetPositionY, transform.position.z);
    }
}
