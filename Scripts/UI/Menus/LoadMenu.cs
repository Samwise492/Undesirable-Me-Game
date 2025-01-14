using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{
    [SerializeField]
    private int maxAvailableLoads;

    [Space]
    [SerializeField]
    private Transform nest;
    [SerializeField]
    private LoadButton loadButtonPrefab;

    private List<LoadButton> buttons = new();

    private void Start()
    {
        for (int i = 0; i < maxAvailableLoads; i++)
        {
            LoadButton button = Instantiate(loadButtonPrefab, nest);
            button.loadIndex = i;

            buttons.Add(button);
        }
    }
    public void UpdatePresentation()
    {
        if (buttons.Count > 0)
        {
            foreach (var button in buttons)
            {
                button.ChangePresentation();
            }
        }
    }
}
