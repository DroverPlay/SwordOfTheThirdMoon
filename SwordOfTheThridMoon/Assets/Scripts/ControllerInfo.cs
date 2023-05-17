using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInfo : MonoBehaviour
{
    public GameObject InfoPanel;
    private bool isOn;
    private void Start()
    {
        InfoPanel.SetActive(true);
        isOn = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isOn = !isOn;
            if (isOn)
            {
                InfoPanel.SetActive(true);
            }
            else
            {
                InfoPanel.SetActive(false);
            }
        }
    }
}
