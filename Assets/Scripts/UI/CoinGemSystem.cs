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

    public Action<int> onCoinCountChange;
    public Action<int> onGemCountChange;

    public void AddCoin(int amount)
    {
        coin += amount;
        onCoinCountChange?.Invoke(Coin);
    }

    public void AddGem(int amount)
    {
        gem += amount;
        onGemCountChange?.Invoke(Gem);
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
        onCoinCountChange?.Invoke(Coin);
    }

    public void PayGem(int amount)
    {
        gem-= amount;
        onGemCountChange?.Invoke(Gem);
    }
}
