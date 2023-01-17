using System.Collections.Generic;
using UnityEngine;
using Framework.Singleton;

namespace Framework.CoolDown
{
    public class CoolDownManager : Singleton<CoolDownManager>
{
    public List<CoolData> currentCooldownData = new List<CoolData>();

    public void PutOnCooldown(IHasCoolDown cooldown, string purpose, GameObject obj)
    {
        int index =  GetIndex(cooldown, purpose,obj);
        if(index<0){return;}
        if(IsOnCooldown(cooldown,cooldown.allCoolDownID[index],obj)){return;}
        currentCooldownData.Add(new CoolData(cooldown,index));
    }

    public int GetIndex(IHasCoolDown cooldown,string purpose, GameObject obj)
    {
        for (int i = 0; i < cooldown.allCoolDownID.Count; i++)
        {
            if (cooldown.allCoolDownID[i] == purpose+ obj.GetInstanceID())
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsOnCooldown(IHasCoolDown cooldown,string purpose, GameObject obj)
    {
        int index =  GetIndex(cooldown, purpose,obj);
        if (index < 0)
        {
            return false;}
        
        // return currentOnCooldown.ContainsKey(cooldownID);
        foreach (var data in currentCooldownData)
        {
            if (data.id == cooldown.allCoolDownID[index])
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        for (int i = 0; i < currentCooldownData.Count; i++)
        {
            if (currentCooldownData[i].FinishedCooldown())
            {
                currentCooldownData.RemoveAt(i);
                return;
            }
        }
    }

    public void SetRemainTime(IHasCoolDown cooldown,string purpose, GameObject obj)
    {
        int index =  GetIndex(cooldown, purpose,obj);
        if (index < 0) {return;}
        
        foreach (var data in currentCooldownData)
        {
            if (data.id == cooldown.allCoolDownID[index])
            {
                data.remainTime = cooldown.allMaxCooldown[index];
                print(cooldown.allMaxCooldown);
            }
        }
    }
}

public class CoolData
{
    public float remainTime;
    public readonly string id;

    public CoolData(IHasCoolDown cooldown, int index)
    {
        this.remainTime = cooldown.allMaxCooldown[index];
        this.id = cooldown.allCoolDownID[index];

    }

    public bool FinishedCooldown()
    {
        remainTime -= Time.deltaTime;

        if (remainTime <= 0)
        {
            return true;
        }

        return false;
    }
}
   
}