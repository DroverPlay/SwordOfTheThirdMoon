using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_XYZ_Teleport : MonoBehaviour
{
    private Vector3 position = new Vector3();
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (ContinueData.startGame == true && ContinueData.level2 == false)
        {
            Vector3 position = new Vector3(-13, 0, 29);
            _player.gameObject.transform.position = position;
        }
        else if (ContinueData.level2 == true && ContinueData.level3 == false)
        {
            Vector3 position = new Vector3(4, 0, -34);
            _player.gameObject.transform.position = position;
        }
        else if (ContinueData.level3 == true && ContinueData.level4 == false)
        {
            Vector3 position = new Vector3(8, 0, -34);
            _player.gameObject.transform.position = position;
        }
        else if (ContinueData.level4 == true)
        {
            Vector3 position = new Vector3(11, 0, -30);
            _player.gameObject.transform.position = position;
        }
    }

}
