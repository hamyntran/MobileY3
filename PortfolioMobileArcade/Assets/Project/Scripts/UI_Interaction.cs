using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Interaction : MonoBehaviour
{
   public void ShowGO(GameObject go) => go.SetActive(true);

   public void HideGO(GameObject go) => go.SetActive(false);

   public void FadeIn(Image fadeGO) => fadeGO.DOFade(255,0.3f);
   
   public void FadeOut(Image fadeGO) => fadeGO.DOFade(0,0.3f);


   public void ChangeScene(string name) => SceneManager.LoadScene(name);

   private void Start()
   {
      Image fadeIMG = transform.GetChild(0).GetComponent<Image>();
      if (fadeIMG) FadeOut(fadeIMG);
      else Debug.LogWarning($"Fade image was not assigned");
   }
}
