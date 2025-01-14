using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraVFX : MonoBehaviour
{
    private Camera cameraComponent => GetComponent<Camera>();

    [Header("Shaking")]
    [SerializeField]
    private float shakingIncrement;

    [Space]
    [SerializeField]
    private float minOrtographicSize;
    [SerializeField]
    private float maxOrtographicSize;

    public bool isShaking;
    private bool isIncrementDescending;

    private float defaultDistance;

    private void Start()
    {
        defaultDistance = cameraComponent.orthographicSize;
    }

    private void Update()
    {
        if (isShaking)
        {
            if (!isIncrementDescending)
            {
                cameraComponent.orthographicSize += shakingIncrement;

                if (cameraComponent.orthographicSize >= maxOrtographicSize)
                {
                    isIncrementDescending = true;
                }
            }
            else
            {
                cameraComponent.orthographicSize -= shakingIncrement;

                if (cameraComponent.orthographicSize <= minOrtographicSize)
                {
                    isIncrementDescending = false;
                }
            }
        }
    }

    public void ApplyShaking(bool state)
	{
        isShaking = state;

        if (state == false)
        {
            cameraComponent.orthographicSize = defaultDistance;
        }
	}
}