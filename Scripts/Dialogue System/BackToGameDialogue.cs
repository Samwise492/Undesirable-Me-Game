using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGameDialogue : MonoBehaviour
{
    [SerializeField]
    private SimpleDialogue greetingDialogue;

    [SerializeField]
    private DialogueData[] dialogueData;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneNameData.MainMenu)
        {
            greetingDialogue.isStopCheck = true;
            return;
        }

        greetingDialogue.OnDialogueFinished += SetFinish;

        if (!PlayerSaveLoadProvider.Instance.GetCurrentSave().isGreeted)
        {
            greetingDialogue.InjectDialogueData(PickDialogue());
            greetingDialogue.Play();
        }
    }
    private void OnDestroy()
    {
        greetingDialogue.OnDialogueFinished -= SetFinish;
    }

    private DialogueData PickDialogue()
    {
        return dialogueData[Random.Range(0, dialogueData.Length)];
    }

    private void SetFinish(DialogueData data)
    {
        PlayerSaveStorage.PlayerSave save = PlayerSaveLoadProvider.Instance.GetCurrentSave();
        save.isGreeted = true;
        PlayerSaveLoadProvider.Instance.SaveInTemp(save);

        greetingDialogue.isStopCheck = true;
    }
}