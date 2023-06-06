using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectID : MonoBehaviour
{
    public int ID;
    public string tag;
    public static Dictionary<string, int> IDbyTags = new Dictionary<string, int>();

    private void Awake()
    {
    }

   
}