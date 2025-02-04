using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FocusWindow : UIElement
{
    public float inputThreshold = 0f;

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

    protected virtual void EnableInput()
    {
        //UIManager.eventSystem.enabled = true;
        //enabled = false;
    }

    protected virtual void DisableInput()
    {
        //UIManager.eventSystem.enabled = false;
        //enabled = true;
    }

    protected IEnumerator CoInputThreshold()
    {
        DisableInput();
        yield return new WaitForSecondsRealtime(inputThreshold);
        EnableInput();
    }
}
