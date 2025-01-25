using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopWindow : MonoBehaviour
{
    protected WindowManager windowManager;

    public void Initialize(WindowManager mgr)
    {
        windowManager = mgr;
    }

    public float popTime = 1f;

    public virtual void Open()
    {
        gameObject.SetActive(true);
        StartCoroutine(CoPop());
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator CoPop()
    {
        yield return new WaitForSeconds(popTime);
        Close();
    }
}
