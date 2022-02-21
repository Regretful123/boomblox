using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider)), DisallowMultipleComponent]
public class HandleBallVelocity : MonoBehaviour
{
    [SerializeField] CameraController _controller;
    [SerializeField] Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        if( _controller == null )
        {
            _controller = FindObjectOfType<CameraController>();
        }

        if( _controller == null )
        {
            Debug.LogError("Unable to find Camera controller to reference to! Disabling!");
            this.enabled = false;
            return;
        }

        if( _slider != null )
            _slider = GetComponent<Slider>() ?? gameObject.AddComponent<Slider>();

        _controller.OnPowerChanged += HandleVelocityChanged;
    }

    void OnDestroy()
    {
        if( _controller != null )
            _controller.OnPowerChanged -= HandleVelocityChanged;
    }

    void HandleVelocityChanged(float value )
    {
        _slider.value = value;
    }
}
