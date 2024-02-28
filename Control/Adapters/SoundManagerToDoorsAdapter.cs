using UnityEngine;

public class SoundManagerToDoorsAdapter : MonoBehaviour
{
    private SoundManager soundManager => FindObjectOfType<SoundManager>();
    
    private Door[] doors => FindObjectsOfType<Door>(true);

    private void OnEnable()
    {
        foreach (Door door in doors)
        {
            door.OnStageChanged += () => PlayDoorSound(door);

            if (door.TryGetComponent(out SpecificDoorBehaviour behaviour))
                behaviour.OnTriedToOpenClosedDoor += PlayClosedDoorSound;
        }
    }
    private void OnDisable()
    {
        foreach (Door door in doors)
        {
            door.OnStageChanged -= () => PlayDoorSound(door);

            if (door.TryGetComponent(out SpecificDoorBehaviour behaviour))
                behaviour.OnTriedToOpenClosedDoor -= PlayClosedDoorSound;
        }
    }

    private void PlayDoorSound(Door door)
    {
        soundManager.PlaySound(door.TransitionSound);
    }

    private void PlayClosedDoorSound()
    {
        soundManager.PlaySound(SoundManager.SoundType.ClosedDoor);
    }
}
