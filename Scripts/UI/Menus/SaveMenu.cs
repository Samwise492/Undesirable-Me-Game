using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    public event Action OnSaved;

    [SerializeField]
    private int maxAvailableSaves;

    [Space]
    [SerializeField]
    private Transform nest;
    [SerializeField]
    private SaveButton saveButtonPrefab;

    private List<SaveButton> buttons = new();

    private void Start()
    {
        for (int i = 0; i < maxAvailableSaves; i++)
        {
            SaveButton button = Instantiate(saveButtonPrefab, nest);
            button.saveIndex = i;
            button.OnSaved += NotifyOnSaved;

            buttons.Add(button);
        }
    }
    private void OnDestroy()
    {
        foreach (var button in buttons)
        {
            button.OnSaved -= OnSaved.Invoke;
        }
    }

    private void NotifyOnSaved()
    {
        OnSaved?.Invoke();
    }
}
