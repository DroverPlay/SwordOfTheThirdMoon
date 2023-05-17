using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// Скрипт который позволяет использовать инвентарь
    /// </summary>
    public GameObject UIBG;
    public GameObject crosshair;
    public Transform inventoryPanel;
    public Transform quickslotPanel;
    public Transform clothingPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    private Camera mainCamera;
    public CinemachineVirtualCamera CVC;
    public float reachDistance = 3;
    [SerializeField] private Transform player;
    [SerializeField] private List<ClothAdder> _clothAdders = new List<ClothAdder>();
    private bool escOpen;
    private void Awake()
    {
        UIBG.SetActive(true); 
    }
    /// <summary>
    /// Метод который при старте смотрит и считает количество слотов в инвентаре и панели быстрого доступа
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        for (int i = 0; i < quickslotPanel.childCount; i++)
        {
            if (quickslotPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(quickslotPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        UIBG.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
        clothingPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        /// При нажатии кнопки I открывается инвентарь 
        if (Input.GetKeyDown(KeyCode.I))
        {
            escOpen = ContinueData.boolEsc;
            if (!escOpen)
            {
                isOpened = !isOpened;
                if (isOpened)
                {

                    UIBG.SetActive(true);
                    inventoryPanel.gameObject.SetActive(true);
                    clothingPanel.gameObject.SetActive(true);
                    crosshair.SetActive(false);
                    CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
                    CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
                    CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = 0;
                    CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = 0;
                    
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                }
                else
                {
                    UIBG.SetActive(false);
                    inventoryPanel.gameObject.SetActive(false);
                    clothingPanel.gameObject.SetActive(false);
                    crosshair.SetActive(true);
                    CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
                    CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIBG.SetActive(false);
            inventoryPanel.gameObject.SetActive(false);
            clothingPanel.gameObject.SetActive(false);
            crosshair.SetActive(true);
            CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
            CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpened = false;
        }
        /// Луч который позволяет подбирать предметы
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                if (hit.collider.gameObject.GetComponent<Item>() != null)
                {
                    AddItem(hit.collider.gameObject.GetComponent<Item>().item, hit.collider.gameObject.GetComponent<Item>().amount);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    /// <summary>
    /// Метод который добавляет предметы в инвентарь
    /// </summary>
    public void AddItem(ItemScriptableObject _item, int _amount)
    {
        bool allFull = true;
        foreach (InventorySlot inventorySlot in slots)
        {
            if (inventorySlot.isEmpty)
            {
                allFull = false;
                break;
            }
        }
        if (allFull)
        {
            GameObject itemObject = Instantiate(_item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
            itemObject.GetComponent<Item>().amount = _amount;
        }
        int amount = _amount;
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + amount <= _item.maximumAmount)
                {
                    slot.amount += amount;
                    slot.itemAmountText.text = slot.amount.ToString();
                    return;
                }
                else
                {
                    amount -= _item.maximumAmount - slot.amount;
                    slot.amount = _item.maximumAmount;
                    slot.itemAmountText.text = slot.amount.ToString();
                }
                continue;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            if (amount <= 0)
            {
                return;
            }
            if (slot.isEmpty)
            {
                if (amount <= _item.maximumAmount)
                {
                    slot.item = _item;
                    slot.amount = amount;
                    slot.isEmpty = false;
                    slot.SetIcon(_item.icon);
                    if (slot.item.maximumAmount != 1)
                    {
                        slot.itemAmountText.text = slot.amount.ToString();
                    }
                    break;
                }
                else
                {
                    slot.item = _item;
                    slot.amount = _item.maximumAmount;
                    slot.isEmpty = false;
                    slot.SetIcon(_item.icon);
                    if (slot.item.maximumAmount != 1)
                    {
                        slot.itemAmountText.text = slot.amount.ToString();
                    }
                    amount -= _item.maximumAmount;
                }

                foreach (InventorySlot inventorySlot in slots)
                {
                    if (inventorySlot.isEmpty)
                    {
                        allFull = false;
                        break;
                    }
                }
                if (allFull)
                {
                    GameObject itemObject = Instantiate(_item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                    itemObject.GetComponent<Item>().amount = amount;
                    return;
                }
                continue;
            }
        }
    }
    //Система сохранения
    public void RemoveItemFromSlot(int slotID)
    {
        InventorySlot slot = slots[slotID];

        if (slot.clothType != ClothType.None && !slot.isEmpty)
        {
            foreach (ClothAdder clothAdder in _clothAdders)
            {
                clothAdder.RemoveClothes(slot.item.clothingPrefab);
            }
        }
        slot.item = null;
        slot.isEmpty = true;
        slot.amount = 0;
        slot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        slot.iconGO.GetComponent<Image>().sprite = null;
        slot.itemAmountText.text = "";
    }
    public void AddItemToSlot(ItemScriptableObject _item, int _amount, int slotId)
    {
        InventorySlot slot = slots[slotId];
        slot.item = _item;
        slot.isEmpty = false;
        slot.SetIcon(_item.icon);
        
        if (_amount <= _item.maximumAmount)
        {
            slot.amount = _amount;
            if (slot.item.maximumAmount != 1)
            {
                slot.itemAmountText.text = slot.amount.ToString();
            }
        }
        else
        {
            slot.amount = _item.maximumAmount;
            _amount -= _item.maximumAmount;
            if (slot.item.maximumAmount != 1)
            {
                slot.itemAmountText.text = slot.amount.ToString();
            }
        }

        if (slot.clothType != ClothType.None)
        {
            foreach (ClothAdder clothAdder in _clothAdders)
            {
                clothAdder.addClothes(slot.item.clothingPrefab);
            }
        }
    }
}
