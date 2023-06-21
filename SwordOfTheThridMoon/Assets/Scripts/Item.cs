using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour , IInteractable
{
    public ItemScriptableObject item;
    public int amount;

    public string GetDescription()
    {
        return "Нажмите [E] что-бы <color=green>подобрать</color> предмет.";
    }
    public void Interact()
    {

    }

}
