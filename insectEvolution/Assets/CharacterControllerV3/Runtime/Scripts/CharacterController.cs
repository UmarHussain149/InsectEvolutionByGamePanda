using GameDevUtils.HealthSystem;
using THEBADDEST;
using THEBADDEST.InteractSyetem;
using UnityEngine;
using GameDevUtils;
using SWS;
using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;
using MergeSystem;

namespace THEBADDEST.CharacterController3
{


    public class CharacterController : GameDevBehaviour, IEffectContainer, IDamageable
    {
        [SerializeField] bool isTesting;
        [SerializeField] int testLevelCount;
        public ParticleSystem lizardCollection;
        [SerializeField] Transform spownPos;
        public ParticleSystem win;
        [SerializeField] DOTweenAnimation barSprite;
        public Material greyInsect;
        public GameObject splashPrefeb;
        public Rigidbody m_Rigidbody;
        public GameObject camera;
        public GameObject hand;
        public GameObject tap;
        bool inGamePlay;
        public GameObject currentInsect;
        public Animator currentInsectAnimator;
        public GameObject prefebShoot;
        public GameObject canvasPlayer;
        public GameObject progressionbar;
        public ParticleSystem collectionParticle;
        public ParticleSystem deathParticle;
        public ParticleSystem skullPickupParticle;
        public ParticleSystem hitOnHigerlevelParticle;
        public ParticleSystem upGradelevelParticle;
        public List<InsectsData> insects = new List<InsectsData>();
        public LevelType currentLevel;
        public Text upGradeText;
        public int currentLevelCount;
        [SerializeField] public CharacterBehaviour[] behaviours;
        [SerializeField] HealthSystem healthSystem;
        IEffectContainer effectContainer;
        public splineMove splineMove;
        public UIManager uIManager;
        [SerializeField] Image reviveFiller;
        public bool IsDestroyed { get; set; }
        public int currentBehaviour = 0;
        float progressionValue;
        public Animator cinemachine;
        public int totallCollection;
        float spineSpeed;
        [SerializeField] GameObject larwaPrefeb;
        public static event Action<bool> onFight;
        public int totalLarwa;
        public int eatLarwa;
        void Awake()
        {
            InitializeAllBehaviors();
            effectContainer = new EffectContainer();
            GameController.onGameplay += GameStart;
            GameController.onLevelFail += GameStop;
            GameController.OnRevive += GameStop;
            if (ReferenceManager.instance.tutorialManager) ReferenceManager.instance.tutorialManager.OnTutorial += GamepauseOnTutorial;
            GameController.onLevelComplete += GameStop;
            if (isTesting)
            {
                currentLevelCount = testLevelCount;
            }
            else
            {


                if (PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) == 0)
                {
                    PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), currentLevelCount);
                    currentLevelCount = PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName());
                }
                else
                {
                    currentLevelCount = PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName());
                }
            }

        }

        protected override void Start()
        {
            base.Start();
            RunBehaviour(0);
            currentLevel = (LevelType)currentLevelCount;
            UpgardeLevelOfCharacter(currentLevel);
        }

        private void InitializeAllBehaviors()
        {
            foreach (CharacterBehaviour behaviour in behaviours)
            {
                behaviour.Init(this);
            }
        }

        public void StopAllBehaviours()
        {
            foreach (CharacterBehaviour behaviour in behaviours)
            {
                behaviour.CanControl = false;
            }
        }

        public void RunBehaviour(int behaviorIndex)
        {
            behaviours[currentBehaviour].CanControl = false;
            currentBehaviour = behaviorIndex;
            behaviours[behaviorIndex].CanControl = false;
        }

        public void RunAdditiveBehaviour(int behaviorIndex)
        {
            behaviours[behaviorIndex].CanControl = true;
        }

        public void AddEffect(string id, IEffect effect)
        {
            effectContainer.AddEffect(id, effect);
        }

        public void EmitEffect(IEffector effect)
        {
            effectContainer.EmitEffect(effect);
        }

        public void EmitEffect(string effectId)
        {
            effectContainer.EmitEffect(effectId);
        }


        public void Damage(float damageAmount, Vector3 hitPoint)
        {
            healthSystem.TakeDamage(damageAmount, hitPoint);
        }

        public void DestroyObject()
        {
            healthSystem.Death();
        }

        void GameStart()
        {
            behaviours[currentBehaviour].CanControl = true;
            splineMove.speed = PlayerPrefs.GetFloat("Speed" + PlayerPrefManager.GetSceneName());
            splineMove.StartMove();
            currentInsectAnimator.SetTrigger("Walk");
            inGamePlay = true;

        }
        void GameStop()
        {
            canvasPlayer.SetActive(false);
            behaviours[currentBehaviour].CanControl = false;
            splineMove.Pause();
            //m_Rigidbody.isKinematic = true;
        }
        void GamepauseOnTutorial(bool canStop)
        {

            canvasPlayer.SetActive(!canStop);
            behaviours[currentBehaviour].CanControl = !canStop;
            if (canStop)
            {
                spineSpeed = splineMove.speed;
                splineMove.speed = 0;
                splineMove.Stop();
            }
            else
            {
                //inGamePlay = true;
                //splineMove.speed = PlayerPrefs.GetFloat("Speed" + PlayerPrefManager.GetSceneName());
                //splineMove.StartMove();
                //behaviours[currentBehaviour].CanControl = true;
                //Debug.Log(spineSpeed);
            }
        }
        public void CurrentPorgression(LevelType levelType, float value)
        {

            //switch (levelType)
            //{
            //    case LevelType.Level1:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level2:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level3:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level4:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level5:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level6:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level7:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level8:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level9:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level10:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level11:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level12:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level13:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level14:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level15:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level16:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level17:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level18:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level19:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level20:
            //        UpdateBar(value);
            //        break;
            //    case LevelType.Level21:
            //        UpdateBar(value);
            //        break;

            //}
            UpdateBar(value);
        }
        public void UpdateBar(float value)
        {

            var temp = uIManager.progressionBar.fillAmount;
            temp += value;
            uIManager.progressionBar.DOFillAmount(temp, .1f).OnComplete(() =>
            {
                OnCompleteCheckProgression();


            });

            if (barSprite) barSprite.DORestartById("Rotate");

        }
        void OnCompleteCheckProgression()
        {
            if (uIManager.progressionBar.fillAmount >= 1)
            {

                currentLevelCount++;

                currentLevel = (LevelType)currentLevelCount;
                UpgardeLevelOfCharacter(currentLevel);
                uIManager.progressionBar.fillAmount = 0;

            }
            else if (uIManager.progressionBar.fillAmount <= 0 && currentLevelCount > 0)
            {

                currentLevelCount--;
                if (currentLevelCount != 0)
                {

                    currentLevel = (LevelType)currentLevelCount;
                    UpgardeLevelOfCharacter(currentLevel);
                    if (currentLevelCount != 0)
                    {
                        uIManager.progressionBar.fillAmount = .99f;
                    }
                    else
                    {
                        uIManager.progressionBar.fillAmount = 0;

                    }
                }
                else
                {
                    deathParticle.Play();
                    hitOnHigerlevelParticle.Play();
                    GameController.changeGameState(GameState.Fail);
                    currentInsect.SetActive(false);
                }

            }
            else if (uIManager.progressionBar.fillAmount <= 0 && currentLevelCount == 0)
            {
                deathParticle.Play();
                hitOnHigerlevelParticle.Play();
                GameController.changeGameState(GameState.Fail);
                currentInsect.SetActive(false);


            }

        }
        public void UpgardeLevelOfCharacter(LevelType levelType)
        {
            //switch (levelType)
            //{
            //    case LevelType.Level1:
            //        currentLevel = LevelType.Level1;
            //        ChangeCharacter();
            //        //partical
            //        //Shape
            //        //Animations
            //        break;
            //    case LevelType.Level2:
            //        currentLevel = LevelType.Level2;
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level3:
            //        currentLevel = LevelType.Level3;
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level4:
            //        currentLevel = LevelType.Level4;
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level5:
            //        currentLevel = LevelType.Level5;
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level6:
            //        currentLevel = LevelType.Level6;
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level7:
            //        currentLevel = LevelType.Level7;
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level8:
            //        currentLevel = LevelType.Level8;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level9:
            //        currentLevel = LevelType.Level9;
            //        //cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level10:
            //        currentLevel = LevelType.Level10;
            //        //cinemachine.SetTrigger("Up");
            //        UpdatePlayerCanvas(1f);
            //        ChangeCharacter();
            //        break;
            //    case LevelType.Level11:
            //        currentLevel = LevelType.Level11;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.1f);
            //        break;
            //    case LevelType.Level12:
            //        currentLevel = LevelType.Level12;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.1f);
            //        break;
            //    case LevelType.Level13:
            //        currentLevel = LevelType.Level13;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.15f);
            //        break;
            //    case LevelType.Level14:
            //        currentLevel = LevelType.Level14;
            //        //  cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.2f);
            //        break;
            //    case LevelType.Level15:
            //        currentLevel = LevelType.Level15;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.2f);
            //        break;
            //    case LevelType.Level16:
            //        currentLevel = LevelType.Level16;
            //        //  cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.2f);
            //        break;
            //    case LevelType.Level17:
            //        currentLevel = LevelType.Level17;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.5f);
            //        break;
            //    case LevelType.Level18:
            //        currentLevel = LevelType.Level18;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.1f);
            //        break;
            //    case LevelType.Level19:
            //        currentLevel = LevelType.Level19;
            //        //  cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.6f);
            //        break;
            //    case LevelType.Level20:
            //        currentLevel = LevelType.Level20;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.6f);
            //        break;
            //    case LevelType.Level21:
            //        currentLevel = LevelType.Level21;
            //        // cinemachine.SetTrigger("Up");
            //        ChangeCharacter();
            //        UpdatePlayerCanvas(1.5f);
            //        break;

            //}
            currentLevel = levelType;
            ChangeCharacter(levelType);
            if (currentLevelCount <= 9)
            {

                upGradeText.text = "0" + currentLevelCount.ToString();
            }
            else
            {
                upGradeText.text = currentLevelCount.ToString();
            }
        }
        void UpdatePlayerCanvas(float val)
        {
            var canvas = canvasPlayer.transform.localPosition;
            canvas.y = val;
            canvasPlayer.transform.localPosition = canvas;
        }
        void ChangeCharacter(LevelType level)
        {
            //upGradelevelParticle.Play();
            if (level <= LevelType.Level35)
                for (int i = 0; i < insects.Count; i++)
                {
                    if (insects[i].level == level)
                    {
                        insects[i].insect.SetActive(true);
                        //insects[i].transformationParticle.Play();
                        currentInsect = insects[i].insect;
                        currentInsectAnimator = insects[i].animator;
                        if (inGamePlay)
                        {
                            insects[i].animator.SetTrigger("Walk");
                        }
                        if (this.transform.GetChild(0).TryGetComponent<DOTweenAnimation>(out DOTweenAnimation animation))
                        {
                            animation.DORestartById("Rotate");
                        }
                        UpdatePlayerCanvas(insects[i].canvusOffset);
                    }
                    else
                    {
                        insects[i].insect.SetActive(false);
                    }

                }
        }
        public void TestCharacter()
        {

            currentLevelCount++;
            PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), currentLevelCount);
            ChangeCharacter((LevelType)currentLevelCount);
        }
        public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius, Transform target)
        {
            var required = new Vector3(0, -23, 0);
            this.transform.GetChild(0).transform.DORotate(required, 1);
            this.transform.GetChild(0).transform.DOScale(0.5f, 0.5f).SetRelative();
            StartCoroutine(Delay(num, point, radius, target));

        }
        IEnumerator Delay(int num, Vector3 point, float radius, Transform target)
        {
            for (int i = 0; i < num; i++)
            {
                yield return new WaitForSeconds(0.05f);
                /* Distance around the circle */
                var radians = 2 * Mathf.PI / num * i;

                /* Get the vector direction */
                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var spawnDir = new Vector3(horizontal, 0, vertical);

                /* Get the spawn position */
                var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

                /* Now spawn */
                var enemy = Instantiate(larwaPrefeb, this.transform.position, Quaternion.identity) as GameObject;

                /* Rotate the enemy to face towards player */
                enemy.transform.LookAt(point);
                enemy.transform.localScale = Vector3.zero;
                enemy.transform.position = spawnPos;
                /* Adjust height */
                //enemy.transform.Translate(new Vector3(0, 0.8f, 0));
                //enemy.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                //enemy.transform.DOJump(spawnPos, 4, 1, .3f);
                //var errorValue = new Vector3(spawnPos.x + UnityEngine.Random.Range(4f, 6f), spawnPos.y, spawnPos.z + UnityEngine.Random.Range(6f, 9f));
                // enemy.transform.DOMove(spawnPos, .5f).SetEase(Ease.OutBack).OnComplete(() => { enemy.transform.DOJump(this.transform.position, 7, 1, .3f); });
                enemy.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce).OnComplete(() =>
                {


                    enemy.AddComponent<Rigidbody>();
                    enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    enemy.GetComponent<Lawra>().canMove = true;
                    enemy.GetComponent<Lawra>().target = target;
                    // Constraints Frezz kro

                });

            }
            FunctionTimer.Create(() => { onFight?.Invoke(true); }, 0.5f);

        }
        public void GenrateLarwa(int amount, Transform target)
        {
            totalLarwa = amount;
            CreateEnemiesAroundPoint(amount, this.transform.position, 5f, target);
        }

        public void Revive()
        {

            StartCoroutine(DelayInRevive());
        }
        IEnumerator DelayInRevive()
        {
            //CoinsManager.Instance.groundUnlock = true;
            if (Mediation_Manager.instance) Mediation_Manager.instance.ShowRewardedVideo();

            yield return new WaitForSeconds(0.5f);
            GameController.changeGameState(GameState.Revive);

            this.transform.localPosition = new(-.6f, transform.localPosition.y, transform.localPosition.z + 5);
            currentInsect.SetActive(true);
            currentInsect.transform.DOScale(0, 0.01f);
            currentInsect.transform.DOScale(1, 1f).SetEase(Ease.OutBounce).SetRelative();
            //revive.Play();
            reviveFiller.transform.parent.gameObject.SetActive(true);
            FunctionTimer.Create(() =>
            {
                //splineMove.speed = 8;
                splineMove.Resume();
                //splineMove.StartMove();
                behaviours[0].CanControl = true;
                currentInsectAnimator.SetTrigger("Walk");
                reviveFiller.transform.parent.gameObject.SetActive(false);
                FunctionTimer.Create(() =>
                {
                    this.GetComponent<Collider>().enabled = true;
                    m_Rigidbody.isKinematic = false;
                    //GameStart();
                }, 0.2f);
            }, reviveFiller, 2, antiClock: true);

            //CoinsManager.Instance.groundUnlock = false;
        }


        private void OnDestroy()
        {
            onFight = null;
        }
        [Serializable]
        public class InsectsData
        {
            [SerializeField] public GameObject insect;
            [SerializeField] public LevelType level;
            [SerializeField] public ParticleSystem transformationParticle;
            [SerializeField] public Animator animator;
            public float canvusOffset;
        }

    }
}
public enum LevelType
{
    Level1 = 1,
    Level2 = 2,
    Level3 = 3,
    Level4 = 4,
    Level5 = 5,
    Level6 = 6,
    Level7 = 7,
    Level8 = 8,
    Level9 = 9,
    Level10 = 10,
    Level11 = 11,
    Level12 = 12,
    Level13 = 13,
    Level14 = 14,
    Level15 = 15,
    Level16 = 16,
    Level17 = 17,
    Level18 = 18,
    Level19 = 19,
    Level20 = 20,
    Level21 = 21,
    Level22 = 22,
    Level23 = 23,
    Level24 = 24,
    Level25 = 25,
    Level26 = 26,
    Level27 = 27,
    Level28 = 28,
    Level29 = 29,
    Level30 = 30,
    Level31 = 31,
    Level32 = 32,
    Level33 = 33,
    Level34 = 34,
    Level35 = 35,
}