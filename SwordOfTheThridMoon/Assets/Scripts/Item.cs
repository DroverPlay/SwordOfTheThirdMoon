using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour , IInteractable
{
    public ItemScriptableObject item;
    public int amount;

    public string GetDescription()
    {
        return "������� [E] ���-�� <color=green>���������</color> �������.";
    }
    public void Interact()
    {

    }

}
