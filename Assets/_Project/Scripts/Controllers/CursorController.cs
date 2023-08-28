using UnityEngine;
using Util;

namespace dragoni7
{
    /// <summary>
    /// Controller class for the cursor
    /// </summary>
    public class CursorController : Singleton<CursorController>
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
