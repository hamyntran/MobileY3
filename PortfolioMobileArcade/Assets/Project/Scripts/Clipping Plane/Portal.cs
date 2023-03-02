using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<PortalObject> PortalObjects = new List<PortalObject>();
    public Portal otherPortal;
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject.GetComponent<PortalObject>();

        if (obj)
        {
            PortalObjects.Add(obj);
            
            obj.EnterPortal(this,otherPortal );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject.GetComponent<PortalObject>();

        if (PortalObjects.Contains(obj))
        {
            PortalObjects.Remove(obj);
            obj.ExitPortal();
        }
    }

    private void Update()
    {
        foreach (PortalObject portalObject in PortalObjects)
        {
            Vector3 objPos = transform.InverseTransformPoint(portalObject.transform.position);
            
            Debug.Log(objPos.z);
            if (objPos.z > 0)
            {
                portalObject.Warp();
            }
        }
    }
}
