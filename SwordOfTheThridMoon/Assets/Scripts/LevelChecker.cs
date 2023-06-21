using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    [SerializeField] GameObject portalLevel2;
    [SerializeField] GameObject portalLevel3;
    [SerializeField] public bool level2;
    [SerializeField] public bool level3;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level2"))
        {
            int level2 = PlayerPrefs.GetInt("Level2");
        }
        else if (PlayerPrefs.HasKey("Level3"))
        {
            int level3 = PlayerPrefs.GetInt("Level3");
        }
    }
    private void Start()
    {
        level2 = ContinueData.level2;
        level3 = ContinueData.level3;
        Debug.Log("Второй левел равен " + level2);
        Debug.Log("Третий левел равен " + level3);
        if (level2)
        {
            portalLevel2.SetActive(true);
        }
        else if (level3)
        {
            portalLevel3.SetActive(true);
        }
        else if (level2 && level3)
        {
            portalLevel2.SetActive(true);
            portalLevel3.SetActive(true);
        }
    }
}
