using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Talking))]
public class ActivateDialogue : MonoBehaviour
{
    void OnDisable() => Destroy(this.gameObject);
    void Start() => StartCoroutine(WaitForDialogue());

    IEnumerator WaitForDialogue()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Talking>().PlayDialogue(gameObject.GetComponent<Talking>().dialogueNumber);

        yield break;
    }
}
