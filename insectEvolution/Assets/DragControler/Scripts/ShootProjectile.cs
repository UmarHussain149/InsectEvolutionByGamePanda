using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
   // public static ShootProjectile instance;
    [SerializeField] private GameObject ShootingPrefeb;


    [SerializeField] private AnimationCurve ProjectileCurve;
    [SerializeField] private float HeightModifier;
    [SerializeField] private float Speed;

    public Transform LAstDestination;
    private void Start()
    {
      //  ProjectileCurve.
        //  instance = this;
        LAstDestination = GameObject.Find("destination1").transform;
    }
    public void ShootThis()
    {
        Shoot(LAstDestination);
        print("Shoot");

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Shoot(LAstDestination);

        }
        
    }
    public void Shoot(Transform Target)
    {
       // Transform Projectile = Instantiate(ShootingPrefeb, transform.position, Quaternion.identity,null).transform;
        //Projectile.transform.localPosition = Vector3.zero;

        StartCoroutine(ShootRoutine(Target, gameObject.transform));
    }
    private IEnumerator ShootRoutine(Transform Target, Transform Projectile)
    {
        Vector3 IniPos = Projectile.position;
        Vector3 FinalPos = Target.position;

        float T = 0;
        float CurveTime = 0;
        float Dis = Vector3.Distance(new Vector3(IniPos.x, 0, IniPos.z), new Vector3(FinalPos.x, 0, FinalPos.z));
        float DeltaT = Dis / Speed;


        while (T <= 1 && Projectile)
        {

            CurveTime = ProjectileCurve.Evaluate(T);
            Projectile.position = Vector3.Lerp(IniPos, FinalPos + Vector3.up * CurveTime * HeightModifier, T);

            T += Time.deltaTime / DeltaT;


            yield return null;
        }
    }

}
