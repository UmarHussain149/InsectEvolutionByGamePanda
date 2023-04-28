using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool Enable_Input;


    [Space(10)]
    public float Scenctivity;
    public float DeScenctivity;
    public bool IsSwerl;

    [Space(10)]

    public bool TouchnHold = false;
    public bool TouchUp = false;

    [Space(10)]
    public string Horizonal_Input;
    public string Verticle_Input;

    [Space(10)]
    public bool InvertX;
    public bool InvertY;



    private Vector2 Dir;
    private float H_Res = 0;
    private float Min_Input_Treshhold = 0.1f;
    private Vector3 Ini_Pos;
    private Touch m_Touch;
    private float Delta_X;
    private float Delta_Y;
    private Vector2 m_Dir;
    private Vector2 DeltaPos;
    private bool IsInside = false;


    private void OnEnable()
    {

        Dir = Vector2.zero;
        H_Res = Screen.width;

        TouchnHold = false;
        TouchUp = false;

        IsInside = false;

    }

    private void Update()
    {
        if (Enable_Input)
        {
            //ReferenceManager.instance._UI_Handler.SwipeToMoveHelp(false);
        }
        else
        {
            return;
        }


        Handle_EditorInput();

        NormalizeInput();
        UpdateCrossPlatformInput();
    }

    private void NormalizeInput()
    {
        Dir.x = Mathf.Clamp(Dir.x, -1, 1);
        Dir.y = Mathf.Clamp(Dir.y, -1, 1);

        Dir.x = Dir.x * (InvertX ? -1 : 1);
        Dir.y = Dir.y * (InvertY ? -1 : 1);

        if (Mathf.Abs(Dir.x) < 0.01f)
        {
            Dir.x = 0;
        }

        if (Mathf.Abs(Dir.y) < 0.01f)
        {
            Dir.y = 0;
        }

        m_Dir = Dir;

    }

    private void UpdateCrossPlatformInput()
    {
       
        CrossPlatformInputManager.SetAxis(Horizonal_Input, Dir.x);
        CrossPlatformInputManager.SetAxis(Verticle_Input, Dir.y);
    }

    private void Handle_EditorInput()
    {
        if (!IsInside)
        {
            TouchUp = true;
            TouchnHold = false;

            On_PointerUp();
            return;
        }

        if (Input.GetMouseButtonDown(0) )
        {
            TouchUp = false;
            TouchnHold = true;

            On_PointerDown(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) )
        {
            On_PointerDragging(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) )
        {
            On_PointerUp();
        }
        else
        {
            TouchUp = true;
            TouchnHold = false;

            On_PointerUp();
        }

    }

    private void On_PointerDown(Vector3 start_pos)
    {
       Ini_Pos = Input.mousePosition;
       //RayCast_Handler(Ini_Pos);
    }

    private void On_PointerDragging(Vector3 C_pos)
    {
        DeltaPos = C_pos - Ini_Pos;
        float m_Sign = Mathf.Sign(DeltaPos.magnitude);

        Delta_X = 1 - (H_Res - Mathf.Abs(DeltaPos.x))/H_Res;
        Delta_Y = 1 - (H_Res - Mathf.Abs(DeltaPos.y))/H_Res;

        if (IsSwerl)
        {
            Ini_Pos = Vector2.Lerp(Ini_Pos, C_pos, Time.deltaTime * Scenctivity);
        }
        if (Mathf.Abs(DeltaPos.magnitude) > Min_Input_Treshhold)
        {
            //Dir = new Vector2(Delta * Mathf.Sign(DeltaPos.x), Delta * Mathf.Sign(DeltaPos.y));
            Dir = new Vector2(Delta_X * Mathf.Sign(DeltaPos.x), Delta_Y * Mathf.Sign(DeltaPos.y));
            //Dir = new Vector2(Delta * Mathf.Sign(DeltaPos.x),0);

        }
        else
        {
            if (IsSwerl)
            {
                Dir = Vector2.Lerp(Dir, Vector2.zero, Time.deltaTime * DeScenctivity);
            }
        }

        //HandleRaycastHitObj(C_pos);

    }

    private void On_PointerUp()
    {
        Dir = Vector2.Lerp(Dir, Vector2.zero, Time.deltaTime * DeScenctivity);
        //LeaveRaycastHitObj();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsInside = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsInside = false;
    }

    private void OnDisable()
    {
        Dir = Vector3.zero;
        UpdateCrossPlatformInput();
    }

}



