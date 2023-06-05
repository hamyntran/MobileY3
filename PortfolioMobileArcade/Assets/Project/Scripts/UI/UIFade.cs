using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIFade : MonoBehaviour
{
    public void FadeIn(Image fadeGO) => fadeGO.DOFade(255,0.5f);
   
    public void FadeOut(Image fadeGO) => fadeGO.DOFade(0,0.5f);

    [SerializeField] private Image fadeIMG;

    private void Awake()
    {
         fadeIMG = transform.Find("Fade IMG").GetComponent<Image>();
         if (fadeIMG)
         {
             fadeIMG.color = new Color(0, 0, 0, 255);
         }
    }

    private void Start()
    {
        if (fadeIMG)
        {
            fadeIMG.color = new Color(0, 0, 0, 255);
            FadeOut(fadeIMG);
        }
        else Debug.LogWarning($"Fade image was not assigned");
    }
}
