using UnityEngine;

[RequireComponent(typeof(Rigidbody)), DisallowMultipleComponent]
public class BetterCollisionDetect : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] LayerMask _mask;
    RaycastHit[] hitBuf = new RaycastHit[5]; 
    
    bool hasCollide;
    Vector3 stopPoint = Vector3.zero;
    // find a way to snap on the next physics update. 
    public void FixedUpdate()
    {


        int count = Physics.RaycastNonAlloc( transform.position, _rb.velocity, hitBuf, 999f, _mask );
        for( int i = 0; i<count; ++i )
        {
            if( !hitBuf[i].transform.gameObject.Equals( this.gameObject ))
            {
                 
            }
        }
    }    
}
