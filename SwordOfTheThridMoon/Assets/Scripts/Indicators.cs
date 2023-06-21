using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Indicators : MonoBehaviour
{
    public Image healthBar, manaBar;
    public static float healthAmount = 100;
    public float manaAmount = 60;
    private float uiManaAmount = 60;
    private float uiHealthAmount = 100;
    private static bool gameOver = false;
    DamageFromPerson damage;

    public float secondsToFillMana = 60f;
    private float changeFactor = 5f;
    void Start()
    {
        healthBar.fillAmount = healthAmount / 100;
        manaBar.fillAmount = manaAmount / 100;
    }

    void Update()
    {
        if (manaAmount < 100)
        {
            manaAmount += 100 / secondsToFillMana * Time.deltaTime;
            uiManaAmount = Mathf.Lerp(uiManaAmount, manaAmount, Time.deltaTime * changeFactor);
            manaBar.fillAmount = uiManaAmount / 100;
        }
        if (healthAmount < 100)
        {
            healthAmount += 100 / secondsToFillMana * Time.deltaTime;
            uiHealthAmount = Mathf.Lerp(uiHealthAmount, healthAmount, Time.deltaTime * changeFactor);
            healthBar.fillAmount = uiHealthAmount / 100;
        }
        if (gameOver == true)
        {
            SceneManager.LoadScene("GameScene");
        }

    }
    public static void DamagePerson(int damage)
    {
        healthAmount -= damage;
        if (healthAmount <= 0)
        {
            gameOver = true;
        }
    }

    public void ChangeManaAmount(float changeValue)
    {
        manaAmount += changeValue;
        manaBar.fillAmount = manaAmount / 100;
    }
}
