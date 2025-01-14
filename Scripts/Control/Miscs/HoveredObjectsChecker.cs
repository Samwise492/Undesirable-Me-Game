using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoveredObjectsChecker : MonoBehaviour
{
    public event Action<GameObject> OnHovered;

    public GameObject CurrentHoveredGO => currentHoveredGO;

    [SerializeField]
    private List<GameObject> targetObjects;

#if UNITY_EDITOR
    [ReadOnly]
#endif
    [SerializeField]
    private GameObject currentHoveredGO;

    private bool isEventRaised;

    private void Update()
    {
        CheckHovering(GetEventSystemRaycastResults());
    }

    // Returns 'true' if we touched or hovering on Unity UI element.
    private void CheckHovering(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult currentRaysastResult = eventSystemRaysastResults[index];
            
            if (targetObjects.Count > 0)
            {
                if (targetObjects.Contains(currentRaysastResult.gameObject))
                {
                    currentHoveredGO = currentRaysastResult.gameObject;
                }
                else
                {
                    currentHoveredGO = null;
                }
            }
            else
            {
                currentHoveredGO = currentRaysastResult.gameObject;
            }

            if (!isEventRaised)
            {
                if (currentHoveredGO)
                {
                    isEventRaised = true;
                    OnHovered?.Invoke(currentHoveredGO);
                }
            }
        }

        isEventRaised = false;
    }

    // Gets all event system raycast results of current mouse or touch position.
    private List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        return raysastResults;
    }
}
