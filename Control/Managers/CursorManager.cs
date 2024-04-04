using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public bool isMute;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        Init();
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

    private void Init()
    {
        Cursor.visible = false;

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            gameObject.SetActive(false);
            isMute = true;
        }
        else
        {
            gameObject.SetActive(true);
            isMute = false;
        }
    }
}