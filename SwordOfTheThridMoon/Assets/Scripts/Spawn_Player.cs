using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Player : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawner;

    private void Start()
    {
        player.GetComponent<Transform>().position = spawner.GetComponent<Transform>().position;
    }
}
