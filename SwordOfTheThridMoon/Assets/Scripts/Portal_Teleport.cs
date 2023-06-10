using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Teleport : MonoBehaviour
{
    [SerializeField] private string _scene;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(_scene);
        Debug.Log("Внутри");
    }
}
