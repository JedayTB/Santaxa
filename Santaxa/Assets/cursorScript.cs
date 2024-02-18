using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorImage;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }
}
