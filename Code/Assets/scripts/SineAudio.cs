using System;
using UnityEngine;

[RequireComponent(typeof(AudioLowPassFilter)), DisallowMultipleComponent]
public class SineAudio : MonoBehaviour
{
    int samplerate = 48000;
    float increment = 0;
    float phase = 0;

    public int frequency = 420;
    public float gain = 0.05f;
    [SerializeField] AudioLowPassFilter lowPassFilter;
    [SerializeField] AudioSource audioSource;
    AudioClip clip;
    
    public virtual void Start()
    {
        if( lowPassFilter == null )
                lowPassFilter = GetComponent<AudioLowPassFilter>() ?? gameObject.AddComponent<AudioLowPassFilter>();
        if( audioSource == null )
            audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        clip = AudioClip.Create("RuntimeSound", samplerate * 2, 1, samplerate, true );

        Debug.Log( AudioSettings.outputSampleRate );
        samplerate = Mathf.Min( AudioSettings.outputSampleRate, 48000 );
        Debug.Log( "After samplerate" );
    }

    // how or where is this data being feed into???
    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2f * Mathf.PI / samplerate;
        for( int i = 0; i < data.Length; ++i )
        {
            phase += increment;
            data[i] = gain * Mathf.Sin(phase);

            if( channels == 2 )
            {
                data[i+1] = data[i];
                i++;
            }
        }
    }
}
