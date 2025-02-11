using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float duration=1f;
    public Action release;

    public virtual void Play(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public virtual void Reset()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnGet()
    {

    }

    public virtual void Release()
    {
        release?.Invoke();
    }
}
