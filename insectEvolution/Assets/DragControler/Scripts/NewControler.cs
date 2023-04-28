using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
public class NewControler : MonoBehaviour
{
    public static NewControler instance;
    

    public float touchSpeed, thrust,PlayerSpeed;
    Vector2 lastMousePos;
    public Rigidbody rb;
 
    public bool IsProjectile;
    private Vector3 rotOffsetx;
    private Vector3 rotOffsety;
    //[Header(" Projectile Controler")]
    // Projectile Controler
   
 
  
   // public Transform positionlast;
    //  public GameObject CoinsFind;
   
   
 
    private void OnEnable()
    {
        instance = this;
        PlayerSpeed = 0; 
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
       
    
        rb = GetComponent<Rigidbody>();
      
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = positionlast.transform.position;
       

    }
   
    private void FixedUpdate()
    {
       
        //start Non Projectile
        if (IsProjectile == true)
        {
         //   transform.Translate(transform.forward * PlayerSpeed * Time.fixedDeltaTime);
            Vector2 deltaPos = Vector2.zero;
            if (Input.GetMouseButton(0))
            {
                Vector2 currentMousePos = Input.mousePosition * touchSpeed;

                if (lastMousePos == Vector2.zero)
                {
                    lastMousePos = currentMousePos;
                }
                deltaPos = (currentMousePos - lastMousePos) * Time.fixedDeltaTime;
                lastMousePos = currentMousePos;
              //  rb.drag = 2.5f;
                Vector3 force = new Vector3(-deltaPos.x, deltaPos.y, 0) * thrust * Time.fixedDeltaTime;
                

                rb.AddForce(force, ForceMode.Impulse);

            }
            else
            {
                rotOffsety = new Vector3(0, 0, 0);
                lastMousePos = Vector2.zero;
            }
            // End non Projectile


        }
    }


}
