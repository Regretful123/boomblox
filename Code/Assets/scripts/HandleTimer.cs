using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text)), DisallowMultipleComponent]
public class HandleTimer : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    
    void Start()
    {
        if( _text != null )
            _text = GetComponent<TMP_Text>() ?? gameObject.AddComponent<TMP_Text>();
        GameManager.Instance.OnTimerChanged += HandleTimerChanged;
    }

    void OnDestroy()
    {
        if( GameManager.Exists())
            GameManager.Instance.OnTimerChanged -= HandleTimerChanged;
    }

    void HandleTimerChanged(int time)
    {
        int sec = time % 60;
        _text.text = $"{time / 60}:{sec.ToString("D2")}";
    }
}
