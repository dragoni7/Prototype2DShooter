using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class CursorController : Singletone<CursorController>
    {
        [SerializeField] private Texture2D _cursorTexture;

        private Vector2 cursorHotspot;

        void Start()
        {
            cursorHotspot = new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2);
            Cursor.SetCursor(_cursorTexture, cursorHotspot, CursorMode.Auto);
        }
    }
}
