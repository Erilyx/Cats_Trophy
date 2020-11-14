using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeClickable : MonoBehaviour
{
    private float startPosX, startPosY;
    private bool isBeingHeld = false;
    public bool wasJustReleased = false;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

            rb.freezeRotation = true;  // so cats don't spin out of control when moving
            rb.simulated = false;

        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
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
        rb.simulated = true;
        rb.velocity = new Vector2(0, 0);
        wasJustReleased = true;
    }

    private void OnCollisionEnter2D()
    {
        wasJustReleased = false;
    }
}
