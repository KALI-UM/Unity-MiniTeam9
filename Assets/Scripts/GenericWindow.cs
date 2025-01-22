using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GenericWindow : MonoBehaviour
{
    public GameObject firstSelected;

    protected WindowManager windowManager;

    public void Init(WindowManager mgr)
    {
        windowManager = mgr;
    }

    public void OnFocus()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        OnFocus();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

}
