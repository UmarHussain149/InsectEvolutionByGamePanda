using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController2 : MonoBehaviour
{

    public static Vector2 Dir;
    public float H_Res = 0;

    [Space(10)] public bool Enable_Input;


    [Space(10)] public InputType E_inputType;


    public float Min_Input_Treshhold = 0.1f;
    public float Scenctivity;
    public float DeScenctivity;



    private Vector3 Ini_Pos;
    private Touch m_Touch;
    public float Delta_X;
    public float Delta_Y;


    public Vector2 m_Dir;

    public void OnEnable()
    {
        Application.targetFrameRate = 1000;
        Dir = Vector2.zero;
        H_Res = Screen.width;
        //#if UNITY_EDITOR
        //        E_inputType = InputType.Editor;
        //#else
        //        E_inputType = InputType.Touch;

        //#endif
    }

    void Update()
    {


        if (Enable_Input)
        {
            //ReferenceManager.instance._UI_Handler.SwipeToMoveHelp(false);
        }
        else
        {
            return;
        }

        if (E_inputType == InputType.Editor)
            Handle_EditorInput();
        else
            Handle_MobileInput();

        Dir.x = Mathf.Clamp(Dir.x, -1, 1);
        Dir.y = Mathf.Clamp(Dir.y, -1, 1);

        m_Dir = Dir;

    }

    public void Handle_MobileInput()
    {
        if (Input.touchCount > 0)
        {
            m_Touch = Input.touches[0];
            if (m_Touch.phase == TouchPhase.Began)
            {
                On_PointerDown(m_Touch.position);
            }
            else if (m_Touch.phase == TouchPhase.Moved)
            {
                On_PointerDragging(m_Touch.position);
            }
            else if (m_Touch.phase == TouchPhase.Ended || m_Touch.phase == TouchPhase.Canceled || m_Touch.phase == TouchPhase.Stationary)
            {
                On_PointerUp();
            }
        }
        else
        {
            On_PointerUp();
        }


    }

    public void Handle_EditorInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            On_PointerDown(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            On_PointerDragging(Input.mousePosition);
        }
        else
        {
            On_PointerUp();
        }




    }

    public Vector2 DeltaPos;
    public void On_PointerDown(Vector3 start_pos)
    {
        Ini_Pos = Input.mousePosition;
        RayCast_Handler(Ini_Pos);
        // Game_Controller.instance.animPlay = true;
    }

    public void On_PointerDragging(Vector3 C_pos)
    {
        DeltaPos = C_pos - Ini_Pos;
        float m_Sign = Mathf.Sign(DeltaPos.magnitude);

        Delta_X = 1 - (H_Res - Mathf.Abs(DeltaPos.x)) / H_Res;
        Delta_Y = 1 - (H_Res - Mathf.Abs(DeltaPos.y)) / H_Res;

        Ini_Pos = Vector2.Lerp(Ini_Pos, C_pos, Time.deltaTime * Scenctivity);

        if (Mathf.Abs(DeltaPos.magnitude) > Min_Input_Treshhold)
        {
            //Dir = new Vector2(Delta * Mathf.Sign(DeltaPos.x), Delta * Mathf.Sign(DeltaPos.y));
            Dir = new Vector2(Delta_X * Mathf.Sign(DeltaPos.x), Delta_Y * Mathf.Sign(DeltaPos.y));
            //Dir = new Vector2(Delta * Mathf.Sign(DeltaPos.x),0);

        }
        else
        {
            Dir = Vector2.Lerp(Dir, Vector2.zero, Time.deltaTime * DeScenctivity);
        }

        //HandleRaycastHitObj(C_pos);

    }

    public void On_PointerUp()
    {
        Dir = Vector2.Lerp(Dir, Vector2.zero, Time.deltaTime * DeScenctivity);
        // Game_Controller.instance.animPlay = false;
        // Game_Controller.instance.anim.Play("Car", -1, 0.5f * Time.deltaTime);
        //LeaveRaycastHitObj();
    }

    #region ("Raycast Interacytion")


    public Camera mainCamera;

    public float Ray_length;

    public LayerMask LayerMask_Interactable;

    [HideInInspector]
    public static RaycastHit rayCastHit;

    bool IspickedSomething = false;
    GameObject raycatHit_Obj;
    private void RayCast_Handler(Vector3 ScreenInteract_Pos)
    {
      //  bool Hitsomething = false;

       // Ray ray = mainCamera.ScreenPointToRay(ScreenInteract_Pos);

       // if (Physics.Raycast(ray, out rayCastHit, Ray_length, LayerMask_Interactable))
        //{
        //    Debug.DrawRay(ray.origin, ray.direction * rayCastHit.distance, Color.yellow);
        //    raycatHit_Obj = rayCastHit.collider.gameObject;
        //    Hitsomething = true;
        //}
        //else
        //{
        //    raycatHit_Obj = null;
        //    Hitsomething = false;
        //}
    }

    //public void HandleRaycastHitObj(Vector3 ScreenInteract_Pos)
    //{
    //    if (raycatHit_Obj)
    //    {
    //        Interaction_Base this_Interactable = rayCastHit.collider.GetComponent<Interaction_Base>();

    //        if (this_Interactable != null)
    //        {
    //            Ray ray = mainCamera.ScreenPointToRay(ScreenInteract_Pos);
    //            this_Interactable.OnDrag(ray.direction);
    //        }

    //    }
    //}

    //public void LeaveRaycastHitObj()
    //{

    //    if (raycatHit_Obj)
    //    {
    //        Interaction_Base this_Interactable = rayCastHit.collider.GetComponent<Interaction_Base>();

    //        if (this_Interactable != null)
    //        {
    //            this_Interactable.OnUp(raycatHit_Obj);
    //        }

    //    }
    //    raycatHit_Obj = null;
    //}

    #endregion
}


public enum InputType
{
    Editor,
    Touch,
}