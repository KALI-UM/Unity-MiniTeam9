using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizationText : MonoBehaviour
{
    public string stringId;
    private string textString;
#if UNITY_EDITOR
    static public Languages editorLang;
#endif
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            OnChangeLanguage(Variables.currentLang);
        }
        else
        {
#if UNITY_EDITOR
            OnChangeLanguage(editorLang);
#endif
        }
    }

    public void OnChangeLanguage(Languages lang)
    {
        if (stringId.NullIfEmpty() == null)
        {
            return;
        }

        var stringTableId = DataTableIds.String[(int)lang];
        var stringTable = DataTableManager.Get<StringTable>(stringTableId);

        textString = stringTable.Get(stringId);
        text.text = textString;
    }
    public void OnStringIdChange(string id)
    {
        stringId = id;
        OnChangeLanguage(Variables.currentLang);
    }

    public void OnTextParamChange(params string[] elements)
    {
        text.text = string.Format(textString, elements);
    }

}
