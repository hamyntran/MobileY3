using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasCoolDown 
{
    List<float>  allMaxCooldown { get; }
    List<string> allCoolDownID { get; }
}

[System.Serializable]       
public struct CoolDown
{
    public float maxCooldown;
    public string coolDownPurpose;
}