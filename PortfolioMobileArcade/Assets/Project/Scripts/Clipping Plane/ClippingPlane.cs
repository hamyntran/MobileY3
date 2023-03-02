using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ClippingPlane : MonoBehaviour
{
    public Material Material;
    public List<GameObject> maskObj;
    
    private void OnTriggerStay(Collider other)
    {
        Plane plane = new Plane(transform.up, transform.position);

        Vector4 planeVisualisation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
      
       Material.SetVector("_Plane", planeVisualisation);
       
      // other.gameObject.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
    
}
