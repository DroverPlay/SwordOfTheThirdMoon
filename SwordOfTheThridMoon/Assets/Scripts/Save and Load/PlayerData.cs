using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public float health;
    public float mana;

    public float[] position;

    public string[] itemNames;
    public int[] itemAmounts;

    public PlayerData(Indicators indicators, CharacterController player, InventoryManager inventoryManager)
    {
        health = indicators.healthAmount;
        mana = indicators.manaAmount;


        position = new float[3];
        var playerPosition = player.transform.position;
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        itemNames = new string[inventoryManager.slots.Count];
        itemAmounts = new int[inventoryManager.slots.Count];

        for (int i = 0; i < inventoryManager.slots.Count; i++)
        {
            if (inventoryManager.slots[i].item != null)
                itemNames[i] = inventoryManager.slots[i].item.name;
        }

        for (int i = 0; i < inventoryManager.slots.Count; i++)
        {
            if (inventoryManager.slots[i].item != null)
                itemAmounts[i] = inventoryManager.slots[i].amount;
        }
    }
}
