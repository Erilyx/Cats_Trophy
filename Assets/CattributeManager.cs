using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CattributeManager : MonoBehaviour
{

    public Rigidbody2D rb;
    public MakeClickable makeClickableScript;
    public SpriteRenderer catSprite;

    [Header("Cattributes")]
    public float Weight;
    public float Gravity;
    public float AngularDrag;
    public float comX;
    public float comY;

    [Header("COM Adjust")]
    public LayerMask groundLayer;
    public bool isLeftLanding, isRightLanding, isCenterLanding, isAdjusting = false;
    public Vector3 rayOffset;
    public Vector3 perCatColliderAlignment;
    public float groundLength;
    public float catHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        makeClickableScript = GetComponent<MakeClickable>();
        catSprite = GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        rb.mass = Weight;
        rb.gravityScale = Gravity;
        rb.angularDrag = AngularDrag;
        comX = rb.centerOfMass.x;
        comY = rb.centerOfMass.y;


        isLeftLanding = Physics2D.Raycast(transform.position + perCatColliderAlignment - rayOffset, Vector2.down, groundLength, groundLayer);
        isRightLanding = Physics2D.Raycast(transform.position + perCatColliderAlignment + rayOffset, Vector2.down, groundLength, groundLayer);
        isCenterLanding = Physics2D.Raycast(transform.position + perCatColliderAlignment, Vector2.down, groundLength, groundLayer);

        if(makeClickableScript.wasJustReleased == true && isAdjusting == false)
        {
            if (isCenterLanding || isLeftLanding || isRightLanding)
            {
                isAdjusting = true;
                SenseLanding();
            }
        }
    }

    private void FixedUpdate()
    {
 
    }


    public void SenseLanding()
    {
        if (isCenterLanding == false)
        {
            if (isLeftLanding == false)
            {
                //left and center are both missing, shift weight to the right
                Vector3 comAdjust = new Vector3((rb.centerOfMass.x + rayOffset.x) * 0.8f, rb.centerOfMass.y - (catSprite.size.y * 0.4f), 0);
                rb.centerOfMass = comAdjust;
            }


            if (isRightLanding == false)
            {

            }
        }

    }

    private void OnCollisionEnter2D()
    {
        isAdjusting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + perCatColliderAlignment, transform.position + perCatColliderAlignment + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position + perCatColliderAlignment + rayOffset, transform.position + perCatColliderAlignment + rayOffset + (Vector3.down * groundLength));
        Gizmos.DrawLine(transform.position + perCatColliderAlignment - rayOffset, transform.position + perCatColliderAlignment - rayOffset + (Vector3.down * groundLength));

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.rotation * rb.centerOfMass, 0.1f);
    }
}
