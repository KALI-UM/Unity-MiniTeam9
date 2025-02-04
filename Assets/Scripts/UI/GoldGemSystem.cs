using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GoldGemSystem
{
    [ReadOnly, SerializeField]
    private int gold;
    public int Gold
    {
        get => gold;
    }

    [ReadOnly, SerializeField]
    private int gem;
    public int Gem
    {
        get => gem;
    }

    public Action<int> onGoldCountChange;
    public Action<int> onGemCountChange;

    public Action onGoldPayFail;
    public Action onGemPayFail;

    public void AddGold(int amount)
    {
        gold += amount;
        onGoldCountChange?.Invoke(Gold);
    }

    public void AddGem(int amount)
    {
        gem += amount;
        onGemCountChange?.Invoke(Gem);
    }

    public bool CanPayGold(int amount)
    {
        return gold >= amount;
    }

    public bool CanPayGem(int amount)
    {
        return gem >= amount;
    }

    public bool PayGold(int amount)
    {
        if (!CanPayGold(amount))
        {
            onGoldPayFail?.Invoke();
            return false;
        }

        gold -= amount;
        onGoldCountChange?.Invoke(Gold);
        return true;
    }

    public bool PayGem(int amount)
    {
        if (!CanPayGem(amount))
        {
            onGemPayFail?.Invoke();
            return false;
        }

        gem -= amount;
        onGemCountChange?.Invoke(Gem);
        return true;
    }
}
