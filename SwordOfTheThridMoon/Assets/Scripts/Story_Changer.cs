using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Changer : MonoBehaviour
{
    [SerializeField] GameObject manChapter1;
    [SerializeField] GameObject manChapter2;
    [SerializeField] GameObject pickaxeChapter2;
    [SerializeField] GameObject manChapter3;
    [SerializeField] GameObject panChapter3;
    [SerializeField] GameObject manChapter4;
    [SerializeField] GameObject hummerChapter4;
    [SerializeField] GameObject swordChapter4;

    private void Start()
    {
        if(ContinueData.level2 == true && ContinueData.level3 == false)
        {
            manChapter1.SetActive(false);
            manChapter2.SetActive(true);
            pickaxeChapter2.SetActive(true);
        }
        else if (ContinueData.level3 == true && ContinueData.level4 == false)
        {
            manChapter2.SetActive(false);
            manChapter3.SetActive(true);
            panChapter3.SetActive(true);
        }
        else if (ContinueData.level4 == true)
        {
            manChapter3.SetActive(false);
            manChapter4.SetActive(true);
            hummerChapter4.SetActive(true);
            swordChapter4.SetActive(true);
        }
    }
}
