using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;

    public void SetDialogue(string speaker, string text)
    {
        speakerText.text = speaker;
        dialogueText.text = text;
    }

    public void ShowPanel(bool show)
    {
        panel.SetActive(show);
    }

    public bool IsDialogueActive()
    {
        return panel.activeSelf;
    }
}
