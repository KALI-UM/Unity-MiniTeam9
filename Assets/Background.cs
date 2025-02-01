using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Background : UIElement, IPointerClickHandler
{
   
    public void OnPointerClick(PointerEventData eventData)
    {
        uiManager.OnClickNotUIArea();
    }
}
