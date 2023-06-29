using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasCoolDown 
{
    List<float>  allMaxCooldown { get; }
    List<string> allCoolDownID { get; }
}

[System.Serializable]       
public class CoolDown
{
    public CoolDownID coolDownPurpose;
    public float maxCooldown;
}

public enum CoolDownID
{
   
}