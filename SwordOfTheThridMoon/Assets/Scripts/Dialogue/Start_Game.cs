using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Game : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private bool start = true;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (start == true)
        {
            _player.gameObject.GetComponent<Story_Trigger>().TriggerDialogue();
            start = false;
        }
    }
}
