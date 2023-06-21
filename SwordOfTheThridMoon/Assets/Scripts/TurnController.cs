using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private MenuManager _menuManager;
    private Animator _animator;
    private Dialog _dialog;
    private float slowMouseX;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //Отключение поворотов во время использования инвентаря
        if (_inventoryManager.isOpened == true)
        {
            slowMouseX = 0;
        }
        else if (_characterController.isOpened == true)
        {
            slowMouseX = 0;
        }
        else if (_menuManager.isOpened == true)
        {
            slowMouseX = 0;
        }
        else if (ContinueData.dialogue == true)
        {
            slowMouseX = 0;
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");
            slowMouseX = Mathf.Lerp(slowMouseX, mouseX, 10 * Time.deltaTime);
        }
        _animator.SetFloat("MouseX", slowMouseX);
    }
}
