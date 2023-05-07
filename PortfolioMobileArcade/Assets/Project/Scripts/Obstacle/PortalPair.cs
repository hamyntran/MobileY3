using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortalPair : Obstacle
{
    [SerializeField] private Portal _portal;
    [SerializeField] private int _minGap, _maxGap = 5;

    private void Awake()
    {
        _length = Random.Range(_minGap, _maxGap);
    }

    private void Start()
    {
        CreatePair();
    }

    private void CreatePair()
    {
        Portal portalA = Instantiate(_portal, transform);
        Portal portalB = Instantiate(_portal, transform);
        
        portalA.transform.localPosition = new Vector3(0, 0, 0);
        portalB.transform.localPosition = new Vector3(_length, 0, 0);
        portalA.transform.localEulerAngles = new Vector3(0, 90, 0);
        portalB.transform.localEulerAngles = new Vector3(0, portalA.transform.localEulerAngles.y*-1, 0);

        portalA.gameObject.name = "Portal A";
        portalB.gameObject.name = "Portal B";

        portalA.otherPortal = portalB;
        portalB.otherPortal = portalA;
        
        portalA._portalPos =  portalA.transform.GetChild(0);
        portalB._portalPos =  portalB.transform.GetChild(0);


    }
}
