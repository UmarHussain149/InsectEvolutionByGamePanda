using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefManager : MonoBehaviour
{
    public static int MergeInsect
    {
        get { return PlayerPrefs.GetInt("MergeLevel"); }
        set { PlayerPrefs.SetInt("MergeLevel", value); }
    }
    public static int Coins
    {
        get { return PlayerPrefs.GetInt("Coin"); }
        set { PlayerPrefs.SetInt("Coin", value); }
    }
    public static int GetLevelNumber(string scenename)
    {
        return PlayerPrefs.GetInt(scenename);
    }
    public static void SetLevelNumber(string scenename)
    {
        var curLevel = GetLevelNumber(scenename);
        curLevel++;
        PlayerPrefs.SetInt(scenename, curLevel);
    }
    public static string GetSceneName()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }
}
