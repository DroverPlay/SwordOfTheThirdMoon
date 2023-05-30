using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour , IInteractable
{
    public ItemScriptableObject item;
    public int amount;

    public string GetDescription()
    {
        return "Press [E] to <color=green>pick up</color> the item";
    }
    public void Interact()
    {

    }

}
