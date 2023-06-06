using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayDotween : MonoBehaviour
{
   public void PlayDoTween(string id)
   {
      DOTween.Restart(id);
   }
}
