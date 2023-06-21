using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Changer : MonoBehaviour
{
    [SerializeField] GameObject manChapter1;
    [SerializeField] GameObject manChapter2;

    private void Start()
    {
        if(ContinueData.level2 == true)
        {
            manChapter1.SetActive(false);
            manChapter2.SetActive(true);
        }
    }
}
