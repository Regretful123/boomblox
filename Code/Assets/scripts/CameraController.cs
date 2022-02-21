using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class CameraController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GamePlay inputs;
    Vector2 look = Vector2.zero;
    float vertical = 0;
    bool mouseFire = false;
    [SerializeField] float minPower = 5f;
    [SerializeField] float maxPower = 50f;

    BallSoundData _soundFx;
    
    float _p = 0f;
    public float power 
    {
        get => _p;
        set {
            _p = Mathf.Clamp01( _p = value );
            OnPowerChanged?.Invoke(_p);
        }
    }

    public float rawPower => minPower + ( maxPower - minPower ) * power;

    Camera _cam;

    [SerializeField] Transform lookAtTarget;
    Transform _target;
    [SerializeField] GameObject bullet;
    [SerializeField] float rotationMin = -90;
    [SerializeField] float rotationMax = 90;
    public float acceleration = 1f;
    public Action<float> OnPowerChanged;

    bool overUIElements = false;
    
    // Start is called before the first frame update
    void Start()
    {
        inputs = new GamePlay();
        EnableInput();
        _target = new GameObject("cameraAimTarget").transform;
        _target.position = lookAtTarget.position;
        gameObject.transform.SetParent(_target, true);
        _soundFx = new BallSoundData( 420, 2156 );
        _cam = Camera.main;
    }

    void OnDestroy()
    {
        DisableInput();
    }

    void EnableInput()
    {
        // inputs.Game.Look.performed += HandleLook;
        inputs.Game.Look.performed += HandleLook;
        inputs.Game.Look.Enable();

        // inputs.Game.Shoot.performed += HandleShoot;
        inputs.Game.Shoot.started += HandleShoot;
        inputs.Game.Shoot.canceled += HandleShoot;
        inputs.Game.Shoot.Enable();

        inputs.Game.Reset.performed += HandleReset;
        inputs.Game.Reset.Enable();
    }

    void DisableInput()
    {
        inputs.Game.Look.performed -= HandleLook;
        inputs.Game.Look.Disable();

        inputs.Game.Shoot.started -= HandleShoot;
        inputs.Game.Shoot.canceled -= HandleShoot;
        inputs.Game.Shoot.Disable();

        inputs.Game.Reset.performed -= HandleReset;
        inputs.Game.Reset.Disable();
    }

    private void HandleReset(InputAction.CallbackContext context)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);   // may want to be able to reset the scene? Or randomize the level in some way..
    }

    private void HandleShoot(InputAction.CallbackContext context)
    {
        // skip the code below if we're handling over UI elements
        if( overUIElements )
            return;
        
        bool wasFired = mouseFire;
        mouseFire = context.ReadValueAsButton();
        if( !mouseFire && wasFired )
        {
            StopAllCoroutines();
            Fire();
        }
        else
        {
            StartCoroutine( AdjustValue() );
        }
    }

    IEnumerator AdjustValue()
    {
        float i = Time.time;
        while(true)
        {
            power = Mathf.PingPong( Time.time - i, 1f ); 
            yield return null;
        }
    }

    private void HandleLook(InputAction.CallbackContext context) => look = context.ReadValue<Vector2>();

    public void SetAcceleration( float value ) => acceleration = value;

    public void SetMaxFrequency( Single maxHertz )
    {
        _soundFx.maxHertz = (int)maxHertz;
    }

    public void SetMinFrequency( Single minHertz )
    {
        _soundFx.minHertz = (int)minHertz;
    }

    void LateUpdate()
    {
        // move the camera around the scene.
        vertical += look.y * acceleration * Time.deltaTime * Mathf.Rad2Deg;
        vertical = Mathf.Clamp( vertical, rotationMin, rotationMax );
        _target.RotateAround( _target.position, Vector3.up, -look.x * acceleration * Mathf.Rad2Deg * Time.deltaTime );
        _target.rotation = Quaternion.Euler( vertical, _target.rotation.eulerAngles.y, 0f );
    }

    void Fire()
    {
        var _bullet = Instantiate( bullet, _cam.transform.position, Quaternion.identity );
        if( _bullet.TryGetComponent<Rigidbody>(out Rigidbody _rb ))
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = _cam.nearClipPlane;
            Vector3 aim = _cam.ScreenToWorldPoint( mousePos );
            _bullet.transform.LookAt(aim);
            _rb.AddForce( _bullet.transform.forward * rawPower, ForceMode.Impulse );
        }
        if( _bullet.TryGetComponent<BallVelocitySound>( out BallVelocitySound sound ))
        {
            sound.Init( _soundFx );
        }
        Destroy( _bullet, 10f );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RaycastResult result = eventData.pointerCurrentRaycast;
        overUIElements = result.isValid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RaycastResult result = eventData.pointerCurrentRaycast;
        overUIElements = result.isValid;
    }
}
