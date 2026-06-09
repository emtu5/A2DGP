using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursor;

    void Start()
    {
        Debug.Log("Setting cursor");

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
