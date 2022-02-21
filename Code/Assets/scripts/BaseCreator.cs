using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreator : MonoBehaviour
{
    List<GameObject> _col = new List<GameObject>();
    [SerializeField] GameObject template;
    [SerializeField] GameObject baseObject;
    Transform _base;
    [SerializeField] byte itemsPerRow = 3;
    [SerializeField] byte heightCount;
    [SerializeField] float rotateDegreePerRow = 90f;

    Vector3 offset = Vector3.zero;
    float unitPerBlock;
    float unitBlockHeight;

    void Start()
    {
        _base = Instantiate(baseObject, transform.position, transform.rotation ).transform;
        _base.localScale = Vector3.one;
        offset = transform.position;    // reference local object
        offset += Vector3.up * _base.localScale.y / 2;    // move above base cube.
        offset += Vector3.up * ( template.transform.localScale.y / 2 ); // Adjust the height from the height of each blocks.
        unitPerBlock = template.transform.localScale.x;
        unitBlockHeight = template.transform.localScale.y;
        BuildBase();
    }

    void ClearCollection()
    {
        for( int i = _col.Count - 1; i >= 0; --i )
        {
            if( _col[i] != null )
                Destroy( _col[i] );
        }
        _col.Clear();
    }

    void BuildBase()
    {
        float angle = 0;
        float rowMaxLen = itemsPerRow / 2;
        Vector3 dir = Vector3.zero;
        for( byte y = 0; y < heightCount; ++y )
        {
            dir = new Vector3( Mathf.Sin( angle * Mathf.Deg2Rad ), 0, Mathf.Cos( angle * Mathf.Deg2Rad ));
            for( byte x = 0; x < itemsPerRow; ++x )
            {
                var item = Instantiate( template, offset + ( Vector3.up * y * template.transform.localScale.y ), Quaternion.Euler( Vector3.up * ( angle - 90 ) ) );
                item.transform.position += dir * ( x - rowMaxLen ) * unitPerBlock;
                // if( item.TryGetComponent<Rigidbody>(out Rigidbody _rb ))
                // {
                //     _rb.velocity = Vector3.zero;
                //     _rb.isKinematic = true;
                // }
                _col.Add(item);
            }
            angle += rotateDegreePerRow; 
            angle %= 360;    
        }
    }

    void Reload()
    {
        ClearCollection();
        BuildBase();
    }

    public void SetItemPerRow( Single size )
    {
        size = Mathf.Clamp( size, 1, 255 );
        itemsPerRow = (byte)size;
        Reload();
    }

    public void SetCollectionHeight( Single height )
    {
        height = Mathf.Clamp( height, 1, 255 );
        this.heightCount = (byte)height;
        Reload();
    }

    public void SetRotationPerRow( Single newAngle )
    {
        rotateDegreePerRow = newAngle;
        Reload();
    }
}
