using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntity : MonoBehaviour, IDamage
{
    [SerializeField, Range(1,10)] int points = 1;

    void Start() => GameManager.Instance.AddBlock(this);    
    void OnDestroy() => GameManager.Instance.RemoveBlock(this);

    public void Damage()
    {
        GameManager.Instance.AddPoints(points);
        Destroy(this.gameObject);
    }

}
