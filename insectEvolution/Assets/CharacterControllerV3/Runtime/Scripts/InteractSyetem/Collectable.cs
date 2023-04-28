using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine.UI;

namespace THEBADDEST.InteractSyetem
{


    public class Collectable : TriggerEffector
    {
        [SerializeField] SkinnedMeshRenderer meshRenderer;
        public CollectionType collectionType;
        public LevelType level;
        [SerializeField] Text levelText;
        [SerializeField] int levelCount;
        [SerializeField] GameObject x1Point;
        [SerializeField] GameObject x2Point;
        [SerializeField] ParticleSystem Conff;
        [SerializeField] GameObject spider;
        [SerializeField] bool isLizardScene;
        THEBADDEST.CharacterController3.CharacterController characterController;
        private void Start()
        {
            if (levelCount <= 9)
            {

                if (levelText) levelText.text = "0" + levelCount.ToString();
                if (isLizardScene)
                {

                    levelCount = 1;
                }
            }
            else
            {
                if (levelText) levelText.text = levelCount.ToString();
                if (isLizardScene)
                {

                    levelCount = 1;
                }

            }

        }
        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            Vibration.VibrateNope();
            triggered = true;
            base.OnEffect(other, container);
            if (other.CompareTag("Legs"))
            {

                Destroy(this.GetComponent<Collider>());
                //if (meshRenderer) meshRenderer.material = ReferenceManager.instance.characterController.greyInsect;
                transform.parent = other.transform;
                transform.localPosition = Vector3.zero;
                //other.GetComponent<Collider>().enabled = false;
                this.transform.DOScale(0f, 1f).OnComplete(() =>
                {
                    //var particle = Instantiate(ReferanceManager.instance.characterController.venomBreakingParticle, other.transform.position, Quaternion.identity);
                    //particle.transform.parent = other.transform;
                    //particle.transform.localPosition = Vector3.zero;
                    //particle.transform.localScale = new Vector3(.15f, .15f, .15f);
                    //Destroy(particle, .3f);
                    //antData.SetActive(false);
                    //other.GetComponent<Collider>().enabled = true;
                    ReferenceManager.instance.characterController.lizardCollection.Play();
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 1);
                    Destroy(this.gameObject);
                });
                //ReferanceManager.instance.characterController.UpdateLegs();
                if (collectionType == CollectionType.onEndPoint)
                {
                    CoinsManager.Instance.AddCoins(WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                    CoinsManager.Instance.AddCoins(1);
                    x1Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                    x2Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                    Conff.Play();
                }
                Debug.Log("Legs");
            }
            if (other.CompareTag("Player"))
            {

                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();

                if (collectionType == CollectionType.onGround)
                {
                    GroundAction();
                }
                else
                {
                    EndPointAction();
                }
            }
        }
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.4f);
            characterController.currentInsect.SetActive(false);



        }
        public Vector2 WordPointToCanvasPoint(Camera camera, Vector3 worldPoint, RectTransform canvasRect)
        {
            Vector2 viewportPosition = camera.WorldToViewportPoint(worldPoint);
            Vector2 screenPosition = new Vector2(((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)), ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
            return screenPosition;
        }
        void EndPointAction()
        {
            if (levelCount <= characterController.currentLevelCount)
            {
                CoinsManager.Instance.AddCoins(WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                CoinsManager.Instance.AddCoins(1);
                characterController.collectionParticle.Play();
                if (spider) Instantiate(this.spider, this.transform.position, this.spider.transform.rotation);
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 1);
                transform.DOLocalJump(new Vector3(UnityEngine.Random.Range(3, -3), UnityEngine.Random.Range(6, 11), UnityEngine.Random.Range(-2, -5)), 4, 1, .7f).OnComplete(() =>
                {

                    gameObject.SetActive(false);
                });
                characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                //characterController.currentLevelCount++;
                //characterController.upGradeText.text = characterController.currentLevelCount.ToString();
                x1Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                x2Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                Conff.Play();
            }
            else
            {
                if (!isLizardScene)
                {

                    x1Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                    x2Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                    characterController.hitOnHigerlevelParticle.Play();
                    GameController.changeGameState(GameState.Complete);
                    StartCoroutine(Delay());
                    characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Hit");
                }
            }
        }
        void GroundAction()
        {
            if (levelCount <= characterController.currentLevelCount)
            {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 1);
                characterController.collectionParticle.Play();
                if (!isLizardScene)
                {
                    characterController.CurrentPorgression(level, 0.25f);
                }
                gameObject.SetActive(false);
                characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                if (spider) Instantiate(this.spider, this.transform.position, this.spider.transform.rotation);
            }
            else
            {
                if (!isLizardScene)
                {
                    characterController.CurrentPorgression(level, -0.25f);
                    characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Hit");
                }
            }
        }
    }
    public enum CollectionType
    {
        onGround,
        onEndPoint
    }

}