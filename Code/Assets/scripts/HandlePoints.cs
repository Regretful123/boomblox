using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text)), DisallowMultipleComponent]
public class HandlePoints : MonoBehaviour
{

    TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPointChanged += HandlePointChanged;
        _text = GetComponent<TMP_Text>() ?? gameObject.AddComponent<TMP_Text>();   
    }

    void OnDestroy()
    {
        if( GameManager.Exists())
            GameManager.Instance.OnPointChanged -= HandlePointChanged;
    }

    void HandlePointChanged(int point) => _text.text = point.ToString();
}
