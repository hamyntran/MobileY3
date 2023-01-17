using System.Collections.Generic;

namespace Framework.CoolDown
{
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
}