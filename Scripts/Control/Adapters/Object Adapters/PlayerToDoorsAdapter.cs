using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerToDoorsAdapter : MonoBehaviour
{
    private Player player => FindObjectOfType<Player>();

    private Door[] dialoguesOnScene => FindObjectsOfType<Door>(true);

    private void OnEnable()
    {
        Invoke(nameof(Init), 0.2f);
    }
    private void OnDisable()
    {
        if (player)
        {
            foreach (Door door in dialoguesOnScene)
            {
                door.OnTeleport -= () => TeleportPlayer(door);
            }
        }
    }

    private void TeleportPlayer(Door door)
    {
        if (door.TeleportPosition == null && !door.IsLoadNewDay)
        {
            player.transform.position = new Vector3(0f, player.transform.position.y, player.transform.position.z);
        }
        else if (!door.IsLoadNewDay)
        {
            player.transform.localPosition = door.TeleportPosition.localPosition;
        }
    }

    private void Init()
    {
        if (player)
        {
            foreach (Door door in dialoguesOnScene)
            {
                door.OnTeleport += () => TeleportPlayer(door);
            }
        }
    }
}
