using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image healthBar;
    [SerializeField] bool canUpdate;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] Transform jump;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            UpdateBar(-0.15f);
            hitParticle.Play();
            //this.GetComponent<DOTweenAnimation>().DORestartById("Hit");

        }
    }
    public void UpdateBar(float value)
    {
        if (healthBar.fillAmount <= 0.1)
        {
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            healthBar.transform.parent.gameObject.SetActive(false);
            transform.DOLocalJump(jump.localPosition, 4, 1, 2f).OnComplete(() =>
             {

                 //gameObject.SetActive(false);
             });
            //this.gameObject.SetActive(false);
            particleSystem.Play();
            canUpdate = false;
            StartCoroutine(Delay());
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (canUpdate)
        {

            var temp = healthBar.fillAmount;
            temp += value;
            healthBar.DOFillAmount(temp, 1f);
        }
    }
    IEnumerator Delay()
    {
        UpgardeManager.UpgradeFactorOfCharacter = 5;
        Debug.Log("UpgradeFactorOfCharacter" + UpgardeManager.UpgradeFactorOfCharacter);
        if (boolOfEnd == 0)
        {

            OneTime = 1;
            boolOfEnd = 1;
        }
        yield return new WaitForSeconds(1);
        GameController.changeGameState(GameState.Complete);
        yield return new WaitForSeconds(2);


    }
    int boolOfEnd
    {
        set { PlayerPrefs.SetInt("boolOfEnd", value); }
        get { return PlayerPrefs.GetInt("boolOfEnd"); }
    }
    public static int OneTime
    {
        set { PlayerPrefs.SetInt("OneTime", value); }
        get { return PlayerPrefs.GetInt("OneTime"); }
    }
}
