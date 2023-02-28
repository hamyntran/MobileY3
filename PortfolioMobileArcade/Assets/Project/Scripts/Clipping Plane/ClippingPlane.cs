using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingPlane : MonoBehaviour
{
    public Material Material;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      Plane plane = new Plane(transform.up, transform.position);

      Vector4 planeVisualisation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
      
      Material.SetVector("_Plane", planeVisualisation);

    }
}
