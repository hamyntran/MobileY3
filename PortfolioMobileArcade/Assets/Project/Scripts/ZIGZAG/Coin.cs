using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int reward = 10;
    private Material _material;
    private Color _originalColor;

    private void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        _material = mesh.material;
        _originalColor = _material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InGame.OnGainCoinInGame?.Invoke(reward);
            Disappear();
        }
    }

    public void Disappear()
    {
        Tweener fade = null;
        transform.DOMoveY(transform.position.y + 3.2f, 0.3f).OnPlay(() =>
            {
                transform.DORotate(transform.eulerAngles - new Vector3(0, 180, 0), 0.25f);
                 fade = _material.DOFade(0, 0.6f);
            })
            .OnComplete(() => { transform.parent.gameObject.ReturnToPool(); fade.Rewind(); });
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
       // _material.DOFade(1, 0.001f);
    }

}