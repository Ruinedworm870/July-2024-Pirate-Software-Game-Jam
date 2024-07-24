using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleClickable : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D clickableCursor;
    
    private void Start()
    {
        d = defaultCursor;
        c = clickableCursor;
    }

    private static int hoveredClickables = 0;
    private static Texture2D d;
    private static Texture2D c;

    public static void AddHoveredClickable()
    {
        if(hoveredClickables == 0)
        {
            Cursor.SetCursor(c, Vector2.zero, CursorMode.Auto);
        }

        hoveredClickables += 1;
    }

    public static void RemoveHoveredClickable()
    {
        hoveredClickables -= 1;
        
        if(hoveredClickables == 0)
        {
            Cursor.SetCursor(d, Vector2.zero, CursorMode.Auto);
        }
    }
}
