using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HandleDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] string format = "0.00";
    
    void Start()
    {
        if( _text == null )
            _text = GetComponent<TMP_Text>() ?? gameObject.AddComponent<TMP_Text>();
    }

    public void UpdateText(float value ) => _text.text = value.ToString(format);
}
