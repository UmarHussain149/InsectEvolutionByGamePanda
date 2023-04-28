using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using InsectEvolution;
using GameAssets.GameSet.GameDevUtils.Managers;

namespace THEBADDEST.InteractSyetem
{
    public class Lawra : MonoBehaviour
    {
        public bool canMove;
        bool canfight;
        public Transform target;
        [SerializeField] SkinnedMeshRenderer meshRenderer;
        [SerializeField] Material black;
        [SerializeField] GameObject antData;
        [SerializeField] ParticleSystem Death;
        bool isLarwa;
        private void Awake()
        {
            THEBADDEST.CharacterController3.CharacterController.onFight += StartFight;
        }
        private void Start()
        {
            Invoke("ChangeBehaviour", 0.1f);

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Legs") && canfight)
            {

                SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 0.7f);
                //CoinsManager.Instance.AddCoins(1);

                EatEffect(other.transform);


                if (ReferenceManager.instance.characterController.eatLarwa == ReferenceManager.instance.characterController.totalLarwa) PlayerEatEffect(other.transform);


            }
        }
        void EatEffect(Transform leg)
        {
            ReferenceManager.instance.characterController.eatLarwa++;
            canfight = false;
            Destroy(this.GetComponent<Collider>());
            if (meshRenderer) meshRenderer.material = black;
            transform.parent = leg.transform;
            transform.localPosition = Vector3.zero;

            leg.GetComponent<Collider>().enabled = false;
            this.transform.DOScale(0.06f, 0.5f).OnComplete(() =>
            {
                //var particle = Instantiate(ReferanceManager.instance.characterController.venomBreakingParticle, other.transform.position, Quaternion.identity);
                //particle.transform.parent = other.transform;
                //particle.transform.localPosition = Vector3.zero;
                //particle.transform.localScale = new Vector3(.15f, .15f, .15f);
                //Destroy(particle, .3f);
                antData.SetActive(false);
                leg.GetComponent<Collider>().enabled = true;
                Death.Play();

            });

        }
        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.5f);
            UpgardeManager.UpgradeFactorOfCharacter = 5;
            GameController.changeGameState(GameState.Complete);
        }
        void PlayerEatEffect(Transform leg)
        {
            canfight = false;
            Destroy(this.GetComponent<Collider>());
            var currentInsect = ReferenceManager.instance.characterController.currentInsect;
            meshRenderer = currentInsect.GetComponentInChildren<SkinnedMeshRenderer>();
            if (meshRenderer) meshRenderer.material = black;
            currentInsect.transform.parent = leg.transform;
            currentInsect.transform.localPosition = Vector3.zero;

            leg.GetComponent<Collider>().enabled = false;
            currentInsect.transform.DOScale(0.06f, 0.5f).OnComplete(() =>
            {
                //var particle = Instantiate(ReferanceManager.instance.characterController.venomBreakingParticle, other.transform.position, Quaternion.identity);
                //particle.transform.parent = other.transform;
                //particle.transform.localPosition = Vector3.zero;
                //particle.transform.localScale = new Vector3(.15f, .15f, .15f);
                //Destroy(particle, .3f);
                currentInsect.SetActive(false);
                leg.GetComponent<Collider>().enabled = true;
                Death.Play();

            });
            StartCoroutine(delay());
        }
        private void FixedUpdate()
        {
            if (canfight)
            {

                transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * .2f);
            }
        }
        void ChangeBehaviour()
        {

            Destroy(this.GetComponent<Rigidbody>());
            //this.GetComponent<Collider>().isTrigger = true;
            transform.DORotate(new Vector3(0, -113f, 0), 0.1f);
            transform.DOMove(new Vector3(transform.position.x, 0.15f, transform.position.z), 0.1f);
        }
        void StartFight(bool canfight)
        {
            this.canfight = canfight;
        }

    }


}