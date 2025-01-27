using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FocusWindow : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void Initialize(UIManager mgr)
    {
        uiManager = mgr;
    }

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
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }


}
