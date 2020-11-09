using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Controller : MonoBehaviour
{

    private float startPosX, startPosY;
    private bool isBeingHeld = false;
    public Rigidbody2D rb;
    public PolygonCollider2D polyCol;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        polyCol = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

            rb.freezeRotation = true;  // so cats don't spin out of control when moving
           // polyCol.enabled = false;

        }
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


    }
}
