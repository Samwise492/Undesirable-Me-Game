using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool isMute;

    private AudioSource clickSound => GetComponent<AudioSource>();

    private void Start()
    {
        DontDestroyOnLoad(this);

        Cursor.visible = false;
    }
    
    private void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;

        if (Input.GetMouseButtonDown(0) && !isMute)
        {
            clickSound.Play();
        } 
    }
}