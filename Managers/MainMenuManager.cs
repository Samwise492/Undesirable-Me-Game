using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [Space]
    [SerializeField] private RectTransform controlMenuPanel;
    [SerializeField] private RectTransform developersMenuPanel;
    
    public void Play() => SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    
    public void Controls() => controlMenuPanel.gameObject.SetActive(true);
    
    public void Developers() => developersMenuPanel.gameObject.SetActive(true);
    
    public void Exit() => Application.Quit();
    
    public void OnClickClose() => EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
}