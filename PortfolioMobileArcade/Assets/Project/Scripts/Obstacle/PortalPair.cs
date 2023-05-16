using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortalPair : Obstacle
{
    [SerializeField] private Portal _portal;
    [SerializeField] private float _yOffset = 0.75f;
    [SerializeField] private int _minGap, _maxGap = 5;
    [SerializeField] private WeightedRandomList<MovingEnemy> _enemies;

    private Transform _portalPos;

    private void Awake()
    {
        _ativation = new PortalPairActivation();
        _length = Random.Range(_minGap, _maxGap);
    
        // StartCoroutine(WaitToSpawnEnemy());
    }

    public void Activate()
    {
        CreatePair();
        SpawnEnemy();
    }

    private void CreatePair()
    {
        Portal portalA = Instantiate(_portal, transform);
        Portal portalB = Instantiate(_portal, transform);
        
        portalA.transform.localPosition = new Vector3(0, _yOffset, 0);
        portalB.transform.localPosition = new Vector3(_length -1, _yOffset, 0);
        portalA.transform.localEulerAngles = new Vector3(0, 90, 0);
        portalB.transform.localEulerAngles = new Vector3(0, portalA.transform.localEulerAngles.y*-1, 0);

        portalA.gameObject.name = "Portal A";
        portalB.gameObject.name = "Portal B";

        portalA.otherPortal = portalB;
        portalB.otherPortal = portalA;
        
        portalA._portalPos =  portalA.transform.GetChild(0);
        portalB._portalPos =  portalB.transform.GetChild(0);


        this._portalPos = portalA._portalPos;
    }

    private void SpawnEnemy()
    {
        MovingEnemy enemy = _enemies.GetRandom();
        MovingEnemy newEnemy =  Instantiate(enemy, new Vector3(_portalPos.position.x,transform.position.y,_portalPos.position.z), _portalPos.transform.parent.transform.rotation);
    }

    private IEnumerator WaitToSpawnEnemy()
    {
        float randWait = Random.Range(0, 5f);
        yield return new WaitForSeconds(randWait);

        SpawnEnemy();
    }

    public void SetLength(float length = 5)
    {
        _length = (int) length;
    }
}

public class PortalPairActivation : ObstacleAtivation
{
    public override void Activate(Obstacle obstacle)
    {
       PortalPair pair = obstacle as PortalPair;

       if (pair)
       {
           pair.Activate();
       }
    }
}
