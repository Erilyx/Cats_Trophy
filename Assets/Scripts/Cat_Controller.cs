using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Controller : MonoBehaviour
{

    private float startPosX, startPosY;
    private bool isBeingHeld = false;

    [Header("Dash Board")]
    public float speedX;
    public float spin;

    [Header("Cat Tributes")]
    public Vector3 com;
    private Vector3 startingCom;
    public Vector3 leanRight;
    public float spinMin; 
    public float spinMax;

    public bool isTryingToBalance = false;


    [Header("Attached Components")]
    public Rigidbody2D rb;
    public PolygonCollider2D polyCol;
    public YellowLine yellowLine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        polyCol = GetComponent<PolygonCollider2D>();
        startingCom = rb.centerOfMass;

        spinMin = 1;
        spinMax = 30;
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.centerOfMass = com;
        speedX = rb.velocity.x;
        spin = rb.angularVelocity;


        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

            rb.freezeRotation = true;  // so cats don't spin out of control when moving
           // polyCol.enabled = false;

        }
    }

    private void FixedUpdate()
    {

        if ((Mathf.Abs(spin) > spinMin) && (!isTryingToBalance))
        {
            ComBalancing();
        }


        if (Mathf.Abs(spin) < 0.001)

        { 
            isTryingToBalance = false;
        }


       /*
        if(spin == 0)
        {
            isTryingToBalance = false;
        }

        /*if((spin < spinThreshold * 0.1) && (isTryingToBalance))
        {
            com = startingCom;
            isTryingToBalance = false;
        }*/


    }


    public void ComBalancing()
    {

        if(spin < spinMax)  //if it's only spinning a ittle bit....
        {
            Vector3 lerpPoint = new Vector3 (com.x + (spin * 0.008f) , com.y, 0);
            com = Vector3.Lerp(com, lerpPoint, 0.8f);
            Invoke("ResetBalanceAttempt", 0.8f);
        }
        else  //so it can't fly out of wack crazy
        {
            Vector3 lerpPoint = new Vector3(com.x + (spinMax * 0.008f), com.y, 0);
            com = Vector3.Lerp(com, lerpPoint, 0.8f);
            Invoke("ResetBalanceAttempt", 0.8f);

        }

        //this was getting good!!!  How about lerping ^^^  
        /*
        if (spin < spinMax)
        {
            com.x += (spin * 0.008f);
            Invoke("ResetBalanceAttempt", 0.5f);
        }
        else
        {
            com.x += (spinMax * 0.01f);
            Invoke("ResetBalanceAttempt", 1);
        }
        */

        isTryingToBalance = true; 

        /*
       if(spin > 0)
        {
            com = Vector3.Lerp(leanRight * -spin, leanRight, 1);
        }
        else
        {
            com = Vector3.Lerp(-leanRight * spin, -leanRight, 1);
        }
        isTryingToBalance = true;*/

    }
    
    public void ResetBalanceAttempt()
    {
        isTryingToBalance = false;
    }
    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - transform.localPosition.x;
            startPosY = mousePos.y - transform.localPosition.y;

            isBeingHeld = true;
        }
        
    }



    private void OnMouseUp()
    {
        isBeingHeld = false;
        rb.freezeRotation = false;
        // polyCol.enabled = true;   //why does it act differently some times?
        rb.velocity = new Vector2(0, 0);
        yellowLine.SetLineHeight(transform.position.y);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + transform.rotation * com, 0.1f);
        

    }
}
