using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CursorManager : MonoBehaviour
{
    //[SerializeField] private Sprite cursor;

    //[SerializeField] private GameObject cursorObject;

    [SerializeField] private Texture2D cursor;
    [SerializeField] private Texture2D cursorDown;
    private CursorMode cmode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.SetCursor(cursor, Vector2.zero, cmode);
    }

    void OnMouseClickDown()
    {
        Cursor.SetCursor(cursorDown, Vector2.zero, cmode);
    }

    void OnMouseClickUp()
    {
        Cursor.SetCursor(cursor, Vector2.zero, cmode);
    }
}
