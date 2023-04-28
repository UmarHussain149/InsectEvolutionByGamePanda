using AxisGames.Prefs;
using DG.Tweening;
using MergeSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    int tempCount;
    [SerializeField] RectTransform tapToPlay;
    [SerializeField] GameObject textBox;
    [SerializeField] RectTransform characterUpgradePos;
    [SerializeField] Tutorial TutorialState;
    [SerializeField] GameObject wholePanle;
    [SerializeField] GameObject speedUpgradePanle;
    [SerializeField] GameObject characterUpgradePanle;
    [SerializeField] RectTransform tutorialPviot;
    public RectTransform canvasRect;
    public event Action<bool> OnTutorial;
    [SerializeField] Text messageText;
    [SerializeField] List<String> messages = new List<string>();
    [SerializeField] int idOfCurrentTutorial;
    [SerializeField] GameObject button;
    bool canPlay;
    private void Start()
    {
        idOfCurrentTutorial = Id;
        if (ReferenceManager.instance.uIManager.levelNo == 1)
        {

            idOfCurrentTutorial = 0;
        }

        if (ReferenceManager.instance.uIManager.levelNo == 2)
        {
            idOfCurrentTutorial = 4;
        }

        if (idOfCurrentTutorial == 3)
        {
            ExtendedPrefs.SetBool("Tutorial", true);
            idOfCurrentTutorial++;
            tapToPlay.gameObject.SetActive(false);
            TutorialState = (Tutorial)idOfCurrentTutorial;
            if (idOfCurrentTutorial == (int)TutorialState)
            {
                PlayTutorial(TutorialState, true);
            }
        }


        if (!ExtendedPrefs.GetBool("Tutorial"))
        {
            speedUpgradePanle.SetActive(false);
            characterUpgradePanle.SetActive(false);
        }
        else
        {
            speedUpgradePanle.SetActive(true);
            characterUpgradePanle.SetActive(true);
        }
    }
    void PlayTutorial(Tutorial tutorial, bool active)
    {
        canPlay = true;

        if (ReferenceManager.instance.uIManager.levelNo <= 2)
        {
            TutorialState = tutorial;
            switch (tutorial)
            {
                case Tutorial.OnSameLevel:
                    Activate(active);
                    return;
                case Tutorial.OnGreaterLevel:
                    Activate(active);
                    return;
                case Tutorial.OnFruitEat:
                    Activate(active);
                    return;
                case Tutorial.OnUpgradeCharacter:
                    tutorialPviot.localPosition = characterUpgradePos.localPosition;
                    OnTutorial?.Invoke(true);

                    Activate(active);
                    return;
                case Tutorial.OnUpgradeSpeed:
                    return;
                case Tutorial.None:
                    return;
            }
        }
    }
    public void SetScreenPos(Transform pos)
    {

        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(pos.position);
        Vector2 screenPosition = new Vector2(((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)), ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
        tutorialPviot.localPosition = screenPosition;
        tutorialPviot.localRotation = new Quaternion(0, 0, 0, 0);

    }
    void Activate(bool Active)
    {

        //ExtendedPrefs.SetBool("Tutorial", true);
        if (ReferenceManager.instance.characterController.currentInsectAnimator) ReferenceManager.instance.characterController.currentInsectAnimator.SetBool("Idle", true);
        messageText.text = null;
        messageText.gameObject.SetActive(Active);
        wholePanle.SetActive(Active);
        if (idOfCurrentTutorial >= 3)
        {
            tempCount = idOfCurrentTutorial;
            tempCount -= 1;
        }
        else
        {
            tempCount = idOfCurrentTutorial;
        }
        FunctionTimer.Create(() => { textBox.SetActive(true); }, 0.8f);
        messageText.DOText(messages[tempCount], 3).SetDelay(1).OnComplete(() => { button.SetActive(true); });
    }
    public void Restart()
    {
        if (canPlay)
        {
            canPlay = false;
            textBox.SetActive(false);
            messageText.gameObject.SetActive(false);
            wholePanle.SetActive(false);
            OnTutorial?.Invoke(false);
            ReferenceManager.instance.characterController.currentInsectAnimator.SetBool("Idle", false);
            ReferenceManager.instance.characterController.currentInsectAnimator.SetTrigger("Walk");
            idOfCurrentTutorial++;
            GameController.changeGameState(GameState.Gameplay);
            //TutorialState = Tutorial.None;
            Id = idOfCurrentTutorial;

            button.transform.DOScale(0, 0.5f).SetEase(Ease.OutBack).OnComplete(() => { button.SetActive(false); });
        }
    }
    IEnumerator DelayInReturnOnGamePlay()
    {
        yield return new WaitForSeconds(.1f);
        button.transform.DOScale(0, 0.5f).SetEase(Ease.OutBack);
        messageText.gameObject.SetActive(false);
        wholePanle.SetActive(false);
        OnTutorial?.Invoke(false);
        ReferenceManager.instance.characterController.currentInsectAnimator.SetBool("Idle", false);
        ReferenceManager.instance.characterController.currentInsectAnimator.SetTrigger("Walk");
        idOfCurrentTutorial++;
        GameController.changeGameState(GameState.Gameplay);
        //TutorialState = Tutorial.None;
        Id = idOfCurrentTutorial;
        button.SetActive(false);

    }
    public void InvokTutorial(bool fire, Tutorial tutorial, Transform InsectPos)
    {
        OnTutorial?.Invoke(fire);
        PlayTutorial(tutorial, fire);
        SetScreenPos(InsectPos);
    }
    private void OnDestroy()
    {
        OnTutorial = null;
    }
    int Id
    {
        get { return PlayerPrefs.GetInt("Id"); }
        set { PlayerPrefs.SetInt("Id", value); }
    }
}
[System.Serializable]
public class TutorialData
{
    public string tutorialInformation;


}