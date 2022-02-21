using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointField : MonoBehaviour
{

    // This is used to display the component's checkbox in inspector.
    void Start() { }

    void OnCollisionEnter(Collision other )
    {
        if( !enabled )
            return;
        // if blocks hit, add points then destroy the  block
        if( other.gameObject.TryGetComponent<IDamage>(out IDamage target ))
            target.Damage();
    }
}