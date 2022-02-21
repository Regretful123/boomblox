using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), DisallowMultipleComponent]
public class SineAudio : MonoBehaviour
{
    public int position = 0;
    public int samplerate = 44100;
    public float frequency = 440;
    [SerializeField] internal AudioSource source;
    AudioClip _clip;
    
    void Start()
    {
        _clip = AudioClip.Create( "SineWave",samplerate * 2, 1, samplerate, true, OnAudioRead, OnAudioSetPosition );
        source = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        source.clip = _clip; 
        source.Play();   
    }

    private void OnAudioSetPosition(int position)
    {
        this.position = position;
    }

    private void OnAudioRead(float[] data)
    {
        int count = 0;
        while( count < data.Length )
        {
            data[count] = Mathf.Sin( 2 * Mathf.PI * frequency * position / samplerate );
            position++;
            count++;
        }
    }
}
