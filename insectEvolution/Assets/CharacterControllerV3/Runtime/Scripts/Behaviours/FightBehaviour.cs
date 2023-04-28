
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using System;
using UnityEngine;


namespace THEBADDEST.CharacterController3
{


    [CreateAssetMenu(menuName = "THEBADDEST/CharacterBehaviours/FightBehaviour", fileName = "FightBehaviour", order = 0)]
    public class FightBehaviour : CharacterBehaviour
    {

        [SerializeField] AnimationCurve animationCurve;
        [SerializeField] GameObject prefeb;
        //[SerializeField] Transform target;
        [SerializeField] Transform SponPostion;
        [SerializeField] float jumpPower;
        [SerializeField] float jumpDelay;

        Rigidbody rigidbody => behaviour.Rigidbody;
        Transform transform => animator.transform;
        Animator animator => behaviour.Animator;
        Vector3 currentPosition;
        float intercept;
        public bool canFight;

        protected override void Init()
        {
            base.Init();

        }

        protected override void DoUpdate()
        {
            if (CanControl)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
        }


        protected override void DoFixedUpdate()
        {

        }

        void Shoot()
        {
            Debug.Log("Shoot");
            Vibration.VibrateNope();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shoot, 1);

            var bullat = Instantiate(prefeb, transform.position, prefeb.transform.rotation);
            //bullat.GetComponent<ParticleSystem>().Play();
            bullat.transform.DOJump(targetBoss.transform.position, 4, 1, 1);
            Destroy(bullat, 1f);
        }

    }





}