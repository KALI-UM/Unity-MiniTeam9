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

    protected virtual void EnableInput()
    {
        UIManager.eventSystem.enabled = true;
    }

    protected virtual void DisableInput()
    {
        UIManager.eventSystem.enabled = false;
    }

    private IEnumerator CoInputThreshold()
    {
        DisableInput();
        yield return new WaitForSecondsRealtime(inputThreshold);
        EnableInput();
    }
}
