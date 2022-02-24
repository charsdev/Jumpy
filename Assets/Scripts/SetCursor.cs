using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D CursorTexture;
    [SerializeField] private Texture2D cursorNormal;

    void Start()
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
