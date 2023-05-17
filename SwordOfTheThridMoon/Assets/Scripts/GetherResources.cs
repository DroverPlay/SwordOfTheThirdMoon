using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetherResources : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask layerMask;
    public InventoryManager inventoryManager;
    public ItemScriptableObject resource;
    public int resourceAmount;
    public GameObject hitFX;
    ResourceHealth treeHealth;
    public void CreateResource()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit, 1.5f, layerMask))
        {
            if (resource.name == hit.collider.GetComponent<ResourceHealth>().resourceType.name)
            {
                if (hit.collider.GetComponent<ResourceHealth>().healthResource >= 1)
                {
                    Instantiate(hitFX, hit.point, Quaternion.LookRotation(hit.normal));
                    inventoryManager.AddItem(resource, resourceAmount);
                    hit.collider.GetComponent<ResourceHealth>().healthResource--;
                    if (hit.collider.GetComponent<ResourceHealth>().healthResource <= 0 && hit.collider.gameObject.layer == 6)
                    {
                        hit.collider.GetComponent<ResourceHealth>().TreeDown();
                        hit.collider.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * 10, ForceMode.Impulse);
                    }
                    if (hit.collider.GetComponent<ResourceHealth>().healthResource <= 0 && hit.collider.gameObject.layer == 7)
                    {
                        hit.collider.GetComponent<ResourceHealth>().StoneGathered();
                    }
                }
            }

        }
    }
}
