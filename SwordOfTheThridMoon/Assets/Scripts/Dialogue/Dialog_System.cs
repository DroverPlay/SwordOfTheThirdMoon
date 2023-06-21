using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Dialog_System : MonoBehaviour
{
    [SerializeField] private Queue<string> texts;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private bool talkEnd;
    [SerializeField] private CinemachineVirtualCamera CVC;
    public TMP_Text nameNPC;
    public GameObject dialogManager;
    private Animator animator;
    private bool endingDialogue;

    void Start()
    {
        texts = new Queue<string>();
        animator = dialogManager.GetComponent<Animator>();
        dialogManager.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        ContinueData.dialogue = true;
        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = 0;
        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dialogManager.SetActive(true);
        animator.SetBool("Dialogue", true);
        nameNPC.text = dialogue.name;

        texts.Clear();
        if (endingDialogue == true)
        {
            foreach (string text in dialogue.endDialogue)
            {
                texts.Enqueue(text);
            }
        }
        else
        {
            foreach (string text in dialogue.texts)
            {
                texts.Enqueue(text);
            }
        }

        DisplayNextText();
    }

    public void DisplayNextText()
    {
        if(texts.Count == 0)
        {
            EndDialogue();
            return;
        }

        string text = texts.Dequeue();
        StartCoroutine(TypeText(text));

    }

    IEnumerator TypeText (string text)
    {
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator.SetBool("Dialogue", false);
        ContinueData.dialogue = false;
        endingDialogue = true;
        Debug.Log("Конец разговора");
    }
}
