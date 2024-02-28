using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool isMute;

    [SerializeField]
    private AudioSource audioSource;

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
            audioSource.Play();
        } 
    }
}