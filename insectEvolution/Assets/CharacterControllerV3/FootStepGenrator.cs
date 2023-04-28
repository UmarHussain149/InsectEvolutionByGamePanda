using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FootStepGenrator : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask layerMask;
    public GameObject prefebFootStep;
    [SerializeField] bool canGenrate;

    public float rayCastLength;

    private void Awake()
    {
        GameController.onGameplay += OngamePlay;
    }


    // Update is called once per frame

    void FixedUpdate()
    {
        RaycastHit hitPointFront;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitPointFront, rayCastLength, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayCastLength/*hitPointFront.distance*/, Color.black);
            Debug.Log("Front Hit");
            if (hitPointFront.transform.CompareTag("Ground"))
            {
                Debug.Log("Front Hit" + hitPointFront.transform.gameObject.name);
                if (canGenrate)
                {
                    var foot = Instantiate(prefebFootStep, hitPointFront.point, prefebFootStep.transform.rotation);
                    Destroy(foot, 1f);
                    canGenrate = false;
                    StartCoroutine(Delay());

                }
            }
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        canGenrate = true;

    }
    void OngamePlay()
    {
        canGenrate = true;
    }
}
