using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    protected UIManager uiManager;
    public UIManager UIManager
    {
        get => uiManager;
    }

    public virtual void Initialize(UIManager mgr)
    {
        uiManager = mgr;
    }
}
