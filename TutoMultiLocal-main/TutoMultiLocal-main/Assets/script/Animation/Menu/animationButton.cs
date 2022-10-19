using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class animationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 initScal;
    private void Awake()
    {
        initScal = this.transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3((float)1.2, (float)1.2, (float)1.2);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = initScal;
    }
}
