using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform)), DisallowMultipleComponent]
public class UIFollowMouse : MonoBehaviour
{
    Canvas _parentCanvas;
    RectTransform _rect;
    Camera _cam;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        var c = GetComponentsInParent<Canvas>();
        _parentCanvas = c[c.Length - 1];
        _cam = Camera.main;
    }

    void FixedUpdate()
    {
        Vector2 movePos;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentCanvas.transform as RectTransform,
            mousePos, _parentCanvas.worldCamera, out movePos
        );
        transform.position = _parentCanvas.transform.TransformPoint( movePos );
    }
}
