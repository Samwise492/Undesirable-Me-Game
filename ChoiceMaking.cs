using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceMaking : MonoBehaviour
{
    [SerializeField] string choiceText_One, choiceText_Two;
    [SerializeField] int dialogueNumber_One, dialogueNumber_Two;
    List<Button> buttons = new List<Button>();

    void Start()
    {
        GameObject.Find("GUI").transform.GetChild(0).gameObject.SetActive(true);
        foreach (Transform choiceLine in GameObject.Find("GUI").transform.GetChild(0).GetChild(0).transform)
        {
            choiceLine.gameObject.SetActive(true);
            buttons.Add(choiceLine.gameObject.GetComponent<Button>());
        }

        buttons[0].onClick.AddListener(() => MakeChoice(1));
        buttons[0].transform.GetChild(0).GetComponent<Text>().text = choiceText_One;
        buttons[1].onClick.AddListener(() => MakeChoice(2));
        buttons[1].transform.GetChild(0).GetComponent<Text>().text = choiceText_Two;
    }

    void MakeChoice(int lineObjectNumber)
    {
        if (lineObjectNumber == 1)
        {
            gameObject.GetComponent<Talking>().PlayDialogue(dialogueNumber_One);
        }
        else if (lineObjectNumber == 2)
        {
            gameObject.GetComponent<Talking>().PlayDialogue(dialogueNumber_Two);
        }

        foreach (Transform choiceLine in GameObject.Find("GUI").transform.GetChild(0).GetChild(0).transform)
        {
            choiceLine.gameObject.SetActive(false);
        }
    }
}
