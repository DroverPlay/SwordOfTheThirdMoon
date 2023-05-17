using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Default, Food, Weapon, Instrument, Cloth}
public enum ClothType { None, Head, Body, Legs, Feet}

public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public int maximumAmount;
    public ItemType itemType;
    public ClothType clothType = ClothType.None;
    public GameObject itemPrefab;
    public GameObject clothingPrefab;
    public Sprite icon;
    public string itemDescription;
    public bool isConsumeable;
    public string inHandName;

    [Header("Consumeable Characteristics")]
    public float changeHealth;
    public float changeMana;

    [Header("Damage Characteristics")]
    public float damage;
}
