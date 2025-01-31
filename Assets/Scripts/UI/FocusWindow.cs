using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FocusWindow : UIElement
{
    public float inputThreshold = 0.5f;

    public virtual void OnFocus()
    {

    }

    public virtual void OnOutFocus()
    {

    }

    public virtual void Open()
    {
        OnFocus();
        gameObject.SetActive(true);
        StartCoroutine(CoInputThreshold());
    }

    public virtual void Close()
    {
        gameObject.SetActive(false); 
    }

    private IEnumerator CoInputThreshold()
    {
        enabled = false;
        yield return new WaitForSecondsRealtime(inputThreshold);
        enabled = true;
    }
}
