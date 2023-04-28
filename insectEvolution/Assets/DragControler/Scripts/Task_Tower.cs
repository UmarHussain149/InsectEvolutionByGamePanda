using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Task_Tower : MonoBehaviour
{

    public bool Enable_WaterThrow;




    [SerializeField] private Transform StartPoint;

    [SerializeField] private Transform EndPoint;

    public float Resolution = 10;
    public List<Vector3> Colelction = new List<Vector3>();

    [SerializeField] private float HMagnitude;
    [SerializeField] private AnimationCurve HorizontalCurve;
    public float CurveTime_X;
    public float InterpulateAmount;
    public float radius = 0.25f;
    public float FoloSpeed = 10;

    public void Start()
    {
        Colelction.Clear();
        for (int i = 0; i < Resolution; i++)
        {
            Colelction.Add(StartPoint.position);
        }
    }

    void Update()
    {
        //Colelction.Clear();
        InterpulateAmount = 0;
        if (Enable_WaterThrow)
        {
            for (int i = 0; i < Resolution; i++)
            {
                InterpulateAmount = i / Resolution;

                ThrowWater(i, InterpulateAmount);
            }

            Vector3 newPOS = Vector3.Lerp(EndPoint.position, StartPoint.position, Time.deltaTime * FoloSpeed);
            newPOS.y = EndPoint.position.y;
            EndPoint.position = newPOS;
        }
    }






    void ThrowWater(int i, float t)
    {


        HMagnitude = StartPoint.position.x - EndPoint.position.x;

        CurveTime_X = HorizontalCurve.Evaluate(t);
        Vector3 iniPos = Vector3.Lerp(StartPoint.position, EndPoint.position, t);

        float f   = Mathf.Lerp(StartPoint.position.x, EndPoint.position.x, CurveTime_X);
        float f_Z = Mathf.Lerp(StartPoint.position.z, EndPoint.position.z, CurveTime_X);

        iniPos.x = f;
        //iniPos.y = StartPoint.position.y + radius * Colelction.Count;
        //iniPos.z = StartPoint.position.z;
        iniPos.z = f_Z;

        Colelction[i] = (iniPos);
        //transform.position = Vector3.Lerp(transform.position, iniPos, 0.25f);


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var item in Colelction)
        {
            Gizmos.DrawWireSphere(item, radius);
        }

    }
}
