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
        //SoundManager.Instance.PlaySFX("BattleEffect_08_GainGold");
    }

    public void AddGem(int amount)
    {
        gem += amount;
        onGemCountChange?.Invoke(Gem);
        //SoundManager.Instance.PlaySFX("BattleEffect_08_GainCristal");
    }

    public bool CanPayGold(int amount)
    {
        return gold >= amount;
    }

    public bool CanPayGem(int amount)
    {
        return gem >= amount;
    }

    public bool TryPayGold(int amount)
    {
        if (!CanPayGold(amount))
        {
            onGoldPayFail?.Invoke();
            return false;
        }

        PayGold(amount);
        return true;
    }

    public bool TryPayGem(int amount)
    {
        if (!CanPayGem(amount))
        {
            onGemPayFail?.Invoke();
            return false;
        }

        PayGem(amount);
        return true;
    }

    public void PayGold(int amount)
    {
        gold -= amount;
        onGoldCountChange?.Invoke(Gold);
    }

    public void PayGem(int amount)
    {
        gem -= amount;
        onGemCountChange?.Invoke(Gem);
    }
}
