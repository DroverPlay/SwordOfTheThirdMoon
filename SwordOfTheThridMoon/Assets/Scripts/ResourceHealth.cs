using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHealth : MonoBehaviour
{
    public int healthResource;
    public int startHealth;
    public Transform resourceSpawner;
    public ItemScriptableObject resourceType;
    public GameObject rockBreakFX;
    public float destroyTime = 5f;

    private void Start()
    {
        healthResource = startHealth;
    }
    public void TreeDown()
    {
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.isKinematic = false;
        rig.useGravity = true;
        rig.mass = 200;
        rig.constraints = RigidbodyConstraints.FreezeRotationY;
        Destroy(gameObject, destroyTime);
    }
    public void StoneGathered()
    {
        Instantiate(rockBreakFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
