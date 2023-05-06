using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Interaction : MonoBehaviour
{
   
   public void ShowGO(GameObject go) => go.SetActive(true);

   public void HideGO(GameObject go) => go.SetActive(false);
   
   public void ChangeScene(string name) => SceneManager.LoadScene(name);


}
