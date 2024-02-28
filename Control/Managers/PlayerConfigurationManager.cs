using System;
using System.Collections;
using UnityEngine;

public class PlayerConfigurationManager : MonoBehaviour
{
    public event Action OnStartEnding;

    [SerializeField] 
    private PlayerConfiguration playerConfiguration;

    private void Start()
    {
        playerConfiguration.OnPointsAdded.AddListener(() => StartCoroutine(CheckForEnding()));
    }
    private void OnDestroy()
    {
        playerConfiguration.OnPointsAdded.RemoveAllListeners();
    }

    private IEnumerator CheckForEnding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (UIManager.Instance.GetActiveWindow() == null)
            {
                if (playerConfiguration.DeathPoints >= 3)
                {
                    OnStartEnding?.Invoke();
                }
                else if (playerConfiguration.BadPoints >= 2 || playerConfiguration.GoodPoints >= 2)
                {
                    if (GameManager.IsEndScene)
                    {
                        OnStartEnding?.Invoke();
                    }
                }

                yield break;
            }
        }
    }


}