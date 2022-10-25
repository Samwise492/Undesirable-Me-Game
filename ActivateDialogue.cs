using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Talking))]
public class ActivateDialogue : MonoBehaviour
{
    [SerializeField] int dialogueNumber;

    void Start() 
    {
        StartCoroutine(WaitForDialogue());
    }
    void OnDisable() => Destroy(this.gameObject);

    IEnumerator WaitForDialogue()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Talking>().PlayDialogue(dialogueNumber);

        yield break;
    }
}
