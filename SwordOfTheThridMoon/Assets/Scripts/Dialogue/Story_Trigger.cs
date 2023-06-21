using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Trigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<Dialog_System>().StartDialogue(dialogue);
    }
}
