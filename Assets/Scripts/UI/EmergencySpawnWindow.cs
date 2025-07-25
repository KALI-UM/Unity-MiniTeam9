using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmergencySpawnWindow : FocusWindow
{
    [SerializeField]
    private Button windowOpen;

    [SerializeField]
    private List<EmergencySpawnButton> spawnButtons;


    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        windowOpen.onClick.AddListener(() => UIManager.Open(FocusWindows.EmergencySpwanWindow));

        foreach (var button in spawnButtons)
        {
            button.Initialize(mgr);
        }
    }
    public override void OnOutFocus()
    {
        base.OnOutFocus();
        Close();
    }
}
