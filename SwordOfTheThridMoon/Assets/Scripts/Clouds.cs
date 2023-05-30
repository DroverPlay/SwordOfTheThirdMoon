using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private Transform _Cloud;

    [SerializeField] private float maxCloudSpeed = 2;
    [SerializeField] private float minCloudSpeed = 1;
    [SerializeField] private float maxCloudHeight = 12;
    [SerializeField] private float minCloudHeight = 6;

    private void FixedUpdate()
    {
        if(_Cloud.transform.position.z > 1000)
        {
            float height = Random.Range(maxCloudHeight, minCloudHeight);
            _Cloud.GetComponent<Transform>().Translate(0, height, -1000);
        }
        else
        {
            _Cloud.GetComponent<Transform>().Translate(0, 0, Mathf.Lerp(maxCloudSpeed, minCloudSpeed, 1f));
        }
    }
}
