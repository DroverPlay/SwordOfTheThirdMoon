using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public Animator m_Animator;
    public bool isOpen;

    void Start()
    {
        if (isOpen)
            m_Animator.SetBool("isOpen", true);
    }

    public string GetDescription()
    {
        if (isOpen) return "Нажмите [E] что-бы <color=red>закрыть</color> дверь.";
        return "Нажмите [E] что-бы <color=green>открыть</color> дверь.";
    }

    public void Interact()
    {
        isOpen = !isOpen;
        if (isOpen)
            m_Animator.SetBool("isOpen", true);
        else
            m_Animator.SetBool("isOpen", false);
    }
}
