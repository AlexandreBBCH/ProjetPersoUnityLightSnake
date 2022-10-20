using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class animationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D pointer;
    public Texture2D pointerNull;
    /// <summary>
    /// Animation de grandissement
    /// </summary>
    Vector3 initScal;
    private void Awake()
    {
        initScal = this.transform.localScale;

    }

    private void Update()
    {

        //pointer = Resources.Load<Texture2D>("Assets/img/mouse/pointerPinkAndBlue.png");
    
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3((float)1.2, (float)1.2, (float)1.2);
        Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = initScal;
        Cursor.SetCursor(pointerNull, Vector2.zero, CursorMode.Auto);
    }
}
