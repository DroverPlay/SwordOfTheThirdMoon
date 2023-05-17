using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Transform player;
    private QuickslotInventory quickslotInventory;
    public List<ClothAdder> clothAdders;
    [SerializeField] public Transform savingEnvironment;

    private void Start()
    { 
        quickslotInventory = FindObjectOfType<QuickslotInventory>();
        player = GameObject.FindObjectOfType<CharacterController>().transform;
        oldSlot = transform.GetComponentInParent<InventorySlot>();

        //Одежда 
        if (oldSlot.clothType != ClothType.None)
        {
            clothAdders = new List<ClothAdder>();
            clothAdders.AddRange(FindObjectsOfType<ClothAdder>());
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        GetComponentInChildren<Image>().raycastTarget = true;

        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIBG")
        {
            //Выбрасываение одежды
            if (oldSlot.clothType != ClothType.None && oldSlot.item != null)
            {
                foreach (ClothAdder clothAdder in clothAdders)
                {
                    clothAdder.RemoveClothes(oldSlot.item.clothingPrefab);
                }
            }
            //Выбрасывание половины или большей части
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                itemObject.transform.SetParent(savingEnvironment);
                itemObject.GetComponent<Item>().amount = Mathf.CeilToInt((float)oldSlot.amount / 2);
                oldSlot.amount -= Mathf.CeilToInt((float)oldSlot.amount / 2);
                oldSlot.itemAmountText.text = oldSlot.amount.ToString();
            }
            //Выбрасывание всего 1 предмета
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                itemObject.transform.SetParent(savingEnvironment);
                itemObject.GetComponent<Item>().amount = 1;
                oldSlot.amount--;
                oldSlot.itemAmountText.text = oldSlot.amount.ToString();
            }
            else
            {
                GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                itemObject.transform.SetParent(savingEnvironment);
                itemObject.GetComponent<Item>().amount = oldSlot.amount;
                NullifySlotData();
            }
            quickslotInventory.CheckItemInHand();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent == null)
        {
            return;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            //Перемещение данных из одного слота в другие слоты 
            InventorySlot inventorySlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>();

            //Перенесено снизу сюда
            if (oldSlot.clothType != ClothType.None && oldSlot.item != null)
            {
                foreach (ClothAdder clothAdder in clothAdders)
                {
                    clothAdder.RemoveClothes(oldSlot.item.clothingPrefab);
                }
            }

            if (inventorySlot.clothType != ClothType.None)
            {
                if (inventorySlot.clothType == oldSlot.item.clothType)
                {
                    ExchangeSlotData(inventorySlot);
                    foreach (ClothAdder clothAdder in inventorySlot.GetComponentInChildren<DragAndDropItem>().clothAdders)
                    {
                        clothAdder.addClothes(inventorySlot.item.clothingPrefab);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                ExchangeSlotData(inventorySlot);
                quickslotInventory.CheckItemInHand();
            }
        }

        if (oldSlot.amount <= 0)
        {
            NullifySlotData();
        }


    }
    public void NullifySlotData()
    {
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmountText.text = "";
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemScriptableObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.iconGO;
        TMP_Text itemAmountText = newSlot.itemAmountText;
        if (item == null)
        {
            if (oldSlot.item.maximumAmount > 1 && oldSlot.amount > 1)
            {
                //Разделение стака на равные части
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    newSlot.item = oldSlot.item;
                    newSlot.amount = Mathf.CeilToInt((float)oldSlot.amount / 2);
                    newSlot.isEmpty = false;
                    newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
                    newSlot.itemAmountText.text = newSlot.amount.ToString();
                    oldSlot.amount = Mathf.FloorToInt((float)oldSlot.amount / 2);
                    oldSlot.itemAmountText.text = oldSlot.amount.ToString();
                    return;
                }
                //Взять 1 предмет из стака
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    newSlot.item = oldSlot.item;
                    newSlot.amount = 1;
                    newSlot.isEmpty = false;
                    newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
                    newSlot.itemAmountText.text = newSlot.amount.ToString();
                    oldSlot.amount--;
                    oldSlot.itemAmountText.text = oldSlot.amount.ToString();

                    return;
                }
            }
        }
        //Стаканье предметов
        if (newSlot.item != null)
        {
            if (oldSlot.item.name.Equals(newSlot.item.name))
            {
                if (Input.GetKey(KeyCode.LeftShift) && oldSlot.amount > 1)
                {
                    if (Mathf.CeilToInt((float)oldSlot.amount / 2) < newSlot.item.maximumAmount - newSlot.amount)
                    {
                        newSlot.amount += Mathf.CeilToInt((float)oldSlot.amount / 2);
                        newSlot.itemAmountText.text = newSlot.amount.ToString();
                        oldSlot.amount -= Mathf.CeilToInt((float)oldSlot.amount / 2);
                        oldSlot.itemAmountText.text = oldSlot.amount.ToString();
                    }
                    else
                    {
                        int difference = newSlot.item.maximumAmount - newSlot.amount;
                        newSlot.amount = newSlot.item.maximumAmount;
                        newSlot.itemAmountText.text = newSlot.amount.ToString();
                        oldSlot.amount -= difference;
                        oldSlot.itemAmountText.text = newSlot.amount.ToString();
                    }
                    return;
                }
                else if (Input.GetKey(KeyCode.LeftControl) && oldSlot.amount > 1)
                {
                    if (newSlot.item.maximumAmount != newSlot.amount)
                    {
                        newSlot.amount++;
                        newSlot.itemAmountText.text = newSlot.amount.ToString();
                        oldSlot.amount--;
                        oldSlot.itemAmountText.text = oldSlot.amount.ToString();
                    }
                    return;
                }
                else
                {
                    if (newSlot.amount + oldSlot.amount >= newSlot.item.maximumAmount)
                    {
                        int difference = newSlot.item.maximumAmount - newSlot.amount;
                        newSlot.amount = newSlot.item.maximumAmount;
                        newSlot.itemAmountText.text = newSlot.amount.ToString();

                        oldSlot.amount -= difference;
                        oldSlot.itemAmountText.text = oldSlot.amount.ToString();
                    }
                    else
                    {
                        newSlot.amount += oldSlot.amount;
                        newSlot.itemAmountText.text = newSlot.amount.ToString();
                        NullifySlotData();
                    }
                    return;
                }
            }
        }


        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            if (oldSlot.item.maximumAmount != 1)
            {
                newSlot.itemAmountText.text = oldSlot.amount.ToString();
            }
            else
            {
                newSlot.itemAmountText.text = "";
            }
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmountText.text = "";
        }
        newSlot.isEmpty = oldSlot.isEmpty;

        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(item.icon);
            if (item.maximumAmount != 1)
            {
                oldSlot.itemAmountText.text = amount.ToString();
            }
            else
            {
                oldSlot.itemAmountText.text = "";
            }
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.itemAmountText.text = "";
        }

        oldSlot.isEmpty = isEmpty;
    }
}
