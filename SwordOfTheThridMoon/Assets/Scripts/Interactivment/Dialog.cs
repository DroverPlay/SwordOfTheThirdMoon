using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Dialog : MonoBehaviour , IInteractable
{
    public bool isOpen = false;
    [SerializeField] private GameObject nPC;
    [SerializeField] private GameObject talkObj;
    [SerializeField] private CinemachineVirtualCamera CVC;
    [SerializeField] private Camera _mainCamera;
    
    
    public string GetDescription()
    {
        return "Нажмите [E] что-бы <color=purple>начать</color> диалог.";
    }

    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, 3f))
            {
                if(hit.collider.gameObject.GetComponent<Dialog>() != null)
                {
                    nPC.GetComponent<Story_Trigger>().TriggerDialogue();
                    isOpen = true;
                }
            }
        }
    }

    public void Interact()
    {

    }
}
