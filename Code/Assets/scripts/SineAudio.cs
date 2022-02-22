using UnityEngine;

[RequireComponent(typeof(AudioLowPassFilter)), DisallowMultipleComponent]
public class SineAudio : MonoBehaviour
{
    float samplerate = 48000;
    float increment = 0;
    float phase = 0;

    public int position = 0;
    public float frequency = 420f;
    public float gain = 0.05f;
    [SerializeField] internal AudioLowPassFilter lowPassFilter;
    
    void Start()
    {
        if( lowPassFilter != null )
            lowPassFilter = GetComponent<AudioLowPassFilter>() ?? gameObject.AddComponent<AudioLowPassFilter>();

        samplerate = AudioSettings.outputSampleRate;   
    }

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
