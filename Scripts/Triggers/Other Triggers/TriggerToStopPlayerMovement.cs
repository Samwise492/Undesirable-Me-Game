using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToStopPlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private bool isPermanent;
    
    [SerializeField] 
    private float stopTime;
    
    private Player player;
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
        
        player.ProhibitMovement();

        if (!isPermanent)
        {
            StartCoroutine(DelayBeforeAllowance());
        }
        else
        {
            player.isPermanentMovingProhibition = true;
        }
    }

    private IEnumerator DelayBeforeAllowance()
    {
        yield return new WaitForSeconds(stopTime);

        if (UIManager.Instance.GetActiveWindow() == null)
        {
            player.AllowMovement();
        }
        
        yield break;
    }
}
