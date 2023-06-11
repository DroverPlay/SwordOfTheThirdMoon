using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DamageFromPerson : MonoBehaviour
{
    [SerializeField] public int damage = 0;
    [SerializeField] private GameObject damageOverlay;
    private void OnCollisionEnter(Collision collision)
    {
        Indicators.DamagePerson(damage);
        flash();
        
    }
    IEnumerable flash()
    {
        damageOverlay.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damageOverlay.SetActive(false);
    }
}
