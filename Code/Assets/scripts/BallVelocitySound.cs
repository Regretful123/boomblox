using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), DisallowMultipleComponent]
public class BallVelocitySound : SineAudio 
{

    [SerializeField] Rigidbody _rb;
    Vector3 _currentVelocity;
    Vector3 _orgVelocity;

    public int maxHertz = 2156;
    public int minHertz = 420;
    float percent = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if( _rb != null )
            _rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        _orgVelocity = _rb.velocity;    
    }

    void FixedUpdate()
    {
        _currentVelocity = _rb.velocity;
    }

    // Update is called once per frame
    public void Update()
    {
        percent += Time.deltaTime;
        percent = Mathf.Clamp01( percent );
        frequency = Mathf.CeilToInt( Mathf.Lerp( maxHertz, minHertz, percent ) );
        if( percent >= 1f )
        {
            frequency = 0;
            enabled = false;
        }
    }

    public void Init( BallSoundData soundData )
    {
        minHertz = soundData.minHertz;
        maxHertz = soundData.maxHertz;
    }
}
