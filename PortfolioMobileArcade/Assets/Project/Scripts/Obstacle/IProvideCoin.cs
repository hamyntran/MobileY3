using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProvideCoin
{
    public void ProvideCoin(int coin);
}

public interface ISwordAttacked
{
    public void Attacked(GameObject go);
}

public class StaticAndCoinSwordAttacked : ISwordAttacked, IProvideCoin
{
    private int _coin;
    public StaticAndCoinSwordAttacked(int coin)
    {
        _coin = coin;
    }
    public void Attacked(GameObject go)
    {
        GameObject.Destroy(go);
        Actions.DestroyObstacle(this);
        ProvideCoin(_coin);
    }

    public void ProvideCoin(int coin)
    {
        Actions.GainCoin?.Invoke(coin);
    }
}
