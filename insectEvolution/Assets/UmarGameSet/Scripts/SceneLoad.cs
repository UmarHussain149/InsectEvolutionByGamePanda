using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{

    [SerializeField] string sceneName;
    [SerializeField] GameObject nextbutton;
    [SerializeField] GameObject noThanksbutton;
    [SerializeField] GameObject claimbutton;
    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ReloadScene()
    {


        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = SceneManager.GetActiveScene().name;
        }
        SceneManager.LoadSceneAsync(sceneName);
        if (noThanksbutton) noThanksbutton.GetComponent<Button>().enabled = false;
        if (nextbutton) nextbutton.GetComponent<Button>().enabled = false;
        if (claimbutton) claimbutton.GetComponent<Button>().enabled = false;

    }
    IEnumerator Delay()
    {
        if (Mediation_Manager.instance) Mediation_Manager.instance.ShowRewardedVideo();

        yield return new WaitForSeconds(.3f);
        ReloadScene();
    }
    IEnumerator DelayInIni()
    {
        if (Mediation_Manager.instance) Mediation_Manager.instance.Show_Interstital();

        yield return new WaitForSeconds(.3f);
        ReloadScene();
    }

    public void AdPlusReload()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        StartCoroutine(Delay());
    }
    public void AdIniPlusReload()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        StartCoroutine(DelayInIni());
    }
}