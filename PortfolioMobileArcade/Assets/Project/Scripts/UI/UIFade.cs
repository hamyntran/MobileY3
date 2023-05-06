using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIFade : MonoBehaviour
{
    public void FadeIn(Image fadeGO) => fadeGO.DOFade(255,0.3f);
   
    public void FadeOut(Image fadeGO) => fadeGO.DOFade(0,0.3f);

    private void Start()
    {
        Image fadeIMG = transform.GetChild(0).GetComponent<Image>();
        if (fadeIMG) FadeOut(fadeIMG);
        else Debug.LogWarning($"Fade image was not assigned");
    }
}
