
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Teleport : MonoBehaviour
{
    [SerializeField] private string _scene;
    [SerializeField] private int numberPortal = 0;
    [SerializeField] private bool mazeTeleport;
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( mazeTeleport == true || ContinueData.taked_story_item == true)
        {
            SceneManager.LoadScene(_scene);
            Debug.Log("Внутри");
            ContinueData.taked_story_item = false;
            if (numberPortal != 0)
            {
                if (numberPortal == 1)
                {
                    ContinueData.level2 = true;
                    Debug.Log("Уровень второй доступен!");
                }
                else if (numberPortal == 2)
                {
                    ContinueData.level3 = true;
                    Debug.Log("Уровень третий доступен!");
                }
            }
        }
        else
        {
            player.GetComponent<Story_Trigger>().TriggerDialogue();
        }
    }
}
