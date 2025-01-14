using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WIPPresenter : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private float pauseTime;

    [Header("Sound")]
    [SerializeField]
    private float timetToChangeVolume;
    [SerializeField]
    private float startVolume;
    [SerializeField]
    private float endVolume;

    [Header("Colour")]
    [SerializeField]
    private float timeToChangeColour;
    [SerializeField]
    private Color startColour;
    [SerializeField]
    private Color endColour;

    private void Start()
    {
        audioSource.volume = startVolume;

        ChangeTitleColour();
    }

    private void ChangeTitleColour()
    {
        Sequence seq = DOTween.Sequence();

        Tween show = DOTween.To(() => title.color, x => title.color = x, endColour, timeToChangeColour);
        Tween raiseVolume = DOTween.To(() => audioSource.volume, x => audioSource.volume = x, endVolume, timetToChangeVolume);

        seq.Append(raiseVolume);
        seq.AppendInterval(pauseTime).onComplete += () => SceneManager.LoadSceneAsync(SceneNameData.MainMenu, LoadSceneMode.Single);
    }
}
