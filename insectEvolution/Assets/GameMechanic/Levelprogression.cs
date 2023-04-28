using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using InsectEvolution;
using GameAssets.GameSet.GameDevUtils.Managers;
using System;

public class Levelprogression : MonoBehaviour
{
    [SerializeField] int currentPlanet;
    public float[] decreaseValues;
    public int[] percentValue;
    public static event Action OnCompleteProgression;
    [SerializeField] Image unlockInsectSprite;
    public float[] decSecondLvl;
    public int[] PerSecondLvl;
    public Text percentText;
    public Image progressionImg;
    public Image board;
    [SerializeField] List<GameObject> plantes = new List<GameObject>();
    [SerializeField] List<GameObject> UnlockLizard = new List<GameObject>();
    // [SerializeField] List<Sprite> planteBoard = new List<Sprite>();
    public GameObject planetPopUp;

    private void Awake()
    {
        //currentPlanet = Planet;
        CurrentPlanet(Planet);
        progressionImg.fillAmount = decreaseValues[torchProgress];
        percentText.text = percentValue[torchProgress].ToString() + "%";

    }
    public void UpdateProgressionBar()
    {


        planetPopUp.SetActive(true);
        progressionImg.fillAmount = decreaseValues[torchProgress];
        percentText.text = percentValue[torchProgress].ToString() + "%";
        StartCoroutine(changeValues());


    }
    public IEnumerator changeValues()
    {

        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.shoot, 0.1f);
        float T = 0;
        float tempValue = decreaseValues[torchProgress];
        int tempPercent = percentValue[torchProgress];
        while (T <= 1)
        {
            T += Time.deltaTime * 1f;

            tempValue = Mathf.Lerp(decreaseValues[torchProgress], decreaseValues[torchProgress + 1], T);
            progressionImg.fillAmount = tempValue;
            tempPercent = Mathf.FloorToInt(Mathf.Lerp(percentValue[torchProgress], percentValue[torchProgress + 1], T));
            percentText.text = tempPercent.ToString() + "%";
            yield return null;
        }
        SaveData();
        if (torchProgress == percentValue.Length - 1)
        {
            percentText.gameObject.SetActive(false);
            OnCompleteProgression?.Invoke();
            if (Planet < plantes.Count)
            {
                plantes[Planet].GetComponent<DOTweenAnimation>().DORestartById("Pop");
                SceneShifting();
            }
            else
            {
                plantes[plantes.Count - 1].GetComponent<DOTweenAnimation>().DORestartById("Pop");
                SceneShifting();
            }
        }

    }

    public void SaveData()
    {
        Debug.Log("DataUpdate");
        torchProgress += 1;
    }
    public void ResetData()
    {
        torchProgress = 0;
    }
    /// <summary>
    /// ////////////////////////////////////////
    /// </summary>
    int Planet
    {
        get { return PlayerPrefs.GetInt("Planet1"); }
        set { PlayerPrefs.SetInt("Planet1", value); }
    }
    int torchProgress
    {
        get { return PlayerPrefs.GetInt("torchProgress"); }
        set { PlayerPrefs.SetInt("torchProgress", value); }
    }
    void CurrentPlanet(int id)
    {

        for (int i = 0; i < plantes.Count; i++)
        {
            plantes[i].SetActive(false);
            UnlockLizard[i].SetActive(false);

        }
        if (id < plantes.Count)
        {
            plantes[id].SetActive(true);
            UnlockLizard[id].SetActive(true);


        }
        else
        {

            plantes[plantes.Count - 1].SetActive(true);
            UnlockLizard[UnlockLizard.Count - 1].SetActive(true);
        }

    }
    //public void CheckForLizard()
    //{
    //    Vibration.VibratePop();
    //    SceneShifting();
    //}

    void SceneShifting()
    {
        Planet += 1;
        ResetData();

    }

    private void OnDestroy()
    {
        OnCompleteProgression = null;
    }
}
