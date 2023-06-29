using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoolDownManager : SingletonMonoBehaviour<CoolDownManager>
{
    public List<CoolData> currentCooldownData = new List<CoolData>();

    public void PutOnCooldown(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        if (IsOnCooldown(cooldown, purpose, obj))
        {
            return;
        }
        
        int index = GetIndex(cooldown, purpose, obj);
        if (index < 0)
        {
            return;
        }

        currentCooldownData.Add(new CoolData(cooldown, index));
    }
    
    public void RemoveCooldown(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        if (IsOnCooldown(cooldown, purpose, obj))
        {
            int index = GetIndex(cooldown, purpose, obj);
            if (index < 0)
            {
                return;
            }

            var d = currentCooldownData.Where(f => f.id == cooldown.allCoolDownID[index]).FirstOrDefault();
            currentCooldownData.Remove(d);
        }
    }

    public int GetIndex(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        for (int i = 0; i < cooldown.allCoolDownID.Count; i++)
        {
            if (cooldown.allCoolDownID[i] == purpose.ToString() + obj.GetInstanceID())
            {
                return i;
            }
        }

        return -1;
    }

    public bool IsOnCooldown(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        int index = GetIndex(cooldown, purpose, obj);
        
        if (index < 0)
        {
            return false;
        }
        
        foreach (var data in currentCooldownData)
        {
            if (data.id == cooldown.allCoolDownID[index])
            {
                return true;
            }
        }

        return false;
    }

    private CoolData GetCoolDown(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        int index = GetIndex(cooldown, purpose, obj);
        
        if (index < 0)
        {
            return null;
        }
        
        foreach (var data in currentCooldownData)
        {
            if (data.id == cooldown.allCoolDownID[index])
            {
                return data;
            }
        }

        return null;
    }

    public float GetRemainingTime(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        return (GetCoolDown(cooldown, purpose, obj) != null)? GetCoolDown(cooldown, purpose, obj).remainTime : 0;
    }

    public bool IsCoolingDown(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        
        if (IsOnCooldown(cooldown, purpose, obj))
        {
            var cooldata = GetCoolDown(cooldown, purpose, obj);
            if (cooldata != null)
            {
                if (!cooldata.FinishedCooldown())
                {
                    return true;
                }
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

    public void SetRemainTime(IHasCoolDown cooldown, CoolDownID purpose, GameObject obj)
    {
        int index = GetIndex(cooldown, purpose, obj);
        if (index < 0)
        {
            return;
        }

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

[System.Serializable]
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