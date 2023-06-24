using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundData soundData;
    
    private AudioSource currentSound => GetComponent<AudioSource>();

    public void PlaySound(SoundType type)
    {
        foreach (Sound sound in soundData.sounds)
        {
            if (sound._name == type.ToString())
            {
                currentSound.clip = sound.audioClip;
            }
        }

        currentSound.Play();
    }

    public enum SoundType
    {
        Door,
        WayIn,
        Stairs,
        ClosedDoor,
        Stamp
    }
}
