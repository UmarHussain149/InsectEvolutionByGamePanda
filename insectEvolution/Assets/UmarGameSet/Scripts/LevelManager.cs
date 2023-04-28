using SWS;
using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct LevelInfo
{
    public Transform levelData;
    public PathManager path;
    public splineMove splineMove;
    public Color fogColor;
    public Material skybox;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] string resourcePath;
    [SerializeField] LevelInfo[] levels;
    [HideInInspector] public LevelInfo currentLevel;
    int levelNo;
    public int lvl;
    [SerializeField] bool isTesting;
    [SerializeField] GameObject loadlevel;
    [SerializeField] Transform endPoint;
    public void Awake()
    {
        ActiveLevel();
        GameController.onLevelComplete += OnLevelComplete;
    }

    void OnLevelComplete()
    {

        PlayerPrefManager.SetLevelNumber(PlayerPrefManager.GetSceneName());
    }

    void ActiveLevel()
    {

        levelNo = PlayerPrefManager.GetLevelNumber(PlayerPrefManager.GetSceneName());
        if (isTesting)
        {

            levelNo = lvl;
        }
        if (levelNo > levels.Length - 1)
        {
            levelNo = UnityEngine.Random.Range(3, levels.Length - 1);
        }
        currentLevel = levels[levelNo];
        currentLevel.levelData.gameObject.SetActive(true);
        loadlevel = Instantiate(Resources.Load(resourcePath + levelNo.ToString(), typeof(GameObject))) as GameObject;
        levels[levelNo].path = loadlevel.GetComponentInChildren<PathManager>();
        loadlevel.transform.parent = currentLevel.levelData.transform;
        levels[levelNo].splineMove.pathContainer = levels[levelNo].path;
        if (loadlevel.transform.TryGetComponent<LevelObjectsPlacer>(out LevelObjectsPlacer levelObjectsPlacer))
        {
            if (levelObjectsPlacer.endPointAttachedTarget)
                endPoint.position = levelObjectsPlacer.endPointAttachedTarget.position;
        }
        RenderSettings.fogColor = levels[levelNo].fogColor;
        RenderSettings.skybox = levels[levelNo].skybox;

    }

}
