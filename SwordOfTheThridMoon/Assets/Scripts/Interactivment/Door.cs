using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public Animator m_Animator;
    public bool isOpen;
    [SerializeField] private string sceneName;
    [SerializeField] private bool isTeleport;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (isOpen)
        {
            m_Animator.SetBool("isOpen", true);
        }
    }

    public string GetDescription()
    {
        if (isOpen) return "Нажмите [E] что-бы <color=red>закрыть</color> дверь.";
        return "Нажмите [E] что-бы <color=green>открыть</color> дверь.";
    }

    public void Interact()
    {
        if (isTeleport == true)
        {
            SceneManager.LoadScene(sceneName);
            if (ContinueData.startGame == false)
            {
                ContinueData.startGame = true;
            }
        }
        else
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                m_Animator.SetBool("isOpen", true);
            }
            else
            {
                m_Animator.SetBool("isOpen", false);
            }
        }
    }
}
