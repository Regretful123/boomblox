using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), DisallowMultipleComponent]
public class BallVelocitySound : SineAudio 
{

    Rigidbody _rb;
    Vector3 _currentVelocity;
    Vector3 _orgVelocity;

    public int maxHertz = 2156;
    public int minHertz = 420;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        _orgVelocity = _rb.velocity;    
    }

    void FixedUpdate()
    {
        _currentVelocity = _rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        float percent = ( _currentVelocity.sqrMagnitude / _orgVelocity.sqrMagnitude );
        this.frequency = Mathf.Lerp( minHertz, maxHertz, percent );
        this.source.volume = Mathf.Clamp01( percent );
    }

    public void Init( BallSoundData soundData )
    {
        this.minHertz = soundData.minHertz;
        this.maxHertz = soundData.maxHertz;
    }
}
