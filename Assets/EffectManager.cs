using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EffectManager : InGameManager
{
    public List<GameObject> effectPrefabs;
    private readonly Dictionary<eEffects, ObjectPool<Effect>> effectPools = new();

    private void Awake()
    {


        InitializeEffectPools();
    }

    private void InitializeEffectPools()
    {
        effectPools.Clear();

        for (int i = 0; i < effectPrefabs.Count; i++)
        {
            var eff = effectPrefabs[i];
            var id = (eEffects)i;
            ObjectPool<Effect> pool = new
            (
                createFunc: () => CreateEffect(eff, id),
                actionOnGet: e => e.OnGet(),
                //actionOnRelease: e => e.OnRelease(),
                //actionOnDestroy: obj => obj.Dispose(),
                //collectionCheck: false,
                defaultCapacity: 100,
                maxSize: 200
            );

            effectPools.Add(id, pool);
        }
    }

    public Effect CreateEffect(GameObject prefab, eEffects id)
    {
        var gobj=Instantiate(prefab);
        Effect effect = gobj.GetComponent<Effect>();
        effect.release += () => effectPools[id].Release(effect);
        effect.Reset();
        return effect;
    }

    public Effect Get(eEffects Id)
    {
        return effectPools[Id].Get();
    }

    public T Get<T>(eEffects Id) where T : Effect
    {
        return effectPools[Id].Get() as T;
    }

    public Effect Play(eEffects Id, Vector3 position)
    {
        var effect = effectPools[Id].Get();
        effect.Play(position);
        return effect;
    }
}
