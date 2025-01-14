using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance => instance;

    [SerializeField] 
    private SoundData soundData;

    [SerializeField]
    private AudioSource audioSource;

    private static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(SoundType type)
    {
        foreach (Sound sound in soundData.sounds)
        {
            if (sound._name == type.ToString())
            {
                audioSource.clip = sound.audioClip;
            }
        }

        audioSource.Play();
    }

    public enum SoundType
    {
        Door,
        WayIn,
        Stairs,
        ClosedDoor,
        Stamp,
        Gate
    }
}
