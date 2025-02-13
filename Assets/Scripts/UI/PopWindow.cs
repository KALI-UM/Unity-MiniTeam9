using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopWindow : UIElement
{
    public float popDuration = 1f;

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
        yield return new WaitForSeconds(popDuration);
        Close();
    }
}
