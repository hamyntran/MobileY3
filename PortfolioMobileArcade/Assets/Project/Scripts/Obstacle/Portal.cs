using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public Transform _portalPos;

/*#if UNITY_EDITOR
    private void OnValidate()
    {
        _portalPos = transform.GetChild(0);
    }
#endif*/
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            other.transform.position = new Vector3(otherPortal._portalPos.position.x, other.transform.position.y,
                otherPortal._portalPos.position.z);
            other.transform.rotation = new Quaternion(transform.rotation.x, otherPortal.transform.rotation.y,
                transform.rotation.z, transform.rotation.w);
        }
    }


}