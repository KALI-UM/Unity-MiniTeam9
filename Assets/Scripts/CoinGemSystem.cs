using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CoinGemSystem
{
    [ReadOnly, SerializeField]
    private int coin;
    public int Coin
    {
        get => coin;
    }

    [ReadOnly, SerializeField]
    private int gem;
    public int Gem
    {
        get => gem;
    }

    public void AddCoin(int amount)
    {
        coin += amount;
    }

    public void AddGem(int amount)
    {
        gem += amount;
    }

    public bool CanPayCoin(int amount)
    {
        return coin >= amount;
    }

    public bool CanPayGem(int amount)
    {
        return gem >= amount;
    }

    public void PayCoin(int amount)
    {
        coin-=amount;
    }

    public void PayGem(int amount)
    {
        gem-= amount;
    }
}
