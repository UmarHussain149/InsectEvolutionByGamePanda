using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "THEBADDEST/InsectDataHolder", fileName = "InsectData")]
public class InsectDataHolder : ScriptableObject
{
    [SerializeField] List<Data> insectData = new List<Data>();

    public GameObject GetObject(LevelType levelType)
    {
        foreach (var item in insectData)
        {
            if (item.levelType == levelType)
            {
                return item.insectPrefeb;
            }
        }
        return insectData[0].insectPrefeb;
    }
}

[System.Serializable]
public class Data
{
    public GameObject insectPrefeb;
    public LevelType levelType;
}