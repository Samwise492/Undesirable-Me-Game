using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance => instance;

    public bool isMute;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private static CursorManager instance;

    private bool isTemporary;

    private void Awake()
    {
        instance = this;
    }
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

    public void SetTemporaryCursor(bool state)
    {
        isTemporary = true;
        SetCursor(state);
    }
    public void SetCursor(bool state)
    {
        Cursor.visible = false;

        if (state)
        {
            isMute = false;
            spriteRenderer.enabled = true;
        }
        else if (!isTemporary)
        {
            isMute = true;
            spriteRenderer.enabled = false;
        }

        isTemporary = false;
    }

    private void Init()
    {
        if (SceneManager.GetActiveScene().name != SceneNameData.MainMenu)
        {
            SetCursor(false);
        }
        else
        {
            SetCursor(true);
        }
    }
}