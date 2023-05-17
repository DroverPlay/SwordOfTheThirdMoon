using System.Collections.Generic;
using UnityEngine;

public class ClothAdder : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab_1;
    [SerializeField] private GameObject mobPrefab_2;
    [SerializeField] private GameObject mobPrefab_3;
    [SerializeField] private GameObject mobPrefab_4;
    [SerializeField] private GameObject mobPrefab_5;
    [SerializeField] private SkinnedMeshRenderer playerSkin;
    [SerializeField] private List<GameObject> _equipedClothes;

    private void Start()
    {
        //Одевание персонажей игры
        if (mobPrefab_1 != null)
        {
            addClothes(mobPrefab_1);
            addClothes(mobPrefab_2);
            addClothes(mobPrefab_3);
            addClothes(mobPrefab_4);
            addClothes(mobPrefab_5);
        }
        _equipedClothes = new List<GameObject>();
    }
    public void addClothes(GameObject clothPrefab)
    {
        GameObject clothObj = Instantiate(clothPrefab, playerSkin.transform.parent);
        SkinnedMeshRenderer[] renderers = clothObj.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.bones = playerSkin.bones;
            renderer.rootBone = playerSkin.rootBone;
        }
        _equipedClothes.Add(clothObj);
    }
    public void RemoveClothes (GameObject searchedClothObject)
    {
        foreach (GameObject clothObj in _equipedClothes)
        {
            if (clothObj.name.Contains(searchedClothObject.name))
            {
                _equipedClothes.Remove(clothObj);
                Destroy(clothObj);
                return;
            }
        }
    }
}
