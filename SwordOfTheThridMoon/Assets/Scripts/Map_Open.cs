using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Open : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private MenuManager _menuManager;
    private bool isOpened = false;

    void Start()
    {
        _map.SetActive(false);
    }


    void Update()
    {
        if (_inventoryManager.isOpened == false)
        {
            if (_characterController.isOpened == false)
            {
                if (_menuManager.isOpened == false)
                {
                    if(ContinueData.dialogue == false)
                    {
                        if (Input.GetKeyDown(KeyCode.M))
                        {
                            isOpened = !isOpened;
                            if (isOpened == true)
                            {
                                _map.SetActive(true);
                            }
                            else
                            {
                                _map.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}
