
using UnityEngine;
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;

namespace THEBADDEST.InteractSyetem
{


    public class Skull : TriggerEffector
    {

        THEBADDEST.CharacterController3.CharacterController characterController;

        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            triggered = true;
            base.OnEffect(other, container);
            if (other.CompareTag("Legs"))
            {
                ReferenceManager.instance.characterController.totallCollection++;
                Destroy(this.GetComponent<Collider>());
                transform.parent = other.transform;
                transform.localPosition = Vector3.zero;
                other.GetComponent<Collider>().enabled = false;
                this.transform.DOScale(0f, 0.5f).OnComplete(() =>
                {
                    Vibration.VibrateNope();
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.skullcollection, 1);
                    other.GetComponent<Collider>().enabled = true;
                    SkullCOunt++;
                    if (SkullCOunt == 2)
                    {
                        SkullCOunt = 0;
                        CoinsManager.Instance.AddCoins(1);
                    }
                    CoinsManager.Instance.AddCoins(WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                    Vibration.VibrateNope();
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.skullcollection, 1);
                    ReferenceManager.instance.characterController.skullPickupParticle.Play();
                    Destroy(this.gameObject);
                });
                //ReferanceManager.instance.characterController.UpdateLegs();

                Debug.Log("Legs");
            }
            if (other.CompareTag("Player"))
            {

                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
                gameObject.SetActive(false);
                characterController.skullPickupParticle.Play();
                characterController.totallCollection++;
                CoinsManager.Instance.AddCoins(WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                CoinsManager.Instance.AddCoins(1);
                Vibration.VibrateNope();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.skullcollection, 1);

            }


        }
        public Vector2 WordPointToCanvasPoint(Camera camera, Vector3 worldPoint, RectTransform canvasRect)
        {
            Vector2 viewportPosition = camera.WorldToViewportPoint(worldPoint);
            Vector2 screenPosition = new Vector2(((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)), ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
            return screenPosition;
        }


        int SkullCOunt
        {
            get { return PlayerPrefs.GetInt("SkullCOunt"); }
            set { PlayerPrefs.SetInt("SkullCOunt", value); }
        }
    }


}