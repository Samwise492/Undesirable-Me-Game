using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject controlMenuPanel;
    public GameObject developersMenuPanel;
    
    public void Play() => SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    
    public void Controls() => controlMenuPanel.SetActive(true);
    
    public void Developers() => developersMenuPanel.SetActive(true);
    
    public void Exit() => Application.Quit();
    
    public void OnClickClose() => EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    
}
