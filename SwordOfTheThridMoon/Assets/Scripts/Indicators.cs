using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    public Image healthBar, manaBar;
    public float healthAmount = 100;
    public float manaAmount = 60;
    private float uiManaAmount = 60;
    private float uiHealthAmount = 100;
    //DamageFromPerson damage;

    public float secondsToFillMana = 60f;
    private float changeFactor = 5f;
    void Start()
    {
        healthBar.fillAmount = healthAmount / 100;
        manaBar.fillAmount = manaAmount / 100;
    }

    void Update()
    {
        if (manaAmount > 0)
        {
            manaAmount += 100 / secondsToFillMana * Time.deltaTime;
            uiManaAmount = Mathf.Lerp(uiManaAmount, manaAmount, Time.deltaTime * changeFactor);
            manaBar.fillAmount = uiManaAmount / 100;
            
        }
        if (healthAmount > 0)
        {
            healthAmount += 100 / secondsToFillMana * Time.deltaTime;
            uiHealthAmount = Mathf.Lerp(uiHealthAmount, healthAmount, Time.deltaTime * changeFactor);
            healthBar.fillAmount = uiHealthAmount / 100;

        }

        //Урон по персонажу
        //if (damage.hit == true)
        //{
        //    if (damage.count >= 1)
        //    {
        //        healthAmount -= damage.count;
        //        healthBar.fillAmount = healthAmount / 100;
        //    }
        //}
    }
    public void DamagePerson(int damage)
    {
        if(damage >= 1)
        {
            healthAmount -= damage;
            healthBar.fillAmount = healthAmount / 100;
        }
    }
    public void ChangeManaAmount(float changeValue)
    {
        manaAmount += changeValue;
        manaBar.fillAmount = manaAmount / 100;
    }
}
