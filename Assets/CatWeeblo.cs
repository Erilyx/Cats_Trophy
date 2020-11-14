using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWeeblo : MonoBehaviour
{

    public Rigidbody2D rb;
    public float WeebloStrength;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeCOM();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void ChangeCOM()
    {

        Vector3 tempCOM = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y - WeebloStrength, 0);
        rb.centerOfMass = tempCOM;

    }
}
