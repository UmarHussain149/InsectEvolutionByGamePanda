using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelObjectsPlacer : MonoBehaviour
{
    #region Helping Structs------ 
    [System.Serializable]
    private struct SpawnData
    {

        public LevelType levelType;
        public Transform spawnPos;

    }

    #endregion
    [SerializeField] bool spawnObjects = true;
    [Space(5)]
    [Header("Insect Data Sheet....")]
    [SerializeField] InsectDataHolder dataHolder;
    [Space(5)]
    [Header("Fruit Data Sheet....")]
    [SerializeField] InsectDataHolder fruitDataHolder;
    [Space(5)]
    public Transform endPointAttachedTarget;
    [Space(5)]
    [Header("Insect Container....")]
    [SerializeField] Transform Insectcontainer;
    [Space(5)]
    [Header("Insect Data....")]
    [SerializeField] SpawnData[] insectData;
    [Space(5)]
    [Header("Fruit Data....")]
    [SerializeField] SpawnData[] fruitData;


    protected void Awake()
    {
        if (!spawnObjects) { return; }
        SpawnInsectObjects();
        SpawnFrruitObjects();
    }

    private void SpawnInsectObjects()
    {
        for (int i = 0; i < insectData.Length; i++)
        {

            SpawnSingleObject(dataHolder.GetObject(insectData[i].levelType), insectData[i].spawnPos.position, insectData[i].spawnPos.rotation, Insectcontainer);

        }
    }

    private void SpawnFrruitObjects()
    {
        for (int i = 0; i < fruitData.Length; i++)
        {

            SpawnSingleObject(fruitDataHolder.GetObject(fruitData[i].levelType), fruitData[i].spawnPos.position, fruitData[i].spawnPos.rotation, Insectcontainer);

        }
    }

    private Transform SpawnSingleObject(GameObject obj, Vector3 pos, Quaternion rot, Transform container)
    {
        Transform temp = Instantiate(obj).transform;
        temp.position = pos;
        temp.rotation = rot;
        SetParent(temp, container);

        return transform;
    }

    private void SetParent(Transform child, Transform parent)
    {
        if (parent == null)
        {
            child.SetParent(this.transform);
        }
        else
        {
            child.SetParent(parent);
        }
    }


}
