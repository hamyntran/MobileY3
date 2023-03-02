using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PortalObject : MonoBehaviour
{
   public GameObject CloneGO;

   private int _inPortalCount = 0;

   private Portal _inPortal, _outPortal;

   private Rigidbody _rigidbody;
   protected Collider _collider;
   
   private readonly Quaternion _halfTurn = Quaternion.Euler(0,180,0);

   private void Awake()
   {
      CloneGO = new GameObject();
      CloneGO.SetActive(false);

      var meshFilter = CloneGO.AddComponent<MeshFilter>();
      var meshRenderer = CloneGO.AddComponent<MeshRenderer>();

      meshFilter.mesh = GetComponent<MeshFilter>().mesh;
      meshRenderer.materials = GetComponent<MeshRenderer>().materials;
      CloneGO.transform.localScale = transform.localScale;

      _rigidbody = GetComponent<Rigidbody>();
      _collider = GetComponent<Collider>();
   }

   public void LateUpdate()
   {
      if(_inPortal ==null || _outPortal == null) {return;}

      if (CloneGO.activeSelf)
      {
         var inTransform = _inPortal.transform;
         var outTransform = _outPortal.transform;

         Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
         relativePos = _halfTurn * relativePos;
         CloneGO.transform.position = outTransform.TransformPoint(relativePos);
         
         Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
         relativeRot = _halfTurn * relativeRot;
         CloneGO.transform.rotation = outTransform.rotation *relativeRot ;
      }
      else
      {
         CloneGO.transform.position = new Vector3(1000,1000,10000);

      }
   }

   public void EnterPortal(Portal inPortal, Portal outPortal)
   {

      this._inPortal = inPortal;
      this._outPortal = outPortal;
      
      CloneGO.SetActive(false);

      ++_inPortalCount;
   }

   public void ExitPortal()
   {
      --_inPortalCount;

      if (_inPortalCount == 0)
      {
         CloneGO.SetActive(false);
      }
   }

   public void Warp()
   {
      Debug.Log("eneter");

      var inTransform = _inPortal.transform;
      var outTransform = _outPortal.transform;
      
      Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
      relativePos = _halfTurn * relativePos;
      transform.position = outTransform.TransformPoint(relativePos);
      
      Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
      relativeRot = _halfTurn * relativeRot;
      transform.rotation = outTransform.rotation *relativeRot ;
      
      Vector3 relativeVel = inTransform.InverseTransformPoint(_rigidbody.velocity);
      relativeVel = _halfTurn * relativeVel;
      _rigidbody.velocity = outTransform.TransformDirection(relativeVel);

      var t = _inPortal;
      _inPortal = _outPortal;
      _outPortal = t;
   }
}
