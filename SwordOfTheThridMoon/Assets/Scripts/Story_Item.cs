using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Item : MonoBehaviour , IInteractable
{
    [SerializeField] Camera _mainCamera;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void FixedUpdate()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, 3f))
            {
                if (hit.collider.gameObject.GetComponent<Story_Item>() != null)
                {
                    ContinueData.taked_story_item = true;
                    Destroy(hit.collider.gameObject);
                    Debug.Log("�������� ��������� �������");
                }
            }
        }
    }
    public string GetDescription()
    {
        return "������� [E] ���-�� <color=green>���������</color> ��������� �������.";
    }
    public void Interact()
    {

    }
}
