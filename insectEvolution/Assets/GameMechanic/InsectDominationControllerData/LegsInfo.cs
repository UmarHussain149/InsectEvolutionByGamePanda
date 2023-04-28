//using SpericalGravity;
//using System.Collections;
//using System.Collections.Generic;
//using THEBADDEST.InteractSyetem;
//using UnityEngine;
//using DG.Tweening;
//using InsectEvolution;

//public class LegsInfo : MonoBehaviour
//{
//    private GameObject insect;
//    [SerializeField] THEBADDEST.CharacterController3.CharacterController characterController;
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.TryGetComponent<Collectable>(out Collectable collectable))
//        {

//            collectable.gameObject.transform.parent = this.transform;
//            collectable.gameObject.transform.localPosition = Vector3.zero;
//            Destroy(collectable.gameObject.GetComponent<GravityBody>());
//            Destroy(collectable.gameObject.GetComponent<Rigidbody>());
//            Destroy(collectable.gameObject.GetComponent<Collider>());
//            Destroy(collectable.gameObject.GetComponent<Collectable>());
//            insect = collectable.gameObject;
//            insect.transform.DOScale(0, 1f).OnComplete(() =>
//            {
//                characterController.totalPickUp++;
//                SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 0.7f);
//                characterController.collectionParticle.Play();
//                //Destroy(insect.gameObject);
//            });


//            if (characterController.totalPickUp >= 5)
//            {
//                characterController.totalPickUp = 0;
//                if (characterController.currentLevelCount < 6)
//                {
//                    characterController.animation.DORestart();
//                    //characterController.animation.DORestartById("Rotate");
//                    characterController.onWatermelon.Play();
//                    characterController.currentLevelCount++;
//                    characterController.currentLevel = (THEBADDEST.CharacterController3.LevelType)characterController.currentLevelCount;
//                    characterController.UpgardeLevelOfCharacter(characterController.currentLevel);
//                }
//            }
//        }

//    }
//}
