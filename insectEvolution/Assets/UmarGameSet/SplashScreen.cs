using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public int sceneToLoad;
    public int sceneTounLoad;
    public Image Loading;
    [SerializeField] Text text;
    public void Start()
    {
        Loading.fillAmount = 0;
        StartCoroutine(StartLoad());
    }

    float minTimeToComplete = 2;
    AsyncOperation operation;

    IEnumerator StartLoad()
    {
        float t = 0;




        while (t < minTimeToComplete)
        {
            t += Time.deltaTime;
            if (Loading.fillAmount < 0.85f)
            {
                //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
                //text.text = ((int)(t / minTimeToComplete) * 100).ToString();
                Loading.fillAmount = (t / minTimeToComplete);
            }
            //Debug.Log("Loading");
            yield return null;
        }

        operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        //while (!operation.isDone )
        while (operation.progress < 0.9f)
        {
            //Debug.Log("Waiting=" + operation.progress);
            yield return null;
        }

        Loading.fillAmount = 1;
        yield return new WaitForSeconds(1);
        //Debug.Log("2 sec wait");

        DoneLoading = true;
        //loadingScreen.SetActive(false);
    }

    bool DoneLoading = false;

    private void Update()
    {
        if (DoneLoading)
        {
            operation.allowSceneActivation = true;
            SceneManager.UnloadSceneAsync(sceneTounLoad);
            DoneLoading = false;
        }
    }


    public static int CutSceneCount
    {
        get { return PlayerPrefs.GetInt("CutScene"); }
        set { PlayerPrefs.SetInt("CutScene", value); }
    }
}
