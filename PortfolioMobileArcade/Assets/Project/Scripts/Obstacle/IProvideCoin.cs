using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProvideCoin
{
    public void ProvideCoin(int coin);
}

public interface IDestroyable
{
    public void Destroyed(GameObject go);
}

public class StaticAndCoinDestroyable : IDestroyable, IProvideCoin
{
    private int _coin;
    public StaticAndCoinDestroyable(int coin)
    {
        _coin = coin;
    }
    public void Destroyed(GameObject go)
    {
        GameObject.Destroy(go);
        Actions.DestroyObstacle(this);
        ProvideCoin(_coin);
    }

    public void ProvideCoin(int coin)
    {
        Actions.GainCoin(coin);
    }
}
